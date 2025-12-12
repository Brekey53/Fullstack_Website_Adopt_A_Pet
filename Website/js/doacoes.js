document.addEventListener("DOMContentLoaded", function () {
  let precoUnitarioAtual = 0;
  let itemAtual = "";

  const modal = document.getElementById("modalDoacao");

  modal.addEventListener("shown.bs.modal", () => {
    const backdrop = document.querySelector(".modal-backdrop");
    if (backdrop) {
      backdrop.style.opacity = "0.87";
      backdrop.style.backgroundColor = "#000";
    }
  });

  const selectAnimal = document.getElementById("selectAnimal");
  const inputQuantidade = document.getElementById("inputQuantidade");
  const labelTotal = document.getElementById("labelTotal");
  const labelPreco = document.getElementById("labelPreco");
  const labelItem = document.getElementById("labelItem");
  const btnConfirmar = document.getElementById("btnConfirmarDoacao");
  const animaisDoacoes = document.getElementById("animaisDoacoes");

  // 1. Carregar a lista de animais da API ao abrir a página
  carregarAnimais();

  // 2. Configurar os botões de "Doar" (Osso e Manta)
  const botoesDoar = document.querySelectorAll(".btn-doar-trigger");

  botoesDoar.forEach((botao) => {
    botao.addEventListener("click", function () {
      // Ler os dados data-item e data-preco do botão clicado
      itemAtual = this.getAttribute("data-item");
      precoUnitarioAtual = parseFloat(this.getAttribute("data-preco"));

      // Atualizar o Modal com estes dados
      labelItem.textContent = "Doar " + itemAtual;
      labelPreco.textContent = precoUnitarioAtual.toFixed(2);
      inputQuantidade.value = 1; // Resetar quantidade
      atualizarTotal(); // Calcular total inicial
    });
  });

  // 3. Atualizar o total quando a quantidade muda
  inputQuantidade.addEventListener("input", atualizarTotal);

  function atualizarTotal() {
    const qtd = parseInt(inputQuantidade.value) || 0;
    const total = (qtd * precoUnitarioAtual).toFixed(2);
    labelTotal.textContent = total;
  }

  // 4. Função para carregar animais da API
  async function carregarAnimais() {
    try {
      const response = await fetch("https://localhost:7035/api/caes");

      if (!response.ok) throw new Error("Erro ao carregar animais");

      const animais = await response.json();

      // Limpar o select
      selectAnimal.innerHTML =
        '<option value="" selected disabled>Escolha um patudo...</option>';
      selectAnimal.innerHTML +=
        '<option value="0">Qualquer animal (Sem preferência)</option>';
      selectAnimal.innerHTML += '<option value="-1">Vários cães</option>';

      // Adicionar cada animal como opção
      animais.forEach((animal) => {
        if (animal.disponivel) {
          const option = document.createElement("option");
          option.value = animal.caoId;
          option.textContent = animal.nome;
          selectAnimal.appendChild(option);
        }
      });

      selectAnimal.addEventListener("change", () => {
        if (selectAnimal.value === "-1") {
          animaisDoacoes.innerHTML = `
            <label class="my-2">A que cães gostaria de oferecer? (Ex: Simba, Timon, Jaime)</label>
            <input type="text" id="inputListaAnimais" style="width: 300px; height: 35px; padding: 5px;"/> `;
        } else {
          animaisDoacoes.innerHTML = "";
        }
      });
    } catch (error) {
      console.error(error);
      selectAnimal.innerHTML =
        "<option disabled>Erro ao carregar lista</option>";
    }
  }

  // 5. Enviar a Doação (Ao clicar em Confirmar no Modal)
  btnConfirmar.addEventListener("click", async function () {
    const numeroCartao = document.getElementById("inputCartao").value;
    const nomeDoador = document.getElementById("inputNomeDoador").value;
    const animalId = selectAnimal.value;
    const quantidade = inputQuantidade.value;
    const valorTotal = quantidade * precoUnitarioAtual;

    if (quantidade >= 10) {
      alert("Por favor insira no máximo 10 produtos por doação");
      inputQuantidade.value = 1;
      return;
    }

    if (!numeroCartao || !nomeDoador || !animalId || quantidade <= 0) {
      alert("Por favor, preencha todos os campos obrigatórios.");
      return;
    }

    const dadosDoacao = {
      nomeDoador: nomeDoador,
      numeroCartao: numeroCartao,
      valor: valorTotal,
    };

    try {
      const textoOriginal = btnConfirmar.innerHTML;
      btnConfirmar.innerHTML =
        '<i class="fa fa-spinner fa-spin"></i> A processar...';
      btnConfirmar.disabled = true;

      // Enviar para o Mountebank
      const resposta = await fetch("https://localhost:7035/api/Pagamentos", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(dadosDoacao),
      });

      let resultado = null;
      try {
        resultado = await resposta.json();
      } catch (e) {
        resultado = null;
      }

      if (!resposta.ok) {
        throw new Error(
          resultado?.erro ||
            resultado?.mensagem ||
            "Erro no processamento da doação"
        );
      }

      let produtoAtual = document.querySelector('[data-item]').dataset.item;
      
      let descricaoDoacao = "";
      if (selectAnimal.value === "-1") {
        const lista = document.getElementById("inputListaAnimais").value;
        descricaoDoacao = `Cães selecionados: ${lista}`;
      } else if (selectAnimal.value === "0") {
        descricaoDoacao = "Doação sem preferência";
      } else {
        const nomeAnimal =
          selectAnimal.options[selectAnimal.selectedIndex].text;
        descricaoDoacao = `${nomeAnimal}`;
      }

   

      await fetch("https://localhost:7035/api/doacoes", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          nomeDoador,
          tipoDoacao: "Bens ou Comida",
          valor: valorTotal,
          descricao: descricaoDoacao + " - " + produtoAtual,
          data: new Date().toISOString(),
        }),
      });

      // Sucesso!
      alert(`🎉 Sucesso! ${resultado.mensagem}`);

      // Fechar o modal
      const modalElement = document.getElementById("modalDoacao");
      const modalInstance = bootstrap.Modal.getInstance(modalElement);
      modalInstance.hide();
    } catch (erro) {
      alert("❌ Erro: " + erro.message);
    } finally {
      // Restaurar botão
      btnConfirmar.innerHTML = "❤️ Confirmar Doação";
      btnConfirmar.disabled = false;
    }
  });
});

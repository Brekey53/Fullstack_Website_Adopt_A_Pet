// document.addEventListener("DOMContentLoaded", function () {
//   const btnDoarOsso = document.getElementById("btnDoarOsso");

//   btnDoarOsso.addEventListener("click", async function (e) {
//     e.preventDefault();

//     const dados = { // alterar futuramente
//       numeroCartao: "0000",
//       valor: 2.5,
//     };

//     try {
//       const resposta = await fetch("http://localhost:5013/api/doacoes", {
//         method: "POST",
//         headers: {
//           "Content-Type": "application/json",
//         },
//         body: JSON.stringify(dados),
//       });

//       const result = await resposta.json();

//       if (!resposta.ok) {
//         alert("Erro: " + result);
//         return;
//       }
//     } catch (erro) {
//       console.error("Erro a contactar API:", erro); // TODO: apagar, é só para testar
//     }
//   });

//   const btnDoarManta = document.getElementById("btnDoarManta");

//   btnDoarManta.addEventListener("click", async function (e) {
//     e.preventDefault();
//     const dados = {
//       // TODO: arranjar segunda pagina para testar dados do cartão ou um Modal ?
//       numeroCartao: "1234",
//       valor: 4,
//     };

//     try {
//       const resposta = await fetch("http://localhost:5013/api/doacoes", {
//         method: "POST",
//         headers: {
//           "Content-Type": "application/json",
//         },
//         body: JSON.stringify(dados),
//       });

//       const result = await resposta.json();

//       if (!resposta.ok) {
//         alert("Erro: " + result);
//         return;
//       }

//       alert(result.mensagem);
//     } catch (erro) {
//       console.error("Erro a contactar API:", erro);
//     }
//   });
// });

document.addEventListener("DOMContentLoaded", function () {
  // Variáveis globais para controlar o estado do modal
  let precoUnitarioAtual = 0;
  let itemAtual = "";

  // Elementos do DOM
  const selectAnimal = document.getElementById("selectAnimal");
  const inputQuantidade = document.getElementById("inputQuantidade");
  const labelTotal = document.getElementById("labelTotal");
  const labelPreco = document.getElementById("labelPreco");
  const labelItem = document.getElementById("labelItem");
  const btnConfirmar = document.getElementById("btnConfirmarDoacao");

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
      // Nota: Usa a rota correta da tua API. Se for "api/caes" usa essa.
      // Se queres apenas os disponíveis, certifica-te que a API retorna isso ou filtra aqui.
      const response = await fetch("http://localhost:5013/api/caes");

      if (!response.ok) throw new Error("Erro ao carregar animais");

      const animais = await response.json();

      // Limpar o select
      selectAnimal.innerHTML =
        '<option value="" selected disabled>Escolha um patudo...</option>';
      selectAnimal.innerHTML +=
        '<option value="0">Qualquer animal (Sem preferência)</option>';

      // Adicionar cada animal como uma opção
      animais.forEach((animal) => {
        // Filtro opcional: Só mostrar se estiver disponível
        if (animal.disponivel) {
          const option = document.createElement("option");
          option.value = animal.caoId; // O ID que vai para a BD
          option.textContent = `${animal.nome}`;
          selectAnimal.appendChild(option);
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
    // Validação simples
    const numeroCartao = document.getElementById("inputCartao").value;
    const nomeDoador = document.getElementById("inputNomeDoador").value;
    const animalId = selectAnimal.value;
    const quantidade = inputQuantidade.value;
    const valorTotal = quantidade * precoUnitarioAtual;

    if (!numeroCartao || !nomeDoador || !animalId || quantidade <= 0) {
      alert("Por favor, preencha todos os campos obrigatórios.");
      return;
    }

    // Construir o objeto para enviar à API
    // NOTA: A tua API DoacoesController tem de estar à espera destes campos!
    const dadosDoacao = {
      nomeDoador: nomeDoador,
      numeroCartao: numeroCartao,
      valor: valorTotal,
      // Campos extra que podes querer adicionar ao teu DTO na API C#
      // animalId: parseInt(animalId),
      // tipoItem: itemAtual,
      // quantidade: parseInt(quantidade)
    };

    try {
      // Botão em estado de "carregando"
      const textoOriginal = btnConfirmar.innerHTML;
      btnConfirmar.innerHTML =
        '<i class="fa fa-spinner fa-spin"></i> A processar...';
      btnConfirmar.disabled = true;

      // Enviar para o Mountebank (via tua API C#)
      const resposta = await fetch("http://localhost:5013/api/doacoes", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(dadosDoacao),
      });

      const resultado = await resposta.json();

      if (!resposta.ok) {
        // Se o Mountebank ou a API recusarem (ex: cartão 0000)
        throw new Error(resultado.mensagem || "Erro no processamento da doação");
      }

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

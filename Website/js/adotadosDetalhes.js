const id = new URLSearchParams(window.location.search).get("id");

async function carregarCao() {
    try {
        const resposta = await fetch(`https://localhost:7035/api/caes/${id}`);
        if (!resposta.ok) 
            throw new Error("Erro ao carregar dados.");

        const cao = await resposta.json();

        const fotoUrl = `https://localhost:7035/${cao.foto}`;
        document.getElementById("fotoPrincipal").src = fotoUrl;
        document.getElementById("fotoPrincipalLink").href = fotoUrl;
        document.getElementById("fotoPrincipalLink").dataset.title = cao.nome;

        const miniaturasDiv = document.getElementById("miniaturas");
        miniaturasDiv.innerHTML = `
            <a href="${fotoUrl}" data-lightbox="cao" data-title="${cao.nome}">
                <img src="${fotoUrl}" class="thumb rounded shadow-sm">
            </a>
        `;

        document.getElementById("nome").textContent = cao.nome;
        document.getElementById("info").textContent =
            `Cão | ${calcularIdade(cao.dataNascimento)} | Porte ${cao.porte}`;

        document.getElementById("descricao").textContent =
            cao.caracteristica ?? "Sem descrição disponível.";

    } catch (erro) {
        alert("Erro ao carregar o cão.");
        console.error(erro);
    }
}

function calcularIdade(dataNascimentoString) {
  const hoje = new Date();
  const nascimento = new Date(dataNascimentoString);

  // Calcula a diferença total em meses
  let totalMeses = (hoje.getFullYear() - nascimento.getFullYear()) * 12;
  totalMeses -= nascimento.getMonth();
  totalMeses += hoje.getMonth();


  if (hoje.getDate() < nascimento.getDate()) {
    totalMeses--;
  }

  if (totalMeses <= 0) {

    const diffTime = Math.abs(hoje - nascimento);
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    
    if (diffDays <= 30) {
         return diffDays === 1 ? "1 dia" : `${diffDays} dias`;
    }
    return "Menos de 1 mês";
  }

  const anos = Math.floor(totalMeses / 12);
  const meses = totalMeses % 12;

  let idadeFormatada = "";

  if (anos > 0) {
    idadeFormatada += `${anos} ${anos === 1 ? 'ano' : 'anos'}`;
  }

  if (meses > 0) {
    if (anos > 0) {
      idadeFormatada += " e ";
    }
    idadeFormatada += `${meses} ${meses === 1 ? 'mês' : 'meses'}`;
  }

  return idadeFormatada;
}
carregarCao();

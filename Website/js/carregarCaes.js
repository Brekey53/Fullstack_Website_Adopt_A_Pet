async function carregarAnimais() {
  try {
    const resposta = await fetch("http://localhost:5013/api/adotados");
    if (!resposta.ok) throw new Error("Erro ao carregar os dados dos animais");

    const animais = await resposta.json();
    const container = document.getElementById("adotados-container");
    const token = localStorage.getItem("token");

container.innerHTML = animais.map(animal => `
  <div class="col-12 col-sm-6 col-lg-4 mb-4">
    <div class="animal-card shadow-sm">
      <a href="adotados/${animal.id}.html">
        <img src="http://localhost:5013/${animal.foto}" alt="${animal.nome}" class="img-cao">
        <div class="animal-header d-flex justify-content-between align-items-center px-3 py-2">
          <h5 class="mb-0 text-white fw-bold">${animal.nome}</h5>
          <i class="fa-solid ${animal.sexo === 'M' ? 'fa-mars' : 'fa-venus'}"></i>
        </div>
        <div class="animal-body p-3">
          <p class="mb-1"> Cão | ${calcularIdade(animal.dataNascimento)}</p>
          <p class="mb-0">Porte ${animal.porte}</p>
        </div>
      </a>
      <div class="d-flex justify-content-center">
        ${token ? `<button class="btn btn-warning w-50 mt-2 d-flex justify-content-center text-white" onclick="editarCao(${animal.caoId})">Editar</button>` : ""}
      </div>
    </div>
  </div>
`).join("");
  } catch (erro) {
    console.error(erro);
    document.getElementById("adotados-container").innerHTML =
      "<p class='text-center text-danger'>Não foi possível carregar os animais.</p>";
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


carregarAnimais();

function editarCao(id) {
    window.location.href = `editarCaes.html?id=${id}`;
}
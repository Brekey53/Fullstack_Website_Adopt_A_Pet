async function carregarAnimais() {
  try {
    const resposta = await fetch("http://localhost:5013/api/caes");
    if (!resposta.ok) 
        throw new Error("Erro ao carregar os dados dos animais");

    const animais = await resposta.json();
    const container = document.getElementById("adotados-container");

    // Gera dinamicamente o HTML para cada animal
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
              <p class="mb-1">${animal.tipo} | ${animal.idade}</p>
              <p class="mb-0">Porte ${animal.porte}</p>
            </div>
          </a>
        </div>
      </div>
    `).join("");
  } catch (erro) {
    console.error(erro);
    document.getElementById("adotados-container").innerHTML =
      "<p class='text-center text-danger'>Não foi possível carregar os animais.</p>";
  }
}

carregarAnimais();

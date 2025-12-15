document.addEventListener("DOMContentLoaded", () => {
  carregarRacas();
  prepararEventoRaca();
});

function carregarRacas() {
  const select = document.getElementById("raca_select");

  fetch("https://localhost:7035/api/Racas")
    .then((res) => res.json())
    .then((racas) => {
      select.innerHTML = "";

      racas.forEach((r) => {
        const op = document.createElement("option");
        op.value = r.racaId;
        op.textContent = r.raca1;
        select.appendChild(op);
      });
    })
    .catch((err) => console.error("Erro ao carregar raças:", err));
}

function prepararEventoRaca() {
  const select = document.getElementById("raca_select");

  const container = document.createElement("div");
  container.id = "inputCruzamentoContainer";
  container.classList.add("mt-2");

  select.parentNode.appendChild(container);

  select.addEventListener("change", () => {
    if (parseInt(select.value) === 16) {
      container.innerHTML = `
                <label class="form-label mt-2">Cruzamento (opcional)</label>
                <input id="cruzamentoRaca" type="text" class="form-control" placeholder="Ex: Labrador">
            `;
    } else {
      container.innerHTML = "";
    }
  });
}

function guardar() {
  const cruzamentoInput = document.getElementById("cruzamentoRaca");

  const cao = {
    nome: document.getElementById("nome").value,
    dataNascimento: document.getElementById("dataNascimento").value,
    porte: document.getElementById("porte").value,
    sexo: document.getElementById("sexo").value,
    castrado: document.getElementById("castrado").checked,
    disponivel: document.getElementById("disponivel").checked,
    caracteristica: document.getElementById("caracteristica").value,
    foto: null,

    racaId: parseInt(document.getElementById("raca_select").value),
    cruzamentoRaca: cruzamentoInput ? cruzamentoInput.value : null,
  };

  fetch("https://localhost:7035/api/Caes", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("token"),
    },
    body: JSON.stringify(cao),
  })
    .then((res) => res.json())
    .then((data) => alert("Cão adicionado!"))
    .catch((err) => alert("Erro ao guardar."));

  const estavaDisponivel = document.getElementById("disponivel").checked;

  if (estavaDisponivel) {
    window.location.href = "paraAdotar.html";
  } else {
    window.location.href = "adotados.html";
  }
}

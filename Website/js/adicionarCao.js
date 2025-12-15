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

  const resultadoValidacao = validarDadosCao(cao);

  if (!resultadoValidacao.valido) {
      // Exibe os erros para o utilizador
      alert("Por favor, corrija os seguintes erros:\n- " + resultadoValidacao.erros.join("\n- "));
      return;
  }

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

function validarDadosCao(dados) {
    const erros = [];

    // 1. Validação do Nome
    if (!dados.nome || dados.nome.trim().length < 2) {
        erros.push("O nome do cão é obrigatório e deve ter pelo menos 2 letras.");
    }

    // 2. Validação da Data de Nascimento
    if (!dados.dataNascimento) {
        erros.push("A data de nascimento é obrigatória.");
    } else {
        const dataNasc = new Date(dados.dataNascimento);
        const hoje = new Date();
        if (isNaN(dataNasc.getTime())) {
            erros.push("Data de nascimento inválida.");
        } else if (dataNasc > hoje) {
            erros.push("A data de nascimento não pode ser no futuro.");
        }
    }

    // 3. Validação de Selects (Porte, Sexo, Raça)
    const portesValidos = ["Pequeno", "Médio", "Grande"]; // Ajusta conforme os teus values
    if (!portesValidos.includes(dados.porte)) {
        erros.push("Selecione um porte válido.");
    }

    if (dados.sexo !== "M" && dados.sexo !== "F") {
        erros.push("Selecione o sexo do animal.");
    }

    // 4. Validação da Raça (ID deve ser número positivo)
    if (!dados.racaId || isNaN(dados.racaId) || dados.racaId <= 0) {
        erros.push("Selecione uma raça válida.");
    }

    // 6. Característica
    if (dados.caracteristica && dados.caracteristica.length > 500) {
        erros.push("A característica é muito longa (máximo 500 caracteres).");
    }

    return {
        valido: erros.length === 0,
        erros: erros
    };
}
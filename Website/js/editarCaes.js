const id = new URLSearchParams(window.location.search).get("id");

async function carregarDados() {
  try {
    const resposta = await fetch(`http://localhost:5013/api/caes/${id}`, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    });

    if (!resposta.ok) {
      throw new Error("Erro ao carregar dados do cão.");
    }
    const cao = await resposta.json();
    document.getElementById("caoId").value = cao.caoId;
    document.getElementById("nome").value = cao.nome ?? "Micas";

    if (cao.dataNascimento) {
      document.getElementById("dataNascimento").value =
        cao.dataNascimento.split("T")[0];
    }

    document.getElementById("sexo").value = cao.sexo ?? "M";
    document.getElementById("porte").value = cao.porte ?? "Pequeno";
    document.getElementById("castrado").checked = cao.castrado;
    document.getElementById("disponivel").checked = cao.disponivel;
    document.getElementById("caracteristica").value =
      cao.caracteristica ?? "Fofo";

    if (cao.foto) {
      document.getElementById(
        "fotoPreview"
      ).src = `http://localhost:5013/${cao.foto}`;
    }
  } catch (erro) {
    console.error(erro);
  }
}

carregarDados();

async function guardar() {
  const dados = {
    caoId: Number(document.getElementById("caoId").value),
    nome: document.getElementById("nome").value,
    dataNascimento: document.getElementById("dataNascimento").value,
    porte: document.getElementById("porte").value,
    sexo: document.getElementById("sexo").value,
    castrado: document.getElementById("castrado").checked,
    disponivel: document.getElementById("disponivel").checked,
    caracteristica: document.getElementById("caracteristica").value,
  };

  const resposta = await fetch(
    `http://localhost:5013/api/caes/${dados.caoId}`,
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
      body: JSON.stringify(dados),
    }
  );

  if (!resposta.ok) {
    const erro = await resposta.text();
    console.error("Erro do backend:", erro);
    alert("Erro ao guardar alterações:\n" + erro);
    return;
  }

  if (document.referrer.includes("paraAdotar.html")) {
    window.location.href = "paraAdotar.html";
} else {
    // Caso contrário assume que veio dos adotados ou outro sitio
    window.location.href = "adotados.html";
}
  //window.location.back;
}

async function apagar() {
  const id = document.getElementById("caoId").value;

  if (!id) {
    alert("Erro: Nào foi possivel identificar o cão para apagar.");
    return;
  }

  const confirmacao = confirm(
    "Tem a certeza absoluta que deseja eliminar este cão da base de dados? Esta ação é irreversível!"
  );

  if (!confirmacao) {
    return; // O utilizador carregou cancelar
  }

  try {
    // Chamar a API no metodo Delete
    const resposta = await fetch(`http://localhost:5013/api/caes/${id}`, {
      method: "DELETE",
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    });
    // Verificar a resposta
    if (resposta.ok) {
      // Status 200 ou 204
      alert("Cão eliminado com sucesso.");

      // Redirecionar para uma página de lista (já que esta página de edição deixa de fazer sentido)
      const estavaDisponivel = document.getElementById("disponivel").checked;

      if (estavaDisponivel) {
        window.location.href = "paraAdotar.html";
      } else {
        window.location.href = "adotados.html";
      }
    } else {
      const erroMsg = await resposta.text();
      alert("Erro ao eliminar o cão: " + erroMsg);
    }
  } catch (erro) {
    console.error("Erro:", erro);
    alert("Ocorreu um erro de comunicação com o servidor.");
  }
}

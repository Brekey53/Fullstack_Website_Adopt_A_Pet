const form = document.querySelector(".form");
const nomeVoluntario = document.getElementById("nome_voluntario");
const emailVoluntario = document.getElementById("email_voluntario");
const tipoVisita = document.getElementById("tipo_visita");

form.addEventListener("submit", async (e) => {
  e.preventDefault();
  const dados = new FormData();
  dados.append("nome", nomeVoluntario.value);
  dados.append("email", emailVoluntario.value);
  dados.append("tipoVisita", tipoVisita.value);

  try {
    const resposta = await fetch(
      "http://localhost:5013/api/FormularioContacto/enviar",
      {
        method: "POST",
        body: dados,
      }
    );

    if (!resposta.ok) {
      throw new Error("Erro no envio");
    }

    const texto = await resposta.text();
    console.log("Sucesso:", texto); // TODO: podemos apagar, só queria testar
    alert("Email enviado com sucesso!");
  } catch (erro) {
    console.error("Erro:", erro);
    alert("Falha ao enviar email.");
  }
});

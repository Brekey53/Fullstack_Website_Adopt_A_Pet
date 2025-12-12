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
      "https://localhost:7035/api/FormularioContacto/enviar",
      {
        method: "POST",
        body: dados,
      }
    );

    if (!resposta.ok) {
      throw new Error("Erro no envio");
    }
    
    mostrarMensagem("Obrigado pelo seu contacto!", "Entraremos em contacto consigo brevemente 😊", "text-success");

  } catch (erro) {
    console.error("Erro:", erro);
    mostrarMensagem("Falha no envio 😢", "Tente novamente mais tarde.", "text-danger");
  }
});

function mostrarMensagem(tituloMsg, textoMsg, classeTexto) {
  // limpar formulário
  form.innerHTML = "";

  // criar container
  const div = document.createElement("div");
  div.classList.add("text-center", "p-4");

  // título
  const titulo = document.createElement("h2");
  titulo.classList.add(classeTexto);
  titulo.textContent = tituloMsg;

  // parágrafo
  const paragrafo = document.createElement("p");
  paragrafo.textContent = textoMsg;

  // adicionar tudo
  div.appendChild(titulo);
  div.appendChild(paragrafo);
  form.appendChild(div);
}

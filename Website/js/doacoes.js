document.addEventListener("DOMContentLoaded", function () {
  const btnDoarOsso = document.getElementById("btnDoarOsso");

  btnDoarOsso.addEventListener("click", async function (e) {
    e.preventDefault();

    const dados = { // alterar futuramente
      numeroCartao: "0000",
      valor: 2.5,
    };

    try {
      const resposta = await fetch("http://localhost:5013/api/doacoes", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(dados),
      });

      const result = await resposta.json();

      if (!resposta.ok) {
        alert("Erro: " + result);
        return;
      }
    } catch (erro) {
      console.error("Erro a contactar API:", erro); // TODO: apagar, é só para testar
    }
  });

  const btnDoarManta = document.getElementById("btnDoarManta");

  btnDoarManta.addEventListener("click", async function (e) {
    e.preventDefault();
    const dados = {
      // TODO: arranjar segunda pagina para testar dados do cartão ou um Modal ? 
      numeroCartao: "1234",
      valor: 4,
    };

    try {
      const resposta = await fetch("http://localhost:5013/api/doacoes", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(dados),
      });

      const result = await resposta.json();

      if (!resposta.ok) {
        alert("Erro: " + result);
        return;
      }

      alert(result.mensagem);
    } catch (erro) {
      console.error("Erro a contactar API:", erro);
    }
  });
});

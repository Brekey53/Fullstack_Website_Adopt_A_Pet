async function carregarDados() {
    const resposta = await fetch(`http://localhost:5013/api/caes/${id}`, {
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        }
    });

    const cao = await resposta.json();
}

carregarDados();

async function guardar() {
    const dados = {
        nome: document.getElementById("nome").value,
        idade: document.getElementById("idade").value
    };

    await fetch(`http://localhost:5013/api/caes/${id}`, {
        method: "PUT", // Patch ??
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        body: JSON.stringify(dados)
    });

    window.location.href = "../pages/adoptionPages/adotados.html";
}


const id = new URLSearchParams(window.location.search).get("id");

async function carregarDados() {
    try {

        const resposta = await fetch(`http://localhost:5013/api/caes/${id}`, {
            headers: {
                "Authorization": "Bearer " + localStorage.getItem("token")
            }
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
        document.getElementById("caracteristica").value = cao.caracteristica ?? "Fofo";

        if (cao.foto) {
            document.getElementById("fotoPreview").src = `http://localhost:5013/${cao.foto}`;
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
        caracteristica: document.getElementById("caracteristica").value
    };

    const resposta = await fetch(`http://localhost:5013/api/caes/${dados.caoId}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        body: JSON.stringify(dados)
    });

    if (!resposta.ok) {
        const erro = await resposta.text();
        console.error("Erro do backend:", erro);
        alert("Erro ao guardar alterações:\n" + erro);
        return;
    }

    window.location.href = "adotados.html";
}
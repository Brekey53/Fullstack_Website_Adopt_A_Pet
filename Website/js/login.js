const usernameInput = document.getElementById("username")
const passwordInput = document.getElementById("password")

async function sendCredentials() {
    const dataLogin = {
        username: usernameInput.value,
        password: passwordInput.value
    };

    const response = await fetch("http://localhost:5013/api/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(dataLogin)
    });

    if (!response.ok) {
        alert("Credenciais inválidas");
        return;
    }
    const result = await response.json();

    // grava o token JWT no localStorage
    localStorage.setItem("token", result.token);
    localStorage.setItem("username", usernameInput.value);
}
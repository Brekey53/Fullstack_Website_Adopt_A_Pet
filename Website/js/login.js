const usernameInput = document.getElementById("username");
const passwordInput = document.getElementById("password");

async function sendCredentials() {
  const dataLogin = {
    username: usernameInput.value,
    password: passwordInput.value,
  };

  const response = await fetch("http://localhost:5013/api/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(dataLogin),
  });

  if (!response.ok) {
    alert("Credenciais inválidas");
    return;
  }

  const result = await response.json();

  // grava o token
  localStorage.setItem("token", result.token);
  localStorage.setItem("username", usernameInput.value);

  alert("Login efectuado com sucesso!");

  updateLoginButton();
  window.history.back();
}

function logout() {
  localStorage.removeItem("token");
  localStorage.removeItem("username");
  window.location.reload();
}

const loginBtn = document.getElementById("loginBtn");

function updateLoginButton() {
  const token = localStorage.getItem("token");

  if (token) {
    loginBtn.textContent = "Sair";
    loginBtn.href = "#";
    loginBtn.onclick = logout;
  } else {
    loginBtn.textContent = "Login";
    loginBtn.onclick = null;
  }
}

updateLoginButton();

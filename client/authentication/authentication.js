let apiUrl = "http://localhost:5204";

let pageTheme = localStorage.getItem("theme");

let main = {}

document.addEventListener("DOMContentLoaded", function () {
  getAuthElements();
  applyTheme(pageTheme ?? "default");
})

function getAuthElements() {
  main.loginButton = document.getElementById("login");
  main.emailInput = document.getElementById("email");
  main.passwordInput = document.getElementById("password");
  main.errorMessage = document.getElementById("errorMessage");

  main.emailInput.addEventListener("keydown", e => {
    if (e.key === "Enter") main.passwordInput.focus();
  })
}

async function tryLogin() {
  let login = { email: main.emailInput.value, password: main.passwordInput.value }


  if (checkValidEmail(login.email)) {
    let token;
    let responseMessage;
    await fetch(`${apiUrl}/api/authentication/`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(login)
    }).then(response => { return response.json() })
      .then(data => {
        token = data.token;
        responseMessage = data.message;
      });

    if (token) {
      tokenData = parseToken(token);
      localStorage.setItem("login", JSON.stringify({ "name": tokenData.unique_name, "token": token }))
      window.location = "../../dashboard/dashboard.html";
    }
    else if (responseMessage == "incorrect email") {
      main.errorMessage.innerText = "Email incorreto";
    }
    else if (responseMessage == "incorrect password") {
      main.errorMessage.innerText = "Senha incorreta";
    }
  }
}

function checkValidEmail(email) {
  let regex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

  let validEmail = regex.test(email);

  if (!validEmail) main.errorMessage.innerText = "Insira um email válido";

  return validEmail;
}
document.addEventListener("DOMContentLoaded", function () {
    getSignupElements();
})

function getSignupElements() {
    main.nameInput = document.getElementById("name");

    main.nameInput.addEventListener("keydown", e => {
        if (e.key === "Enter") main.emailInput.focus();
    })
    main.passwordInput.addEventListener("keydown", e => {
        if (e.key === "Enter") trySignup();
    })
}

async function trySignup() {
    if (checkValidEmail(main.emailInput.value) && checkValidName(main.nameInput.value)) {
        let newUser = { name: encodeURIComponent(main.nameInput.value), email: main.emailInput.value, password: main.passwordInput.value }
        await fetch(`${apiUrl}/api/authentication/signup`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newUser)
        })
            .then(response => { return response.json() })
            .then(data => main.message = data.message)
        if (main.message == "email already in use")
            main.errorMessage.innerText = "Este email já está em uso";
        else
            window.location = "../login/login.html";
    }
}

function checkValidName(name) {
  regex = /^[^0-9!@#$%*+={}?<>()]*$/

  let validName = regex.test(name)

  if (!validName) main.errorMessage.innerText = "Insira um nome válido";

  return validName;
}
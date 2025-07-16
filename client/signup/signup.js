let apiUrl = "http://localhost:5204";

let pageTheme = localStorage.getItem("theme");

let html = {};

document.addEventListener("DOMContentLoaded", function () {
    getLoginElements();
    applyTheme(pageTheme ?? "default");
})

function getLoginElements() {
    html.loginButton = document.getElementById("login");
    html.nameInput = document.getElementById("name");
    html.emailInput = document.getElementById("email");
    html.passwordInput = document.getElementById("password");
    html.errorMessage = document.getElementById("errorMessage");

    html.nameInput.addEventListener("keydown", e => {
        if (e.key === "Enter") html.emailInput.focus();
    })
    html.emailInput.addEventListener("keydown", e => {
        if (e.key === "Enter") html.passwordInput.focus();
    })
    html.passwordInput.addEventListener("keydown", e => {
        if (e.key === "Enter") tryRegister();
    })
}

async function tryRegister() {
    let newUser = { name: html.nameInput.value, email: html.emailInput.value, password: html.passwordInput.value }
    await fetch(`${apiUrl}/api/authentication/signup`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newUser)
    })
    window.location = "../login/login.html";
}

function checkValidEmail(email) {
    let regex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

    let validEmail = regex.test(email);

    if (!validEmail) html.errorMessage.innerText = "Insira um email válido!";;

    return validEmail;
}

function applyTheme(theme = "default") {
    let themeColors = {
        default: {
            '--main-color': "white",
            '--second-color': "rgb(225, 225, 225)",
            '--font-color': "rgb(74, 74, 74)",
            '--highlight-color': "rgb(195, 195, 195)",
            '--border-color': "rgb(203, 203, 203)"
        },
        dark: {
            '--main-color': "rgb(24, 26, 27)",
            '--second-color': "rgb(44, 47, 49)",
            '--font-color': "rgb(210, 210, 210)",
            '--highlight-color': "rgb(70, 75, 78)",
            '--border-color': "rgb(60, 64, 66)"
        }
    };

    let themeVariables = [
        "--main-color",
        "--second-color",
        "--font-color",
        "--highlight-color",
        "--border-color"
    ]

    themeVariables.forEach(variable => {
        document.documentElement.style.setProperty(variable, themeColors[theme][variable]);
    });

    localStorage.setItem("theme", theme);
}
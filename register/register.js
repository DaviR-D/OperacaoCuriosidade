let html = {};

document.addEventListener("DOMContentLoaded", function () {
    getRegisterElements();
    //insertRegisterData(html);
})

function getRegisterElements() {

    html.statusCheck = document.getElementById("status");
    html.nameInput = document.getElementById("name");
    html.ageInput = document.getElementById("age");
    html.emailInput = document.getElementById("email");
    html.adressInput = document.getElementById("adress");
    html.otherInput = document.getElementById("other");
    html.interestsInput = document.getElementById("interests");
    html.feelingsInput = document.getElementById("feelings");
    html.valuesInput = document.getElementById("values");

}


//function insertRegisterData(html){}


function saveRegistration() {
    [...registerForm.elements].forEach(field => {
        field.style.borderColor = "black";
    });
    if (registerForm.checkValidity()) {
        localStorage.setItem(String(localStorage.length), JSON.stringify({
            name: html.nameInput.value,
            email: html.emailInput.value,
            status: html.statusCheck.checked ? "Ativo" : "Inativo",
            pending: true,
            date: new Date(),
            age: html.ageInput.value,
            adress: html.adressInput.value,
            other: html.otherInput.value,
            interests: html.interestsInput.value,
            feelings: html.feelingsInput.value,
            values: html.valuesInput.value
        }));
    }
    else {
        alert("Preencha todos os campos obrigatórios!");
        let emptyFields = [...registerForm.elements].filter(field => !field.checkValidity());
        emptyFields.forEach(field => {
            field.style.borderColor = "red";
        });
    }
}


function showRegisterWindow() {
    registerWindow.showModal();
}

function hideRegisterWindow() {
    registerWindow.close();
}
const usuarios = [];

document.addEventListener("DOMContentLoaded", function(){
    const loginButton = document.getElementById("login");
    const emailInput = document.getElementById("email");
    const passwordInput = document.getElementById("password");

    loginButton.addEventListener("click", function(){
        console.log(emailInput.value);
        if(emailInput.value && passwordInput.value)
            alert("Sucesso!");
        else
            alert("Preencha as credenciais!");
    })
})
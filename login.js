const usuario = {email:"Davi", password: "senha123"};

document.addEventListener("DOMContentLoaded", function(){
    const loginButton = document.getElementById("login");
    const emailInput = document.getElementById("email");
    const passwordInput = document.getElementById("password");

    loginButton.addEventListener("click", function(){
        let login = {email:emailInput.value, password:passwordInput.value}
        if(JSON.stringify(login) == JSON.stringify(usuario))
            alert("Sucesso!");
        else
            alert("Login errado!");
    })
})
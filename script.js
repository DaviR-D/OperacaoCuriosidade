let loggedUser = {name: "Davi Rodrigues"};

document.addEventListener("DOMContentLoaded", function(){
    let html = getPageElements();
    insertData(html);
})

function getPageElements(){
    let html = {};

    html.userDisplay = document.getElementById("userDisplay");

    html.registrations = document.getElementById("registrations");

    return html;
}


function insertData(html){

    html.userDisplay.innerText = loggedUser.name;

    html.registrations.innerHTML = renderRegistrationsHTML().join('');
}

function renderRegistrationsHTML(amount = 0){
    registrationList = ['<tr><th>Nome</th><th>Email</th><th>Status</th></tr>'];
    registrations.slice(amount).forEach(register => {
        registrationList.push(
            `<tr>
            <td>${register.name}</td>
            <td>${register.email}</td>
            <td>${register.status}</td>
            </tr>`
        )
    });

    return registrationList;
}


let registrations = [
    {name: "João", email: "joao@gmail.com", status: "Ativo", pending:true, date:"2025-05-04T09:27:53"},
    {name: "Maria", email: "maria@gmail.com", status: "Inativo", pending:false, date:"2025-01-06T14:03:12"},
    {name: "Carlos", email: "carlos@gmail.com", status: "Ativo", pending:true, date:"2025-02-08T11:23:45"},
    {name: "Rafael", email: "rafael@gmail.com", status: "Inativo", pending:false, date:"2025-03-19T04:57:12"},
    {name: "Fernanda", email: "fernanda@gmail.com", status: "Ativo", pending: true, date: "2025-06-01T08:15:30"},
    {name: "Bruno", email: "bruno@gmail.com", status: "Inativo", pending: false, date: "2025-04-12T19:42:10"},
    {name: "Luciana", email: "luciana@gmail.com", status: "Ativo", pending: false, date: "2025-05-22T13:09:55"},
    {name: "Eduardo", email: "eduardo@gmail.com", status: "Ativo", pending: true, date: "2025-03-03T07:21:18"},
    {name: "Paula", email: "paula@gmail.com", status: "Inativo", pending: true, date: "2025-02-27T16:33:47"},
    {name: "André", email: "andre@gmail.com", status: "Ativo", pending: false, date: "2025-04-05T10:12:40"},
    {name: "Camila", email: "camila@gmail.com", status: "Inativo", pending: true, date: "2025-01-18T22:44:07"},
    {name: "Thiago", email: "thiago@gmail.com", status: "Ativo", pending: false, date: "2025-03-09T09:57:33"},
    {name: "Juliana", email: "juliana@gmail.com", status: "Ativo", pending: true, date: "2025-05-11T17:03:26"},
    {name: "Fábio", email: "fabio@gmail.com", status: "Inativo", pending: false, date: "2025-06-02T06:28:55"},
    {name: "Tatiane", email: "tatiane@gmail.com", status: "Ativo", pending: false, date: "2025-02-15T15:14:11"},
    {name: "Marcelo", email: "marcelo@gmail.com", status: "Inativo", pending: true, date: "2025-04-29T21:19:38"},
    {name: "Vanessa", email: "vanessa@gmail.com", status: "Ativo", pending: true, date: "2025-03-25T12:07:59"},
    {name: "Igor", email: "igor@gmail.com", status: "Inativo", pending: false, date: "2025-01-30T03:51:02"},
    {name: "Renata", email: "renata@gmail.com", status: "Ativo", pending: false, date: "2025-05-17T14:39:00"}
];
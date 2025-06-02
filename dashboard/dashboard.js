
document.addEventListener("DOMContentLoaded", function(){
    let html = getPageElements();
    insertData(html);

})

function getPageElements(){
    let html = {};

    html.userDisplay = document.getElementById("userDisplay");

    html.total = document.getElementById("total");
    html.pending = document.getElementById("pending");
    html.lastMonth = document.getElementById("lastMonth");

    html.latestRegistrations = document.getElementById("latestRegistrations");

    return html;
}

function insertData(html){
    html.userDisplay.innerText = loggedUser.name;

    html.total.innerText = totalRegistrations;
    html.pending.innerText = pendingRegistrations;
    html.lastMonth.innerText = lastMonthRegistrations;

    html.latestRegistrations.innerHTML = renderLatestRegistrationsHTML().join('');
}

function renderLatestRegistrationsHTML(){
    registrationList = ['<tr><th>Nome</th><th>Email</th><th>Status</th></tr>'];
    registrations.slice(-3).forEach(register => {
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


let loggedUser = {name: "Davi RD"};

let registrations = [
    {name: "João", email: "joao@gmail.com", status: "Ativo"},
    {name: "Maria", email: "maria@gmail.com", status: "Inativo"},
    {name: "Carlos", email: "carlos@gmail.com", status: "Ativo"},
    {name: "Rafael", email: "rafael@gmail.com", status: "Inativo"}
];

let totalRegistrations = 100;
let pendingRegistrations = 10;
let lastMonthRegistrations = 10;
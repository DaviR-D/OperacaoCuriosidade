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

function renderLatestRegistrationsHTML(){
    registrationList = [];
    registrations.slice(-3).forEach(register => {
        registrationList.push(`<li>${register.name} ${register.email} ${register.status}</li>`)
    });

    return registrationList;
}

document.addEventListener("DOMContentLoaded", function(){
    const userDisplay = document.getElementById("userDisplay");

    const total = document.getElementById("total");
    const pending = document.getElementById("pending");
    const lastMonth = document.getElementById("lastMonth");

    const latestRegistrations = document.getElementById("latestRegistrations");
    
    userDisplay.innerText = loggedUser.name;

    total.innerHTML = totalRegistrations;
    pending.innerHTML = pendingRegistrations;
    lastMonth.innerHTML = lastMonthRegistrations;

    latestRegistrations.innerHTML = renderLatestRegistrationsHTML().join('');

})
let loggedUser = { name: "Davi Rodrigues" };

let registrations = [];

document.addEventListener("DOMContentLoaded", function () {
    getStorageRegistrations();

    let html = getPageElements();

    insertData(html);
})

function getPageElements() {
    let html = {};

    html.userDisplay = document.getElementById("userDisplay");

    html.registrations = document.getElementById("registrations");

    return html;
}


function insertData(html) {

    html.userDisplay.innerText = loggedUser.name;

    html.registrations.innerHTML = renderRegistrationsHTML().join('');
}

function renderRegistrationsHTML(amount = 0) {
    registrationList = ['<tr><th>Nome</th><th>Email</th><th>Status</th><th>Ações</th></tr>'];
    registrations.slice(amount).forEach(register => {
        let rowContent = `${register.name} ${register.email.split("@")[0]}`;

        if (rowContent.includes(search.value)) {
            registrationList.push(
                `<tr>
                    <td>${register.name}</td>
                    <td>${register.email}</td>
                    <td>${register.status}</td>
                    <td>
                    <button class="editButton" onclick="editRegistration(${register.key})">&#9998</button>
                    <button class="deleteButton" onclick="deleteRegistration(${register.key})">X</button>
                    </td>
                </tr>`
            );
        }

    });

    return registrationList;
}


function getStorageRegistrations() {
    for (let index = 0; index < localStorage.length; index++) {
        let key = localStorage.key(index);
        let item = JSON.parse(localStorage.getItem(key));
        item.key = key;
        registrations.push(item);
    }
}

function deleteRegistration(key) {
    localStorage.removeItem(key);
    registrations = [];
    getStorageRegistrations();
    updateTable();
}


function updateTable() {
    let table = document.getElementById("registrations");
    table.innerHTML = renderRegistrationsHTML().join('');
}

search.addEventListener("input", function () {
    updateTable();
});


let loggedUser = { name: "Davi Rodrigues" };

let html = {};

let registrations = [];

document.addEventListener("DOMContentLoaded", function () {
    loadLayout();

    getStorageRegistrations();

    getPageElements();

    insertData();
})

function getPageElements() {

    html.search = document.getElementById("search");

    html.userDisplay = document.getElementById("userDisplay");

    html.registrations = document.getElementById("registrations");

}

function insertData() {

    html.search?.addEventListener("input", function () {
        updateTable();
        addActions?.();
    });

    html.userDisplay.innerText = loggedUser.name;

    html.registrations.innerHTML = renderRegistrationsHTML().join('');
}

function renderRegistrationsHTML(amount = 0) {
    registrationList = ['<tr id="tableHeader"><th>Nome</th><th>Email</th><th>Status</th></tr>'];
    registrations.slice(amount).forEach(register => {
        let rowContent = `${register.name} ${register.email.split("@")[0]}`;

        if (rowContent.includes(html.search.value)) {
            registrationList.push(
                `<tr>
                    <td>${register.name}</td>
                    <td>${register.email}</td>
                    <td>${register.status}</td>
                    <td class="actions" style="display: none;">
                        <button class="editButton" onclick="editRegistration('${register.key}')">&#9998</button>
                        <button class="deleteButton" onclick="deleteRegistration('${register.key}')">X</button>
                    </td>
                </tr>`
            );
        }

    });

    return registrationList;
}

function loadHeader() {
    document.body.innerHTML +=
        `
    <header>
        <input type="text" placeholder="Pesquisar..." id="search">
        <div class="login">
            <span id="userDisplay"></span>
            <a href="../login/login.html" id="exit">SAIR</a>
        </div>
    </header>
    `;
}

function loadNav() {
    document.body.innerHTML +=
        `
    <nav>
        <p style="text-align: center;">Operação Curiosidade</p>
        <div class="navLinks">
            <p><a href="../dashboard/dashboard.html">Home</a></p>
            <p><a href="../register/register.html">Cadastro</a></p>
            <p><a href="../report/report.html">Relatórios</a></p>
        </div>
    </nav>
    `
}

function loadLayout() {
    loadHeader();
    loadNav();
}


function getStorageRegistrations() {
    for (let index = 0; index < localStorage.length; index++) {
        let key = localStorage.key(index);
        let item = JSON.parse(localStorage.getItem(key));
        item.key = key;
        registrations.push(item);
    }
}


function updateTable() {
    let table = document.getElementById("registrations");
    table.innerHTML = renderRegistrationsHTML().join('');
}




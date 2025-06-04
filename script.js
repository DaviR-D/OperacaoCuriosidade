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
    registrationList = ['<tr><th>Nome</th><th>Email</th><th>Status</th></tr>'];
    registrations.slice(amount).forEach(register => {
        let rowContent = `${register.name} ${register.email.split("@")[0]}`;

        if (rowContent.includes(search.value)) {
            registrationList.push(
                `<tr>
                    <td>${register.name}</td>
                    <td>${register.email}</td>
                    <td>${register.status}</td>
                </tr>`
            );
        }

    });

    return registrationList;
}

search.addEventListener("input", function () {
    let table = document.getElementById("registrations");
    table.innerHTML = renderRegistrationsHTML().join('');
});

function getStorageRegistrations() {
    for (let index = 0; index < localStorage.length; index++) {
        let key = localStorage.key(index);
        let item = JSON.parse(localStorage.getItem(key));
        item.key = key;
        registrations.push(item);
    }
}




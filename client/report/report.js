document.addEventListener("DOMContentLoaded", async function () {
    await updateTable();
    main.report = {};
    getReportElements();
    insertReportData();
});

function getReportElements() {
    main.report.tableTop = document.getElementById("tableTop");
}

function insertReportData() {
    main.report.tableTop.insertAdjacentHTML('beforeend',
        `
        <h1><strong>Lista de usuários</strong></h1>
        <button onclick="printTable()">IMPRIMIR</button>
        `
    )

    navLink = document.getElementById("reportNav")
    navLink.style.backgroundColor = "var(--highlight-color)";
}

async function printTable() {
    document.body.classList.add("blur");
    await updateTable(0, main.clientsLength);
    window.print();
    clientsCache = {};
    updateTable();
    document.body.classList.remove("blur");
}

function setTableSettings() {
    loadClientsTable();
}

async function openClient(id) {
    readClient(id);
}
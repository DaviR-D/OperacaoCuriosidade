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

function setTableSettings(){
    main.tableHeader = [`
        <tr id="tableHeader">
            <th class="column" onclick="sortTable('name')">Nome ${main.arrow.name ? main.arrow.name : ""}</th>
            <th class="column" onclick="sortTable('email')">Email ${main.arrow.email ? main.arrow.email : ""}</th>
            <th class="column" onclick="sortTable('status')">Status ${main.arrow.status ? main.arrow.status : ""}</th>
            <th class="column" onclick="sortTable('date')">Data ${main.arrow.date ? main.arrow.date : ""}</th>
        </tr>`
    ];
    main.tableContent = (register) => {
        return `
            <tr>
                <td>${register.name}</td>
                <td>${register.email}</td>
                <td><span style="border-radius:5px; padding:5px;" class=${register.status == "Ativo" ? "active" : "inactive"}>${register.status}</span></td>
                <td>${new Date(register.date).toLocaleDateString('pt-BR')}</td>
                <td class="actions" style="display: none;">
                    <button class="editButton material-symbols-outlined" onclick="editClient('${register.id}')">edit</button>
                    <button class="deleteButton material-symbols-outlined" onclick="showDeleteConfirmation('${register.id}')">delete</button>
                </td>
            </tr>
            `}
    main.getTablePage = getClients;
}
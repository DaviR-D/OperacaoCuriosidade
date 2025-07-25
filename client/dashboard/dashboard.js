document.addEventListener("DOMContentLoaded", async function () {
    await updateTable();
    let content = document.getElementById("mainContent")
    content.insertAdjacentHTML('afterbegin',
        `
        <div class="statsContainersWrapper">
            <article class="statsContainer">
                <p id="total" class="stats" style="color: rgb(79, 79, 255);"></p>
                <span class="statsTitle">Total de cadastros</span>
            </article>
            <article class="statsContainer" id="centerCard">
                <p id="lastMonth" class="stats" style="color: rgb(0, 181, 0);"></p>
                <span class="statsTitle">Cadastros no último mês</span>
            </article>
            <article class="statsContainer">
                <p id="pending" class="stats" style="color: rgb(255, 39, 39);"></p>
                <span class="statsTitle">Cadastros com pendência de revisão</span>
            </article>
        </div>
        `
    )
    main.dashboard = {};
    getDashboardElements();
    insertDashboardData();
})

function getDashboardElements() {
    main.dashboard.tableTop = document.getElementById("tableTop");
    main.dashboard.total = document.getElementById("total");
    main.dashboard.pending = document.getElementById("pending");
    main.dashboard.lastMonth = document.getElementById("lastMonth");
}

async function insertDashboardData() {
    if (main.clientsLength == 0) createDB(loggedUser.token);
    main.orderReverse = true;
    await sortTable("date");


    main.dashboard.tableTop.insertAdjacentHTML('beforeend',
        `
        <h1><strong>Últimos cadastros</strong></h1>
        `
    )

    main.dashboard.total.innerText = main.clientsLength;
    main.dashboard.pending.innerText = main.pendingClients;
    main.dashboard.lastMonth.innerText = main.lastMonthClients;

    navLink = document.getElementById("dashboardNav")
    navLink.style.backgroundColor = "var(--highlight-color)";
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
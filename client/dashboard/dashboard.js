document.addEventListener("DOMContentLoaded", async function () {
    main.lastMonthClients = await getStats("lastMonth");
    main.pendingClients = await getStats("pending");
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
    main.sortDescending = true;
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

function setTableSettings() {
    loadClientsTable();
}

async function openClient(id) {
    readClient(id);
}
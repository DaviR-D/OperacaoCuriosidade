let apiUrl = "http://localhost:5204";

let loggedUser = JSON.parse(localStorage.getItem("login"));

let main = {};

main.endpoint = "page"
main.params = ""

let tablePagesCache = {};

let pageTheme = localStorage.getItem("theme");

document.addEventListener("DOMContentLoaded", function () {
    loadLayout();
    applyTheme(pageTheme ?? "default");
})

function loadLayout() {
    loadHeader();
    loadNav();
    loadTable();
}

function loadHeader() {
    document.body.insertAdjacentHTML('beforeend',
        `
    <header>
        <input type="text" placeholder="Pesquisar..." id="search">
        <span id="searchResults"></span>
        <div class="login">
            <span id="themeIcon" class="material-symbols-outlined"></span>
            <span id="userDisplay"></span>
            <a href="../authentication/login/login.html" id="exit">SAIR</a>
        </div>
    </header>
    `);

    main.exit = document.getElementById("exit");

    main.search = document.getElementById("search");

    main.searchResults = document.getElementById("searchResults");

    main.userDisplay = document.getElementById("userDisplay");
    main.userDisplay.innerText = loggedUser.name;

    main.search.addEventListener("input", function () {
        tablePagesCache = {}
        main.arrow = {};
        main.tableOrder = "default"
        main.endpoint = main.search.value.length > 0 ? `page/search` : "page";
        main.params = main.search.value.length > 0 ? `query=${main.search.value.toLowerCase()}` : "";
        updateTable();
    });

    main.exit.addEventListener("click", function () {
        localStorage.removeItem("login");
    })

}

function loadNav() {
    document.body.insertAdjacentHTML('beforeend',
        `
    <nav>
        <p id="navIcon">OC</p>
        <p style="text-align: center;">Operação<br>Curiosidade</p>
        <div class="navLinks">
            <a id="dashboardNav" href="../dashboard/dashboard.html"><span class="material-symbols-outlined">home</span>Home</a>
            <a id="registerNav" href="../register/register.html"><span class="material-symbols-outlined">group_add</span>Cadastro</a>
            <a id="reportNav" href="../report/report.html"><span class="material-symbols-outlined">analytics</span>Relatórios</a>
            <a id="logsNav" href="../logs/logs.html"><span class="material-symbols-outlined">document_search</span>Logs</a>
        </div>
    </nav>
    `);

    main.themeIcon = document.getElementById("themeIcon");
    main.themeIcon.addEventListener("click", function () {
        if (pageTheme == "default") {
            applyTheme("dark")
        }
        else {
            applyTheme("default")
        }
    })
}

function loadTable() {
    let content = document.getElementById("mainContent")
    content.insertAdjacentHTML('beforeend',
        `
        <article id="tableContainer">
            <div id="tableTop"></div>
            <div id="tableWrapper">
                <table id="clients"></table>
            </div>
            <div class="tablePaging">
                <button id="previousButton" class="material-symbols-outlined">arrow_back</button>
                <span id="pageNumber"></span>
                <button id="nextButton" class="material-symbols-outlined">arrow_forward</button>
            </div>
        </article>
        `
    )
    main.clients = document.getElementById("clients");
    main.pageNumber = document.getElementById("pageNumber");
    main.nextButton = document.getElementById("nextButton");
    main.previousButton = document.getElementById("previousButton");
    main.tableOrder = "default";
    main.arrow = {};
    main.orderReverse = false;
}

async function loadTableContent() {
    main.tablePage.forEach(register => {
        main.tableHeader.push(main.tableContent(register));
    });

    if (search.value.length > 0) {
        searchResults.innerText = `${main.clientsLength} resultados`
    } else {
        searchResults.innerText = "";
    }

    main.clients.innerHTML = main.tableHeader.join('');
    main.addActions?.();
}

async function loadPaging(start = 0, increment = 10) {
    let length = main.clientsLength;

    let totalPages = Math.ceil(length / increment);
    let currentPage = Math.round(start / increment) + 1;

    main.pageNumber.innerText = `${currentPage}/${totalPages}`;

    tablePagesCache[currentPage] = main.tablePage;

    let nextPageStart = currentPage == totalPages ? start : (start + increment);
    let previousPageStart = currentPage == 1 ? 0 : (start - increment);

    main.nextButton.onclick = () => {
        if (tablePagesCache[currentPage + 1] != undefined) {
            setTableSettings();
            main.tablePage = tablePagesCache[currentPage + 1];
            loadTableContent();
            loadPaging(nextPageStart);
        }
        else if (currentPage < totalPages) return updateTable(nextPageStart);
        else return () => { };
    };
    main.previousButton.onclick = () => {
        if (currentPage == 1) return () => { };
        else if (tablePagesCache[currentPage - 1] != undefined) {
            setTableSettings();
            main.tablePage = tablePagesCache[currentPage - 1];
            loadTableContent();
            loadPaging(previousPageStart);
        }
        else if (currentPage > 1) return updateTable(previousPageStart);
    };
}

function sortTable(order = "default") {
    tablePagesCache = {};
    main.arrow = {};
    main.arrow[order] = "";

    if (order == main.tableOrder) main.orderReverse = !main.orderReverse;

    main.arrow[order] = main.orderReverse ? "↓" : "↑";

    main.tableOrder = order;
    main.endpoint = "page/sorted";
    main.params = `sortKey=${order}&descending=${main.orderReverse}`;
    updateTable();
}

async function getClients(start = 0, increment = 10) {
    await fetch(`${apiUrl}/api/client/${main.endpoint}?start=${start}&increment=${increment}&${main.params}`, {
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`,
            "Content-Type": "application/json"
        },
    })
        .then(response => {
            if (response.status == 401) {
                localStorage.removeItem("login");
                window.location = "../authentication/login/login.html";
            }
            return response.json()
        })
        .then(data => {
            main.tablePage = data.page;
            main.clientsLength = data.resultsLength;
        });

    if (main.endpoint != "page/search")
        await getStats();
}

async function getStats() {
    await fetch(`${apiUrl}/api/client/stats`, {
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`,
            "Content-Type": "application/json"
        },
    })
        .then(response => {
            if (response.status == 401) {
                localStorage.removeItem("login");
                window.location = "../authentication/login/login.html";
            }
            return response.json()
        })
        .then(data => {
            main.clientsLength = data.clientsLength;
            main.lastMonthClients = data.lastMonthClients;
            main.pendingClients = data.pendingClients;
        });
}

async function updateTable(start = 0, increment = 10) {
    setTableSettings();
    await main.getTablePage(start, increment);
    loadPaging(start, increment);
    loadTableContent();
}

async function loadClientsTable() {
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
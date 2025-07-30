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
    loadModals();
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
        searchTable();
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
    main.currentSortKey = "default";
    main.sortByColumnArrow = {};
    main.sortDescending = false;
}

async function loadTableContent() {
    main.tablePage.forEach(row => {
        main.renderedTableContent.push(main.renderTableContent(row));
    });

    if (search.value.length > 0) {
        searchResults.innerText = `${main.clientsLength} resultados`
    } else {
        searchResults.innerText = "";
    }

    main.clients.innerHTML = main.renderedTableContent.join('');
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
            loadCachedPage(currentPage + 1, nextPageStart)
        }
        else if (currentPage < totalPages) return updateTable(nextPageStart);
        else return () => { };
    };
    main.previousButton.onclick = () => {
        if (currentPage == 1) return () => { };
        else if (tablePagesCache[currentPage - 1] != undefined) {
            loadCachedPage(currentPage - 1, previousPageStart)
        }
        else if (currentPage > 1) return updateTable(previousPageStart);
    };
}

function sortTable(sortKey = "default") {
    tablePagesCache = {};
    main.sortByColumnArrow = {};
    main.sortByColumnArrow[sortKey] = "";

    if (sortKey == main.currentSortKey) main.sortDescending = !main.sortDescending;

    main.sortByColumnArrow[sortKey] = main.sortDescending ? "↓" : "↑";

    main.currentSortKey = sortKey;
    main.endpoint = "page/sorted";
    main.params = `sortKey=${sortKey}&descending=${main.sortDescending}`;
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
        main.clientsLength = await getStats("length");
}

async function getStats(stat) {
    let length;
    await fetch(`${apiUrl}/api/client/${stat}`, {
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
            length = data.length;
        });
    return length;
}

async function updateTable(start = 0, increment = 10) {
    setTableSettings();
    await main.getTablePage(start, increment);
    loadPaging(start, increment);
    loadTableContent();
}

function loadCachedPage(page, pageStart) {
    setTableSettings();
    main.tablePage = tablePagesCache[page];
    loadTableContent();
    loadPaging(pageStart);
}

function searchTable() {
    tablePagesCache = {}
    main.sortByColumnArrow = {};
    main.currentSortKey = "default"
    main.endpoint = main.search.value.length > 0 ? `page/search` : "page";
    main.params = main.search.value.length > 0 ? `query=${main.search.value.toLowerCase()}` : "";
    updateTable();
}

async function loadClientsTable() {
    main.renderedTableContent = [`
        <tr id="tableHeader">
            <th class="column" onclick="sortTable('name')">Nome ${main.sortByColumnArrow.name ? main.sortByColumnArrow.name : ""}</th>
            <th class="column" onclick="sortTable('email')">Email ${main.sortByColumnArrow.email ? main.sortByColumnArrow.email : ""}</th>
            <th class="column" onclick="sortTable('status')">Status ${main.sortByColumnArrow.status ? main.sortByColumnArrow.status : ""}</th>
            <th class="column" onclick="sortTable('date')">Data ${main.sortByColumnArrow.date ? main.sortByColumnArrow.date : ""}</th>
        </tr>`
    ];
    main.renderTableContent = (register) => {
        return `
            <tr>
                <td onclick="openClient('${register.id}')">${register.name}</td>
                <td onclick="openClient('${register.id}')">${register.email}</td>
                <td onclick="openClient('${register.id}')"><span style="border-radius:5px; padding:5px;" class=${register.status == "Ativo" ? "active" : "inactive"}>${register.status}</span></td>
                <td onclick="openClient('${register.id}')">${new Date(register.date).toLocaleDateString('pt-BR')}</td>
                <td class="actions" style="display: none;">
                    <button class="editButton material-symbols-outlined" onclick="lockClient('${register.id}')">edit</button>
                    <button class="deleteButton material-symbols-outlined" onclick="showDeleteConfirmation('${register.id}')">delete</button>
                </td>
            </tr>
            `}
    main.getTablePage = getClients;
}

function loadModals() {
    document.body.insertAdjacentHTML('beforeend',
        `
<dialog id="clientViewModal">
    <div id="clientViewWrapper">
        <div class="modalTop">
            <span id="closeButtonView" class="material-symbols-outlined" onclick="main.clientViewModal.close()">close</span>
        </div>
        <div>
            <h2 class="onionLayer">
                <strong>1° Fatos e dados</strong>
                <span class="activeCheck">Ativo<input type="checkbox" id="statusView"></span>
            </h2>
            <div style="display: flex;">
                <div class="clientViewSection" style="width: 70%;">
                    <div class="inputTitle">Nome</div>
                    <input type="text" id="nameView">
                </div>
                <div class="clientViewSection" style="width: 20%; margin-left: 5%;">
                    <div class="inputTitle">Idade</div>
                    <input type="number" id="ageView" style="min-width: 10px;">
                </div>
            </div>
            <div class="clientViewSection">
                <div class="inputTitle">Email</div>
                <input type="text" id="emailView">
            </div>
            <div class="clientViewSection">
                <div class="inputTitle">Endereço</div>
                <input type="text" id="addressView">
            </div>
            <div class="clientViewSection">
                <div class="inputTitle">Outras informações</div>
                <input type="text" id="otherView">
            </div>
        </div>
        <div>
            <div class="clientViewSection">
                <h2 class="onionLayer"><strong>2° Interesses</strong></h2>
                <textarea id="interestsView"></textarea>
            </div>
            <div class="clientViewSection">
                <h2 class="onionLayer"><strong>3° Sentimentos</strong></h2>
                <textarea id="feelingsView"></textarea>
            </div>
            <div class="clientViewSection">
                <h2 class="onionLayer"><strong>4° Valores</strong></h2>
                <textarea id="valuesView"></textarea>
            </div>
        </div>
    </div>
</dialog>
<dialog id="alertModal">
    <div id="alertWrapper">
        <p id="alertTitle"></p>
        <span id="alertText">Essa ação não pode ser desfeita</span>
        <div id="alertButtons">
            <button id="cancelDeleteButton" onclick="hideAlertModal()">CANCELAR</button>
            <button id="alertDeleteButton">DELETAR</button>
        </div>
    </div>
</dialog>        
    `);

    main.clientViewModal = document.getElementById("clientViewModal");

    main.statusCheck = document.getElementById("statusView");
    main.nameInput = document.getElementById("nameView");
    main.ageInput = document.getElementById("ageView");
    main.emailInput = document.getElementById("emailView");
    main.addressInput = document.getElementById("addressView");
    main.otherInput = document.getElementById("otherView");
    main.interestsInput = document.getElementById("interestsView");
    main.feelingsInput = document.getElementById("feelingsView");
    main.valuesInput = document.getElementById("valuesView");

    let inputs = main.clientViewModal.querySelectorAll("input");
    inputs.forEach(input => input.disabled = true);
    let textareas = main.clientViewModal.querySelectorAll("textarea");
    textareas.forEach(textarea => textarea.disabled = true);

    main.clientViewModal.addEventListener("close", function () {
        document.body.classList.remove("blur");
    });

    main.alertModal = document.getElementById("alertModal");
    main.alertTitle = document.getElementById("alertTitle");
    main.alertText = document.getElementById("alertText");
    main.alertDeleteButton = document.getElementById("alertDeleteButton");
    main.closeAlertButton = document.getElementById("cancelDeleteButton");

    main.alertModal.addEventListener("close", function () {
        document.body.classList.remove("blur");
    });
}

function hideAlertModal() {
    main.alertModal.close();
}

async function readClient(id) {
    let viewItem;
    let responseMessage;

    await fetch(`${apiUrl}/api/client/${id}`, {
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`,
        }
    })
        .then(response => { return response.json() })
        .then(data => {
            viewItem = data.client;
            responseMessage = data.message;
        });

    if (responseMessage == "client does not exist") {
        clientNotFoundAlert();
        return;
    }

    main.clientViewModal.dataset.userId = id;
    showClientViewModal();

    main.nameInput.value = viewItem.name;
    main.emailInput.value = viewItem.email;
    main.ageInput.value = viewItem.age;
    main.addressInput.value = viewItem.address;
    main.otherInput.value = viewItem.other;
    main.interestsInput.value = viewItem.interests;
    main.feelingsInput.value = viewItem.feelings;
    main.valuesInput.value = viewItem.values;
    main.statusCheck.checked = viewItem.status == "Ativo" ? true : false;
    registerLog("Read", id)
}

function showClientViewModal() {
    document.body.classList.add("blur");
    main.clientViewModal.showModal();
}

async function clientNotFoundAlert() {
    main.alertTitle.innerText = `Cliente não encontrado`;
    main.alertText.innerText = "Este cliente não existe ou foi deletado";
    document.body.classList.add("blur");
    main.alertModal.showModal();
    main.alertDeleteButton.style.display = "none";
    main.closeAlertButton.innerText = "OK";
    main.closeAlertButton.onclick = () => {
        hideAlertModal();
    };
    await updateTable();
}

async function registerLog(userAction, id) {
    fetch(`${apiUrl}/api/log/`, {
        method: "POST",
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`,
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ clientId: id, action: userAction })
    })
}
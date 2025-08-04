let logs = []
let logsCache = {};
let dictionary = {
    "Create": "Criação",
    "Delete": "Deleção",
    "Update": "Edição",
    "Read": "Leitura"
}

document.addEventListener("DOMContentLoaded", async function () {
    await updateTable();
    main.logs = {};
    getLogsElements();
    insertLogsData();
    main.search.disabled = true;
});

function getLogsElements() {
    main.logs.tableTop = document.getElementById("tableTop");
    main.logs.table = document.getElementById("clients");
}

function insertLogsData() {
    main.logs.tableTop.insertAdjacentHTML('beforeend',
        `
        <h1><strong>Logs</strong></h1>
        `
    )

    navLink = document.getElementById("logsNav")
    navLink.style.backgroundColor = "var(--highlight-color)";
}

async function getLogs(start, increment) {
    await fetch(`${apiUrl}/api/log?start=${start}&increment=${increment}`, {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`,
            "Content-Type": "application/json"
        }
    })
        .then(response => {
            if (response.status == 401) {
                localStorage.removeItem("login");
                window.location = "../authentication/login/login.html";
            }
            return response.json()
        })
        .then(data => {
            main.tablePage = data.logs;
            main.clientsLength = data.logsLength;
        })
}

function setTableSettings() {
    main.renderedTableContent = [`
        <tr id="tableHeader">
            <th class="column">Usuário</th>
            <th class="column">Cliente</th>
            <th class="column">Ação</th>
            <th class="column">Momento</th>
        </tr>`
    ];
    main.renderTableContent = (log) => {
        return `
            <tr>
                <td>${log.userEmail}</td>
                <td>${log.clientEmail}</td>
                <td>${dictionary[log.action]}</td>
                <td>
            ${new Date(log.timeStamp).toLocaleDateString('pt-BR',
            {
                year: 'numeric',
                month: '2-digit',
                day: '2-digit',
                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit',
                hour12: false
            })}
                </td>
            </tr>
            `}
    main.getTablePage = getLogs;
}
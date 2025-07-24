let logs = []
let logsCache = {};
let dictionary = {
    "Delete": "Deleção",
    "Edit": "Edição",
    "Read": "Leitura"
}

document.addEventListener("DOMContentLoaded", async function () {
    html.logs = {};
    getLogsElements();
    insertLogsData();
    updateLogsTable();
    html.search.disabled = true;
});

function getLogsElements() {
    html.logs.tableTop = document.getElementById("tableTop");
    html.logs.table = document.getElementById("clients");
}

function insertLogsData() {
    html.logs.tableTop.insertAdjacentHTML('beforeend',
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
            logs = data.logs;
            html.logsLength = data.logsLength;
        })
}

function renderLogs() {
    let renderedLogs =
        [`
        <tr id="tableHeader">
            <th class="column">Usuário</th>
            <th class="column">Cliente</th>
            <th class="column">Ação</th>
            <th class="column">Momento</th>
        </tr>`
        ];

    logs.forEach(log => {
        renderedLogs.push(
            `
            <tr>
                <td>${log.userEmail}</td>
                <td>${log.clientEmail}</td>
                <td>${dictionary[log.action]}</td>
                <td>${new Date(log.timeStamp).toLocaleDateString('pt-BR', {
                year: 'numeric',
                month: '2-digit',
                day: '2-digit',
                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit',
                hour12: false
            })}</td>
            </tr>
            `
        );
    });

    html.logs.table.innerHTML = renderedLogs.join('');
}

async function loadPaging(start = 0, increment = 10) {
    let length = html.logsLength;

    let totalPages = Math.ceil(length / increment);
    let currentPage = Math.round(start / increment) + 1;

    html.pageNumber.innerText = `${currentPage}/${totalPages}`;

    logsCache[currentPage] = logs;

    let nextPageStart = currentPage == totalPages ? start : (start + increment);
    let previousPageStart = currentPage == 1 ? 0 : (start - increment);

    html.nextButton.onclick = () => {
        if (logsCache[currentPage + 1] != undefined) {
            logs = logsCache[currentPage + 1];
            renderLogs();
            loadPaging(nextPageStart);
        }
        else if (currentPage < totalPages) return updateLogsTable(nextPageStart);
        else return () => { };
    };
    html.previousButton.onclick = () => {
        if (currentPage == 1) return () => { };
        else if (logsCache[currentPage - 1] != undefined) {
            logs = logsCache[currentPage - 1];
            renderLogs();
            loadPaging(previousPageStart);
        }
        else if (currentPage > 1) return updateLogsTable(previousPageStart);
    };
}

async function updateLogsTable(start = 0, increment = 10) {
    await getLogs(start, increment);
    loadPaging(start, increment);
    renderLogs();
}
let loggedUser = JSON.parse(localStorage.getItem("login"));

let html = {};

let registrations = [];

let pageTheme = localStorage.getItem("theme");

document.addEventListener("DOMContentLoaded", function () {
    loadLayout();
    applyTheme(pageTheme ?? "default");
})

function loadLayout() {
    loadHeader();
    loadNav();
    loadTable();
    loadTableContent();
}

function loadHeader() {
    document.body.insertAdjacentHTML('beforeend',
        `
    <header>
        <input type="text" placeholder="Pesquisar..." id="search">
        <div class="login">
            <span id="userDisplay"></span>
            <a href="../login/login.html" id="exit">SAIR</a>
        </div>
    </header>
    `);

    html.exit = document.getElementById("exit");

    html.search = document.getElementById("search");

    html.userDisplay = document.getElementById("userDisplay");
    html.userDisplay.innerText = loggedUser.name;

    html.search.addEventListener("input", function () {
        loadTableContent();
    });

    html.exit.addEventListener("click", function () {
        localStorage.removeItem("login")
    })

}

function loadNav() {
    document.body.insertAdjacentHTML('beforeend',
        `
    <nav>
        <p style="text-align: center;">Operação Curiosidade</p>
        <div class="navLinks">
            <p><a id="dashboardNav" href="../dashboard/dashboard.html"> ⌂ Home</a></p>
            <p><a id="registerNav" href="../register/register.html"> 🗄 Cadastro</a></p>
            <p><a id="reportNav" href="../report/report.html"> 🗒 Relatórios</a></p>
        </div>
        <input type="checkbox" id="themeToggle">Tema Escuro</div>
    </nav>
    `);

    let themeToggle = document.getElementById("themeToggle");

    themeToggle.checked = (pageTheme == "dark" ? true : false);

    themeToggle.addEventListener("change", function () {
        if (themeToggle.checked) {
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
            <div id="tableWraper">
                <table id="registrations"></table>
            </div>
        </article>
        `
    )

    html.tableOrder = "";
}

function loadTableContent(order = "default") {
    let sortBy = {
        "name": (registrations) => registrations.sort((a, b) => a.name.localeCompare(b.name)),
        "email": (registrations) => registrations.sort((a, b) => a.email.localeCompare(b.email)),
        "status": (registrations) => registrations.sort((a, b) => a.status.localeCompare(b.status)),
        "date": (registrations) => registrations.sort((a, b) => new Date(b.date) - new Date(a.date)),
        "default": (registrations) => registrations,
    }

    if (order == "default") {
        registrations = [];
        getStorageRegistrations();
    } else if (order == html.tableOrder) {
        registrations.reverse();
    } else {
        sortBy[order](registrations);
    }

    html.tableOrder = order;

    html.registrations = document.getElementById("registrations");

    registrationList = [`
        <tr id="tableHeader">
            <th class="column" onclick="loadTableContent('name')">Nome</th>
            <th class="column" onclick="loadTableContent('email')">Email</th>
            <th class="column" onclick="loadTableContent('status')">Status</th>
            <th class="column" onclick="loadTableContent('date')">Data</th>
        </tr>
        `];
    registrations.forEach(register => {
        let rowContent = `${register.name} ${register.email.split("@")[0]}`;

        if (rowContent.includes(html.search.value)) {
            registrationList.push(
                `<tr>
                    <td>${register.name}</td>
                    <td>${register.email}</td>
                    <td style="color:${register.status == "Ativo" ? "rgb(52, 255, 52)" : "rgb(255, 39, 39)"};">${register.status}</td>
                     <td>${register.date}</td>
                    <td class="actions" style="display: none;">
                        <button class="editButton" onclick="editRegistration('${register.key}')">&#9998</button>
                        <button class="deleteButton" onclick="deleteRegistration('${register.key}')">X</button>
                    </td>
                </tr>`
            );
        }

    });

    html.registrations.innerHTML = registrationList.join('');
    html.addActions?.();
}

function getStorageRegistrations() {
    for (let index = 0; index < localStorage.length; index++) {
        let key = localStorage.key(index);
        if (key == "login" || key == "theme") continue;
        let item = JSON.parse(localStorage.getItem(key));
        item.key = key;
        registrations.push(item);
    }
}

function applyTheme(theme = "default") {
    let themeColors = {
        default: {
            '--main-color': "white",
            '--second-color': "rgb(225, 225, 225)",
            '--font-color': "rgb(74, 74, 74)",
            '--hover-color': "rgb(195, 195, 195)",
            '--border-color': "rgb(203, 203, 203)"
        },
        dark: {
            '--main-color': "rgb(24, 26, 27)",
            '--second-color': "rgb(44, 47, 49)",
            '--font-color': "rgb(210, 210, 210)",
            '--hover-color': "rgb(70, 75, 78)",
            '--border-color': "rgb(60, 64, 66)"
        }
    };

    let themeVariables = [
        "--main-color",
        "--second-color",
        "--font-color",
        "--hover-color",
        "--border-color"
    ]

    themeVariables.forEach(variable => {
        document.documentElement.style.setProperty(variable, themeColors[theme][variable])
    });

    localStorage.setItem("theme", theme)
}





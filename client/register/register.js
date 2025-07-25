document.addEventListener("DOMContentLoaded", async function () {
    await updateTable();
    main.register = {};
    getRegisterElements();
    insertRegisterData();
    try { main.addActions(); }
    catch (error) { if (!(error instanceof ReferenceError)) throw error; }
})

function getRegisterElements() {
    main.register.tableTop = document.getElementById("tableTop");
    let tableHeader = document.getElementById("tableHeader");

    main.register.registerModal = document.getElementById("registerModal");
    main.register.alertModal = document.getElementById("alertModal");

    main.register.statusCheck = document.getElementById("status");
    main.register.nameInput = document.getElementById("name");
    main.register.ageInput = document.getElementById("age");
    main.register.emailInput = document.getElementById("email");
    main.register.addressInput = document.getElementById("address");
    main.register.otherInput = document.getElementById("other");
    main.register.interestsInput = document.getElementById("interests");
    main.register.feelingsInput = document.getElementById("feelings");
    main.register.valuesInput = document.getElementById("values");

    main.register.alertTitle = document.getElementById("alertTitle");
    main.register.alertDeleteButton = document.getElementById("alertDeleteButton");
    main.register.cancelDeleteButton = document.getElementById("cancelDeleteButton");

    main.register.alertModal.addEventListener("close", function () {
        document.body.classList.remove("blur");
    });

    main.register.registerModal.addEventListener("close", function () {
        document.body.classList.remove("blur");
        clearFields();
        resetFieldsStyle();
    });

    addInputEvents();
}

function insertRegisterData() {
    main.register.tableTop.insertAdjacentHTML('beforeend',
        `
            <h1><strong>Cadastros</strong></h1>
            <button onclick="showRegisterModal()"> + NOVO CADASTRO</button>
        `
    )

    navLink = document.getElementById("registerNav")
    navLink.style.backgroundColor = "var(--highlight-color)";

    main.addActions = () => {
        tableHeader.insertAdjacentHTML("beforeend", "<th style='cursor: default;'>Ações</th>");
        document.querySelectorAll(".actions").forEach(row => {
            row.style.display = "table-cell";
        })
    }
}

async function saveClient(event, id = null) {
    event.preventDefault();
    resetFieldsStyle();

    if (registerForm.checkValidity()) {
        let newRegister = {
            id: id,
            name: main.register.nameInput.value,
            email: main.register.emailInput.value,
            status: main.register.statusCheck.checked ? "Ativo" : "Inativo",
            pending: true,
            date: id ? main.tablePage.filter((register) => register.id == id)[0].date : new Date(),
            age: main.register.ageInput.value,
            address: main.register.addressInput.value,
            other: main.register.otherInput.value,
            interests: main.register.interestsInput.value,
            feelings: main.register.feelingsInput.value,
            values: main.register.valuesInput.value,
        };

        if (await checkFieldsValidity(id, newRegister)) {
            httpMethod = id == null ? "POST" : "PUT";

            fetch(`${apiUrl}/api/client/`, {
                method: httpMethod,
                headers: {
                    "Authorization": `Bearer ${loggedUser.token}`,
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(newRegister)
            })
            registerForm.submit();
            if (httpMethod == "PUT")
                registerLog("Edit", id);
        }
    }
    else {
        highlightBlankFields();
    }
}

async function deleteClient(id) {
    await fetch(`${apiUrl}/api/client/${id}`, {
        method: 'DELETE',
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`
        }
    })
    clientsCache = {};
    updateTable();
    hideDeleteConfirmation();
    registerLog("Delete", id);
}

async function editClient(id) {
    let editItem;

    await fetch(`${apiUrl}/api/client/${id}`, {
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`,
        }
    })
        .then(response => { return response.json() })
        .then(data => {
            editItem = data.client;
        });

    registerModal.dataset.userId = id;
    showRegisterModal();

    main.register.nameInput.value = editItem.name;
    main.register.emailInput.value = editItem.email;
    main.register.ageInput.value = editItem.age;
    main.register.addressInput.value = editItem.address;
    main.register.otherInput.value = editItem.other;
    main.register.interestsInput.value = editItem.interests;
    main.register.feelingsInput.value = editItem.feelings;
    main.register.valuesInput.value = editItem.values;
    main.register.statusCheck.checked = editItem.status == "Ativo" ? true : false;
    registerLog("Read", id)
}

function showRegisterModal() {
    document.body.classList.add("blur");
    registerModal.showModal();
}

function hideRegisterModal() {
    registerModal.close();
}

function showDeleteConfirmation(id) {
    let deletedUser = main.tablePage.filter((register) => register.id == id)[0].name
    main.register.alertTitle.innerText = `Você tem certeza que deseja deletar ${deletedUser}?`;
    document.body.classList.add("blur");
    main.register.alertModal.showModal();
    main.register.alertDeleteButton.onclick = () => deleteClient(id);
}

function hideDeleteConfirmation() {
    main.register.alertModal.close();
}

function clearFields() {
    registerModal.dataset.userId = null;
    main.register.nameInput.value = "";
    main.register.emailInput.value = "";
    main.register.ageInput.value = "";
    main.register.addressInput.value = "";
    main.register.otherInput.value = "";
    main.register.interestsInput.value = "";
    main.register.feelingsInput.value = "";
    main.register.valuesInput.value = "";
    main.register.statusCheck.checked = false;
}

function resetFieldStyle(field) {
    let errorMessage;
    field.style.borderColor = "black";
    errorMessage = document.getElementById(`${field.id}Error`) ?? {};
    errorMessage.innerText = "";
}

function resetFieldsStyle() {
    let errorMessage;
    [...registerForm.elements].forEach(field => {
        resetFieldStyle(field);
    });
}

function highlightInvalidField(field, message) {
    let errorMessage;
    field.style.borderColor = "red";
    errorMessage = document.getElementById(`${field.id}Error`) ?? {};
    errorMessage.innerText = message;
}

function highlightBlankFields() {
    let emptyFields = [...registerForm.elements].filter(field => !field.checkValidity());
    emptyFields.forEach(field => {
        highlightInvalidField(field, "Campo obrigatório")
    });

    emptyFields[0].scrollIntoView({ behavior: "smooth", block: "center" });
}

async function checkFieldsValidity(id, newRegister) {
    main.invalidFields = [];
    checkValidName(newRegister.name);
    checkValidEmail(newRegister.email);
    await checkExistingEmail(id, newRegister.email);

    if (main.invalidFields.length) {
        main.invalidFields[0].scrollIntoView({ behavior: "smooth", block: "center" });
        return false;
    }
    return true;
}

async function checkExistingEmail(id = null, email) {
    let availableEmail;
    let idParam = String(id) == "null" ? "" : `id=${id}&`;

    await fetch(`${apiUrl}/api/client/checkEmail?${idParam}email=${email}`, {
        headers: {
            "Authorization": `Bearer ${loggedUser.token}`,
        }
    })
        .then(response => { return response.json() })
        .then(data => { availableEmail = data.isAvailable; });

    if (!availableEmail) {
        highlightInvalidField(main.register.emailInput, "Email já cadastrado");
        main.invalidFields.push(main.register.emailInput);
    };
    return availableEmail;
}

function checkValidEmail(email) {
    regex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    let valid = regex.test(email)
    if (!valid) {
        highlightInvalidField(main.register.emailInput, "Insira um email válido");
        main.invalidFields.push(main.register.emailInput);
    };
    return valid;
}

function checkValidName(name) {
    regex = /^[^0-9!@#$%*+={}?<>()]*$/
    let valid = regex.test(name)
    if (!valid) {
        highlightInvalidField(main.register.nameInput, "Insira um nome válido")
        main.invalidFields.push(main.register.nameInput);
    };
    return valid;
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

function addInputEvents() {
    main.invalidFields = [];
    [...registerForm.elements].forEach(field => {
        let errorMessage;
        field.addEventListener("input", function () {
            errorMessage = document.getElementById(`${field.id}Error`) ?? {};
            if (errorMessage.innerText == "Campo obrigatório") resetFieldStyle(field);
        })
    });

    main.register.nameInput.addEventListener("input", function () {
        resetFieldStyle(main.register.nameInput)
        checkValidName(main.register.nameInput.value);
    })
    main.register.emailInput.addEventListener("input", function () {
        resetFieldStyle(main.register.emailInput);
        checkValidEmail(main.register.emailInput.value);
    })
    main.register.emailInput.addEventListener("blur", function () {
        if (main.register.emailInput.value.length > 0)
            checkExistingEmail(registerModal.dataset.userId, main.register.emailInput.value);
    })
}

function setTableSettings() {
    loadClientsTable();
}
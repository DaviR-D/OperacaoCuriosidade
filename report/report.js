document.addEventListener("DOMContentLoaded", function(){
    html.report = {};
    getReportElements();
    insertReportData();
});

function getReportElements(){
    html.report.tableStart = document.getElementById("tableStart");
}

function insertReportData(){
    html.report.tableStart.insertAdjacentHTML('beforeend', 
        `
        <h1><strong>Lista de usuários</strong></h1>
        <button onclick="printTable()">IMPRIMIR</button>
        `
    )
}

function printTable(){
    window.print()
}
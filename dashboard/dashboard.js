document.addEventListener("DOMContentLoaded", function(){
    html.dashboard = {};
    getDashboardElements();
    insertDashboardData();

})

function getDashboardElements(){
    html.dashboard.tableStart = document.getElementById("tableStart");

    html.dashboard.total = document.getElementById("total");
    html.dashboard.pending = document.getElementById("pending");
    html.dashboard.lastMonth = document.getElementById("lastMonth");

}


function insertDashboardData(){
    const [totalRegistrations, pendingRegistrations, lastMonthRegistrations] = calculateStats();

    html.dashboard.tableStart.insertAdjacentHTML('beforeend', 
        `
        <h1><strong>Últimos cadastros</strong></h1>
        `
    )

    html.dashboard.total.innerText = totalRegistrations;
    html.dashboard.pending.innerText = pendingRegistrations;
    html.dashboard.lastMonth.innerText = lastMonthRegistrations;

}

function calculateStats(){
    let totalRegistrations = registrations.length;
    let pendingRegistrations = registrations.filter(registration => registration.pending === true).length;
    let lastMonthRegistrations = registrations.filter(registration => checkLastMonth(registration.date)).length;

    return [totalRegistrations, pendingRegistrations, lastMonthRegistrations];
}


function checkLastMonth(registrationDate){
    let today = new Date();
    let registrationDay = new Date(registrationDate);
    
    let dateDiference = today - registrationDay;
    let days = dateDiference / (1000 * 60 * 60 * 24);

    return days <= 30;
}

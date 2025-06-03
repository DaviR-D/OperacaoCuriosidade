document.addEventListener("DOMContentLoaded", function(){
    let html = getDashboardElements();
    insertDashboardData(html);

})

function getDashboardElements(){
    let html = {};

    html.total = document.getElementById("total");
    html.pending = document.getElementById("pending");
    html.lastMonth = document.getElementById("lastMonth");

    html.latestRegistrations = document.getElementById("registrations");

    return html;
}


function insertDashboardData(html){
    const [totalRegistrations, pendingRegistrations, lastMonthRegistrations] = calculateStats();

    html.total.innerText = totalRegistrations;
    html.pending.innerText = pendingRegistrations;
    html.lastMonth.innerText = lastMonthRegistrations;

    html.latestRegistrations.innerHTML = renderRegistrationsHTML(-3).join('');
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

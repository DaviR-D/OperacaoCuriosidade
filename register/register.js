// document.addEventListener("DOMContentLoaded", function(){
//     let html = getRegisterElements();
//     insertRegisterData(html);
// })

// function getRegisterElements(){
//     let html = {};
//     return html;
// }


// function insertRegisterData(html){}

function showRegisterWindow(){
    registerWindow.showModal();
}

function hideRegisterWindow(){
    registerWindow.close();
}

console.log(JSON.parse(localStorage.getItem("1")));
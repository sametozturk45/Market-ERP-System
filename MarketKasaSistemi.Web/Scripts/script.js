let drawerBurger = document.getElementById("drawerBurger");
let drawerArrow = document.getElementById("drawerArrow");
let drawer = document.getElementById("drawer");
let bulb = document.getElementById("bulb");
let moon = document.getElementById("moon");
let body = document.getElementById("bodyContain");
let tables = document.getElementsByClassName("table");
let dropdownItem = document.getElementById("dropMyLinks");
let dropLinks = document.getElementById("dropLinks");
let dropPosition = 1;

drawerBurger.addEventListener("click", function () {
    drawer.classList.add("drawerOpen");
    drawer.classList.remove("drawerClose");
});
drawerArrow.addEventListener("click", function () {
    drawer.classList.remove("drawerOpen");
    drawer.classList.add("drawerClose");
});
bulb.addEventListener("click", function () {
    bulb.classList.add("d-none");
    moon.classList.remove("d-none");
    body.classList.add("light");
    body.classList.remove("dark");
    tables.classList.remove("table-dark");
});
moon.addEventListener("click", function () {
    moon.classList.add("d-none");
    bulb.classList.remove("d-none");
    body.classList.add("dark");
    body.classList.remove("light");
    tables.classList.add("table-dark");
});
dropdownItem.addEventListener("click", function () {

    if (dropPosition == 0) {
        dropLinks.classList.add("d-none");
        dropPosition = 1;
    }
    else {
        dropLinks.classList.remove("d-none");
        dropPosition = 0;
    }
});

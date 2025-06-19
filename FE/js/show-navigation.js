import { filterAndDisplay, filterOperations } from "./data/filters.js";
import { renderCharts } from "./charts.js";

const categoryDescriptions = {
    REST: "Food",
    TRPT: "Transport",
    HOME: "Household",
    HLTH: "Health",
    EDUC: "Education",
    CLTH: "Clothes",
    SPRT: "Lifestyle"
};

function populateCategoryDropdown() {
    const dropdown = document.getElementById("dynamicCategoryMenu");
    if (!dropdown) return;

    dropdown.innerHTML = ""; // clear existing
    Object.entries(categoryDescriptions).forEach(([code, name]) => {
        const li = document.createElement("li");
        li.innerHTML = `<a class="dropdown-item" href="categories.html?code=${code}">${name}</a>`;
        dropdown.appendChild(li);
    });
}

$(function () {
    $(".header").load("./nav-bar.html", function () {
        setTimeout(() => {
            const dropdownElements =
                document.querySelectorAll(".dropdown-toggle");
            dropdownElements.forEach((el) => new bootstrap.Dropdown(el));
        }, 0);

        populateCategoryDropdown();

        const monthSelector = document.getElementById("monthSelector");
        if (monthSelector) {
            const savedMonth = sessionStorage.getItem("month");
            if (savedMonth) {
                monthSelector.value = savedMonth.toString().padStart(2, "0");
            }

            monthSelector.addEventListener("change", (event) => {
                const selectedValue = parseInt(event.target.value);
                console.log(3, event.target.value);
                sessionStorage.setItem("month", selectedValue);
                sessionStorage.setItem("year", 2025);
                window.location.reload();

                if (typeof filterOperations === "function") filterOperations();
                if (typeof filterAndDisplay === "function") filterAndDisplay();
                if (typeof renderCharts === "function") renderCharts();
            });
        }
        const selectedValue = parseInt(monthSelector.value);
        console.log(1, selectedValue);
        sessionStorage.setItem("month", selectedValue);
        sessionStorage.setItem("year", 2025);

        if (typeof filterOperations === "function") filterOperations();
        if (typeof filterAndDisplay === "function") filterAndDisplay();
        if (typeof renderCharts === "function") renderCharts();
    });
});

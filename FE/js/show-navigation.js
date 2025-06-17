import { filterAndDisplay, filterOperations } from "./data/filters.js";
import { renderCharts } from "./charts.js";

const categoryDescriptions = {
    "BUSS": "Бизнес услуги",
    "CLTH": "Дрехи",
    "DEBT": "Задължения и такси",
    "EDUC": "Образование",
    "HLTH": "Здраве и красота",
    "HOME": "За дома",
    "OTHR": "Други",
    "PUBS": "Публични услуги",
    "REST": "Ресторанти и барове",
    "SHOP": "Шопинг",
    "SPRT": "Забавление и спорт",
    "SUPM": "Супермаркети",
    "TRPT": "Транспорт и авто услуги",
    "TRVH": "Пътуване и ваканция",
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
        // ✅ Bootstrap dropdown fix
        setTimeout(() => {
            const dropdownElements = document.querySelectorAll('.dropdown-toggle');
            dropdownElements.forEach(el => new bootstrap.Dropdown(el));
        }, 0); // Use setTimeout to ensure HTML is parsed before initializing

        // ✅ Populate categories
        populateCategoryDropdown();

        // ✅ Month selector logic
        const monthSelector = document.getElementById("monthSelector");
        if (monthSelector) {
            const savedMonth = sessionStorage.getItem("month");
            if (savedMonth) {
                monthSelector.value = savedMonth.toString().padStart(2, '0');
            }

            monthSelector.addEventListener("change", (event) => {
                const selectedValue = parseInt(event.target.value);
                sessionStorage.setItem("month", selectedValue);
                window.location.reload();

                if (typeof filterOperations === "function") filterOperations();
                if (typeof filterAndDisplay === "function") filterAndDisplay();
                if (typeof renderCharts === "function") renderCharts();
            });
        }
    });
});

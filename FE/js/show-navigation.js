import { filterAndDisplay, filterOperations } from "./data/filters.js"
import { renderCharts } from "./charts.js";

$(function () {
    $(".header").load("./nav-bar.html", function () {
        const monthSelector = document.getElementById("monthSelector");

        if (monthSelector) {
            // Restore selected month if already in sessionStorage
            const savedMonth = sessionStorage.getItem("month");
            if (savedMonth) {
                monthSelector.value = savedMonth.toString().padStart(2, '0');
            }

            // Handle changes
            monthSelector.addEventListener("change", (event) => {
                const selectedValue = parseInt(event.target.value); // "01" -> 1
                sessionStorage.setItem("month", selectedValue);

                // Optional: re-filter and update page visuals
                if (typeof filterOperations === "function") filterOperations();
                if (typeof filterAndDisplay === "function") filterAndDisplay();
                if (typeof renderCharts === "function") renderCharts();
            });
        } else {
            console.error("monthSelector not found after loading nav-bar.html");
        }
    });
});

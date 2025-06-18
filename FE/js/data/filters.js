import { saveThisMonthAndYearOperations } from "./sessionStorage.js";
import { calculate } from "./calculators.js";

export function filterAndDisplay() {
    filterOperations();
    display();
}

export function display() {
    const income = sessionStorage.getItem("income");
    document.getElementById("incomeDisplay").textContent = income
        ? `${income} BGN`
        : "0 BGN";

    const expenses = sessionStorage.getItem("expenses");
    document.getElementById("expensesDisplay").textContent = expenses
        ? `${expenses} BGN`
        : "0 BGN";

    const balance = sessionStorage.getItem("balance");
    document.getElementById("balanceDisplay").textContent = balance
        ? `${balance} BGN`
        : "0 BGN";
}

export function filterOperations() {
    const allOperations =
        JSON.parse(sessionStorage.getItem("allOperations")) || [];
    const month = parseInt(sessionStorage.getItem("month"));
    const year = parseInt(sessionStorage.getItem("year"));

    const filtered = allOperations.filter((operation) => {
        const date = new Date(operation.dateTime);
        return date.getMonth() + 1 === month && date.getFullYear() === year;
    });
    saveThisMonthAndYearOperations(filtered);

    calculate();
}

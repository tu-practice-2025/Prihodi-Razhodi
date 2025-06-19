import {
    saveAllCards,
    saveBudgets,
    saveAllOperations,
    setMonthYear,
    saveUser,
    saveAllAccounts,
    saveCategories,
    saveEmail,
} from "./data/sessionStorage.js";
import { filterAndDisplay } from "./data/filters.js";
import {
    getOperations,
    getCategories,
    getBudgets,
    getAccounts,
    getCards,
    getUser,
} from "./data/hooks.js";
import { renderCharts } from "./charts.js";

document.addEventListener("DOMContentLoaded", async () => {
    await initialLoading();
    renderCharts();
    display();

    sendReportBtn.addEventListener("click", async () => {
        try {
            const email = localStorage.getItem("email");
            const user = await getUser(email);

            if (!user || !user.id) {
                alert("User not found.");
                return;
            }

            const response = await fetch(
                `https://localhost:7121/api/email/${user.id}`
            );
            if (!response.ok) throw new Error("Failed to send email");

            alert("Report sent successfully!");
        } catch (err) {
            console.error(err);
            alert("There was a problem sending the report.");
        }
    });

    const testBtn = document.getElementById("testButton");
    if (testBtn) {
        testBtn.addEventListener("click", test);
    }

    const openBtn = document.getElementById("openInsightsBtn");
    const modal = document.getElementById("insightsModal");
    const closeBtn = document.getElementById("closeModalBtn");
    const content = document.getElementById("insightsContent");

    openBtn.addEventListener("click", async () => {
        modal.style.display = "block";
        content.innerHTML = "<p>Loading insights...</p>";

        try {
            const response = await fetch(
                "https://localhost:7121/api/airesponse/1"
            );
            if (!response.ok) throw new Error("Failed to fetch insights");
            const data = await response.json();

            content.textContent = JSON.stringify(data.content);
        } catch (err) {
            content.innerHTML = `<p style="color: red;">Error: ${err.message}</p>`;
        }
    });

    closeBtn.addEventListener("click", () => {
        modal.style.display = "none";
    });

    window.addEventListener("click", (event) => {
        if (event.target === modal) {
            modal.style.display = "none";
        }
    });
});

async function initialLoading() {
    saveEmail("petar.andreev462@example.com");

    const email = localStorage.getItem("email");
    const user = await getUser(email);
    if (!user) {
        console.error("No user found");
        return;
    }

    saveUser(user);

    const userId = user.id;
    const [accounts, categories, operations, budgets] = await Promise.all([
        getAccounts(userId),
        getCategories(),
        getOperations(
            userId,
            sessionStorage.getItem("month"),
            sessionStorage.getItem("year")
        ),
        getBudgets(userId),
    ]);

    if (accounts) saveAllAccounts(accounts);
    if (categories) saveCategories(categories);
    if (operations) saveAllOperations(operations);
    if (budgets) saveBudgets(budgets);

    filterAndDisplay();

    if (accounts?.length) {
        const cardPromises = accounts.map((account) => getCards(account.id));
        const cardResults = await Promise.all(cardPromises);
        const allCards = cardResults.flat().filter(Boolean);
        saveAllCards(allCards);
    }
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
document.addEventListener("DOMContentLoaded", function () {
    const openBtn = document.getElementById("openInsightsBtn");
    const modal = document.getElementById("insightsModal");
    const closeBtn = document.getElementById("closeModalBtn");
    const content = document.getElementById("insightsContent");

    openBtn.addEventListener("click", async (e) => {
        e.preventDefault();
        console.log(1);
        modal.style.display = "block";
        content.innerHTML = "<p>Loading insights...</p>";

        try {
            const response = await fetch("/api/insights/analyze");
            if (!response.ok) throw new Error("");
            const data = await response.json();

            content.innerHTML = `
                <ul>
                ${data.insights.map(insight => `<li>${insight}</li>`).join("")}
                </ul>
            `;
        } catch (err) {
            content.innerHTML = `<p style="color: red;">Uh-oh. I'm currently unable to load your insights :( </br>Please try again later <3</p>`;
        }
    });

    closeBtn.addEventListener("click", () => {
        console.log(2);
        modal.style.display = "none";
    });

    window.addEventListener("click", (event) => {
        if (event.target === modal) {
            console.log(3);
            modal.style.display = "none";
        }
    });
});

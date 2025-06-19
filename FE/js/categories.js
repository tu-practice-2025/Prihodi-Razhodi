const userId = 1;
const params = new URLSearchParams(window.location.search);
const categoryCode = params.get("code") || "TRPT";
const month = sessionStorage.getItem("month");
const year = 2025;

const categoryCodeToEnglish = {
    REST: "Food",
    TRPT: "Transport",
    HOME: "Household",
    HLTH: "Health",
    EDUC: "Education",
    CLTH: "Clothes",
    SPRT: "Lifestyle",
};

window.addEventListener("DOMContentLoaded", async () => {
    const labelEl = document.querySelector(".label");
    const amountEl = document.querySelector(".amount");
    const budgetEl = document.querySelector(".budget-amount");
    const pie = document.querySelector(".pie");
    const centerText = document.querySelector(".center-text");
    const entriesList = document.getElementById("entriesList");

    const englishLabel = categoryCodeToEnglish[categoryCode] || categoryCode;
    labelEl.textContent = englishLabel;

    let totalSpent = 0;
    let transactions = [];
    let budgetAmount = 0;

    try {
        // Load spending (transactions + total spent)
        const spendingRes = await fetch(
            `https://localhost:7121/api/Categories/${userId}/category-details?code=${categoryCode}&month=${month}&year=${year}`
        );
        if (spendingRes.ok) {
            const spendingData = await spendingRes.json();
            totalSpent = spendingData.totalSpent || 0;
            transactions = spendingData.transactions || [];
        }
    } catch (error) {
        console.error("Error fetching spending data:", error);
    }

    try {
        // Load budget
        const budgetRes = await fetch(
            `https://localhost:7121/api/Budget/${userId}/category?code=${categoryCode}&month=${month}&year=${year}`
        );
        if (budgetRes.ok) {
            const budgetData = await budgetRes.json();
            budgetAmount = budgetData?.amount || 0;
        }
    } catch (error) {
        console.error("Error fetching budget data:", error);
    }

    // Fallback values
    amountEl.textContent = `$${totalSpent.toFixed(2)}`;
    budgetEl.textContent = `Budget: $${budgetAmount.toFixed(2)}`;

    const percentUsed =
        budgetAmount > 0 ? (totalSpent / budgetAmount) * 100 : 0;
    pie.style.background = `conic-gradient(#ff6666 ${percentUsed}%, #ffe5e5 ${percentUsed}% 100%)`;
    centerText.textContent = `${Math.round(percentUsed)}%`;

    entriesList.innerHTML = "";

    if (transactions.length === 0) {
        const li = document.createElement("li");
        li.classList.add("transaction-entry");
        li.style.justifyContent = "center";
        li.textContent = "No transactions available for this category.";
        entriesList.appendChild(li);
    } else {
        transactions.forEach((tx) => {
            const li = document.createElement("li");
            li.classList.add("transaction-entry");
            li.innerHTML = `
            <span>${formatDate(tx.date)}</span>
            <span>${tx.description}</span>
            <span>$${tx.amount.toFixed(2)}</span>
        `;
            entriesList.appendChild(li);
        });
    }
});

function formatDate(dateStr) {
    const d = new Date(dateStr);
    return d.toLocaleDateString("en-GB");
}

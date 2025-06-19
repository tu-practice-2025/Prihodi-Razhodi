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
    SPRT: "Lifestyle"
};

window.addEventListener("DOMContentLoaded", async () => {
    const labelEl = document.querySelector(".label");
    const amountEl = document.querySelector(".amount");
    const budgetEl = document.querySelector(".budget-amount");
    const pie = document.querySelector(".pie");
    const centerText = document.querySelector(".center-text");
    const entriesList = document.getElementById("entriesList");

    try {
        const spendingRes = await fetch(`https://localhost:7121/api/Categories/${userId}/category-details?code=${categoryCode}&month=${month}&year=${year}`);
        const spendingData = await spendingRes.json();

        const totalSpent = spendingData.totalSpent || 0;
        const transactions = spendingData.transactions || [];
        const englishLabel = categoryCodeToEnglish[categoryCode] || categoryCode;

        labelEl.textContent = englishLabel;
        amountEl.textContent = `${totalSpent.toFixed(2)}`;

        const budgetRes = await fetch(`https://localhost:7121/api/Budget/${userId}/category?code=${categoryCode}&month=${month}&year=${year}`);
        const budgetData = await budgetRes.json();
        const budgetAmount = budgetData?.amount || 1;

        budgetEl.textContent = `Budget: ${budgetAmount.toFixed(2)}`;

        const percentUsed = Math.min((totalSpent / budgetAmount) * 100);
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
            transactions.forEach(tx => {
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

    } catch (error) {
        console.error("Error loading category details:", error);

        labelEl.textContent = categoryDescriptions[categoryCode] || categoryCode;
        amountEl.textContent = "$0.00";
        budgetEl.textContent = "Budget: $0.00";
        pie.style.background = `conic-gradient(#ffe5e5 100%)`;
        centerText.textContent = "0%";

        entriesList.innerHTML = "";
        const li = document.createElement("li");
        li.classList.add("transaction-entry");
        li.style.justifyContent = "center";
        li.textContent = "Could not load data.";
        entriesList.appendChild(li);
    }
});

function formatDate(dateStr) {
    const d = new Date(dateStr);
    return d.toLocaleDateString("en-GB");
}
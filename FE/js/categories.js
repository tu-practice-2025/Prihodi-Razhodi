const userId = 1;
const categoryCode = "FOOD"; // Hardcoded for now
const month = 6;
const year = 2025;

window.addEventListener("DOMContentLoaded", async () => {
    try {
        // 🔶 Get transactions for this category
        const txRes = await fetch(`https://localhost:7121/api/Category/${userId}/spending/category?code=${categoryCode}&month=${month}&year=${year}`);
        const txData = await txRes.json(); // Expected: [{ date, description, amount }]

        // 🔶 Get budget for this category
        const budgetRes = await fetch(`https://localhost:7121/api/Budget/${userId}/category?code=${categoryCode}&month=${month}&year=${year}`);
        const budgetData = await budgetRes.json(); // Expected: { amount }

        const totalSpent = txData.reduce((sum, tx) => sum + tx.amount, 0);
        const budget = budgetData.amount || 1; // Avoid division by 0
        const percentUsed = Math.min((totalSpent / budget) * 100, 100);

        // 🔁 Update total spent in header
        document.querySelector(".amount").textContent = `$${totalSpent.toFixed(2)}`;

        // 🔁 Update pie chart
        const pie = document.querySelector(".inner-pie");
        pie.style.background = `conic-gradient(#ff6666 ${percentUsed}%, #ffe5e5 ${percentUsed}% 100%)`;
        document.querySelector(".center-text").textContent = `${Math.round(percentUsed)}%`;

        // 🔁 Populate transactions
        const entriesList = document.getElementById("entriesList");
        entriesList.innerHTML = ""; // Clear

        txData.forEach(tx => {
            const li = document.createElement("li");
            li.classList.add("transaction-entry");
            li.innerHTML = `
                <span>${formatDate(tx.date)}</span>
                <span>${tx.description}</span>
                <span>$${tx.amount.toFixed(2)}</span>
            `;
            entriesList.appendChild(li);
        });

    } catch (err) {
        console.error("Error loading category data:", err);
    }
});

function formatDate(dateStr) {
    const d = new Date(dateStr);
    return d.toLocaleDateString("en-GB");
}

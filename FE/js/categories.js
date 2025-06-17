const userId = 1;
const categoryCode = "TRVH";
const month = 6; // June
const year = 2025;

const categoryDescriptions = {
    "BUSS": "Бизнес услуги",
    "CASH": "Кеш",
    "CLTH": "Дрехи",
    "DEBT": "Задължения и такси",
    "EDUC": "Образование",
    "FINS": "Финансови услуги",
    "HLTH": "Здраве и красота",
    "HOME": "За дома",
    "INAT": "Приход ATM",
    "INCM": "Приход",
    "INVT": "Инвестиции",
    "OTHR": "Други",
    "PUBS": "Публични услуги",
    "REST": "Ресторанти и барове",
    "RPAY": "Погасяване по кредитни продукти",
    "SHOP": "Шопинг",
    "SPRT": "Забавление и спорт",
    "SUPM": "Супермаркети",
    "TRPT": "Транспорт и авто услуги",
    "TRSF": "Преводи",
    "TRVH": "Пътуване и ваканция",
    "UTIL": "Битови сметки"
};

window.addEventListener("DOMContentLoaded", async () => {
    try { 
        const spendingRes = await fetch(`https://localhost:7121/api/Categories/${userId}/category-details?code=${categoryCode}&month=${month}&year=${year}`);
        const spendingData = await spendingRes.json();

        const totalSpent = spendingData.totalSpent || 0;
        const transactions = spendingData.transactions || [];
        const categoryDescription = spendingData.categoryDescription || categoryDescriptions[categoryCode] || categoryCode;

        const budgetRes = await fetch(`https://localhost:7121/api/Budget/${userId}/category?code=${categoryCode}&month=${month}&year=${year}`);
        const budgetData = await budgetRes.json();
        const budgetAmount = budgetData?.amount || 1;

        document.querySelector(".label").textContent = categoryDescription;
        document.querySelector(".amount").textContent = `$${totalSpent.toFixed(2)}`;

        let budgetElem = document.querySelector(".budget-amount");
        if (!budgetElem) {
            budgetElem = document.createElement("div");
            budgetElem.classList.add("budget-amount");
            document.querySelector(".amount-info").appendChild(budgetElem);
        }
        budgetElem.textContent = `Budget: $${budgetAmount.toFixed(2)}`;

        const percentUsed = Math.min((totalSpent / budgetAmount) * 100, 100);
        const pie = document.querySelector(".inner-pie");
        pie.style.background = `conic-gradient(#ff6666 ${percentUsed}%, #ffe5e5 ${percentUsed}% 100%)`;
        document.querySelector(".center-text").textContent = `${Math.round(percentUsed)}%`;

        const entriesList = document.getElementById("entriesList");
        entriesList.innerHTML = "";

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

    } catch (error) {
        console.error("Error loading category details:", error);
    }
});

function formatDate(dateStr) {
    const d = new Date(dateStr);
    return d.toLocaleDateString("en-GB");
}

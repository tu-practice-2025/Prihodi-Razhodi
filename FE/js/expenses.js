// Примерни данни за графиката
const expenseData = {
    labels: ["Rent", "Utilities", "Groceries", "Transport", "Entertainment"],
    datasets: [
        {
            label: "Expenses in June",
            data: [900, 120, 300, 100, 150],
            backgroundColor: [
                "rgba(255, 99, 132, 0.6)",
                "rgba(255, 159, 64, 0.6)",
                "rgba(255, 205, 86, 0.6)",
                "rgba(75, 192, 192, 0.6)",
                "rgba(153, 102, 255, 0.6)",
            ],
        },
    ],
};

const config = {
    type: "bar",
    data: expenseData,
    options: {
        responsive: true,
        plugins: {
            legend: { position: "top" },
            title: { display: true, text: "Expenses by Category" },
        },
    },
};

// Рендериране на графиката
window.addEventListener("DOMContentLoaded", () => {
    const ctx = document.getElementById("expensesChart").getContext("2d");
    new Chart(ctx, config);

    // Примерни транзакции
    const transactions = [
        { date: "2025-06-01", category: "Rent", amount: "$900" },
        { date: "2025-06-03", category: "Groceries", amount: "$75" },
        { date: "2025-06-06", category: "Utilities", amount: "$60" },
        { date: "2025-06-08", category: "Transport", amount: "$25" },
        { date: "2025-06-10", category: "Entertainment", amount: "$50" },
    ];

    const tableBody = document.getElementById("expenseTableBody");
    transactions.forEach((tx) => {
        const row = document.createElement("tr");
        row.innerHTML = `<td>${tx.date}</td><td>${tx.category}</td><td>${tx.amount}</td>`;
        tableBody.appendChild(row);
    });
});

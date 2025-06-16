// Example data – replace with dynamic values via AJAX/fetch if needed
const incomeData = {
    labels: ["June 1", "June 5", "June 10", "June 15", "June 20", "June 25"],
    data: [200, 450, 300, 400, 500, 600],
};

const recentTransactions = [
    { date: "2025-06-14", source: "Freelance Project", amount: "$600" },
    { date: "2025-06-10", source: "Salary", amount: "$1200" },
    { date: "2025-06-08", source: "Gift", amount: "$150" },
    { date: "2025-06-05", source: "Stock Dividend", amount: "$80" },
    { date: "2025-06-02", source: "Refund", amount: "$45" },
];

// Render Chart
const ctx = document.getElementById("incomeChart").getContext("2d");
new Chart(ctx, {
    type: "line",
    data: {
        labels: incomeData.labels,
        datasets: [
            {
                label: "Income ($)",
                data: incomeData.data,
                borderColor: "green",
                backgroundColor: "rgba(0, 128, 0, 0.1)",
                tension: 0.3,
                fill: true,
            },
        ],
    },
    options: {
        responsive: true,
        scales: {
            y: {
                beginAtZero: true,
            },
        },
    },
});

// Populate Table
const tableBody = document.getElementById("incomeTableBody");
recentTransactions.forEach((tx) => {
    const row = document.createElement("tr");
    row.innerHTML = `<td>${tx.date}</td><td>${tx.source}</td><td>${tx.amount}</td>`;
    tableBody.appendChild(row);
});

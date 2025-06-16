const userId = 1;
const month = 6;
const year = 2025;

window.addEventListener("DOMContentLoaded", async () => {
    const chartCtx = document.getElementById("expensesChart").getContext("2d");
    const tableBody = document.getElementById("expenseTableBody");

    try {
        // 🔶 Fetch expense data (for chart)
        const response = await fetch(`https://localhost:7121/api/Category/${userId}/spending?month=${month}&year=${year}`);
        const data = await response.json();

        // 🔶 Render chart
        const labels = data.map(item => item.category);
        const values = data.map(item => item.total);

        new Chart(chartCtx, {
            type: "bar",
            data: {
                labels: labels,
                datasets: [{
                    label: `Expenses in ${getMonthName(month)}`,
                    data: values,
                    backgroundColor: [
                        "rgba(255, 99, 132, 0.6)",
                        "rgba(255, 159, 64, 0.6)",
                        "rgba(255, 205, 86, 0.6)",
                        "rgba(75, 192, 192, 0.6)",
                        "rgba(153, 102, 255, 0.6)",
                        "rgba(54, 162, 235, 0.6)",
                    ],
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: { position: "top" },
                    title: { display: true, text: "Expenses by Category" },
                },
            },
        });

        // 🔶 Fetch latest transactions (for table)
        const latestRes = await fetch(`https://localhost:7121/api/Category/${userId}/latest?month=${month}&year=${year}`);
        const latestData = await latestRes.json();

        // 🔶 Render table
        tableBody.innerHTML = "";
        latestData.forEach((tx) => {
    const row = document.createElement("tr");
    row.innerHTML = `
        <td>${formatDate(tx.date)}</td>
        <td>${tx.category}</td>
        <td>$${tx.amount.toFixed(2)}</td>
    `;
    tableBody.appendChild(row);
});


    } catch (error) {
        console.error("Error loading expenses:", error);
    }
});

function getMonthName(month) {
    const date = new Date(2025, month - 1);
    return date.toLocaleString("default", { month: "long" });
}

function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString("en-GB"); // e.g. 14/06/2025
}

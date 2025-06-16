const userId = 1; // Replace with dynamic ID if needed
const month = 6;
const year = 2025;

window.addEventListener("DOMContentLoaded", async () => {
    const chartCtx = document.getElementById("incomeChart").getContext("2d");
    const tableBody = document.getElementById("incomeTableBody");

    try {
        // 1️⃣ Fetch data for line chart
        const chartRes = await fetch(`https://localhost:7121/api/Income/${userId}/monthly?month=${month}&year=${year}`);
        const chartData = await chartRes.json();

        const chartLabels = chartData.map(entry => formatDateLabel(entry.date));
        const chartValues = chartData.map(entry => entry.total);

        new Chart(chartCtx, {
            type: "line",
            data: {
                labels: chartLabels,
                datasets: [{
                    label: "Income ($)",
                    data: chartValues,
                    borderColor: "green",
                    backgroundColor: "rgba(0, 128, 0, 0.1)",
                    tension: 0.3,
                    fill: true,
                }]
            },
            options: {
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });

        // 2️⃣ Fetch data for transaction table
        const tableRes = await fetch(`https://localhost:7121/api/Income/${userId}/latest?month=${month}&year=${year}`);
        const recentIncomes = await tableRes.json();

        tableBody.innerHTML = "";
        recentIncomes.forEach(tx => {
            const row = document.createElement("tr");
            row.innerHTML = `
                <td>${formatDate(tx.date)}</td>
                <td>${tx.source}</td>
                <td>$${tx.amount.toFixed(2)}</td>
            `;
            tableBody.appendChild(row);
        });

    } catch (error) {
        console.error("Error loading income data:", error);
    }
});

function formatDate(dateStr) {
    const date = new Date(dateStr);
    return date.toLocaleDateString("en-GB");
}

function formatDateLabel(dateStr) {
    const date = new Date(dateStr);
    return date.toLocaleDateString("en-GB", { day: "numeric", month: "short" }); // e.g. "5 Jun"
}

const userId = 1; // Replace with dynamic ID
const month = sessionStorage.getItem("month");
const year = 2025;

let skip = 0;
const take = 10;

window.addEventListener("DOMContentLoaded", async () => {
    const chartCtx = document.getElementById("incomeChart").getContext("2d");
    const tableBody = document.getElementById("incomeTableBody");
    const showMoreBtn = document.querySelector(".button-group button");

    try {
        const chartRes = await fetch(
            `https://localhost:7121/api/Income/${userId}/monthly?month=${month}&year=${year}`
        );
        const chartData = await chartRes.json();

        const chartLabels = chartData.map((entry) =>
            formatDateLabel(entry.date)
        );
        const chartValues = chartData.map((entry) => entry.total); // total, not amount

        new Chart(chartCtx, {
            type: "line",
            data: {
                labels: chartLabels,
                datasets: [
                    {
                        label: `Incomes in ${getMonthName(month)}`,
                        data: chartValues,
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

        await loadIncomeTransactions();

        showMoreBtn.addEventListener("click", async () => {
            await loadIncomeTransactions();
        });
    } catch (error) {
        console.error("Error loading income data:", error);
    }
});

async function loadIncomeTransactions() {
    const tableBody = document.getElementById("incomeTableBody");
    const showMoreBtn = document.querySelector(".button-group button");

    const tableRes = await fetch(
        `https://localhost:7121/api/Income/${userId}/latest?month=${month}&year=${year}&skip=${skip}&take=${take}`
    );
    const recentIncomes = await tableRes.json();

    if (recentIncomes.length === 0) {
        showMoreBtn.disabled = true;
        showMoreBtn.innerText = "No more transactions";
        return;
    }

    recentIncomes.forEach((tx) => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${formatDate(tx.date)}</td>
            <td>${tx.source}</td>
            <td>${tx.amount.toFixed(2)} BGN</td>
        `;
        tableBody.appendChild(row);
    });

    skip += take;
}

function formatDateLabel(dateStr) {
    const date = new Date(dateStr);
    return date.toLocaleDateString("en-GB", { day: "numeric", month: "short" });
}

function formatDate(dateStr) {
    const date = new Date(dateStr);
    return date.toLocaleDateString("en-GB");
}

function getMonthName(month) {
    const date = new Date(2025, month - 1);
    return date.toLocaleString("default", { month: "long" });
}

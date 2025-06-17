const userId = 1;
const month = sessionStorage.getItem("month");
const year = 2025;

let skip = 0;
const take = 10;

window.addEventListener("DOMContentLoaded", async () => {
    const chartCtx = document.getElementById("expensesChart").getContext("2d");
    const tableBody = document.getElementById("expenseTableBody");
    const showMoreBtn = document.querySelector(".button-group button");

    try {
        const chartRes = await fetch(`https://localhost:7121/api/Expenses/${userId}/spending?month=${month}&year=${year}`);
        const chartData = await chartRes.json();

        const labels = chartData.map(item => item.category);
        const values = chartData.map(item => item.total);

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

        await loadExpenseTransactions();

        showMoreBtn.addEventListener("click", async () => {
            await loadExpenseTransactions();
        });

    } catch (error) {
        console.error("Error loading expenses:", error);
    }
});

async function loadExpenseTransactions() {
    const tableBody = document.getElementById("expenseTableBody");
    const showMoreBtn = document.querySelector(".button-group button");

    const res = await fetch(`https://localhost:7121/api/Expenses/${userId}/latest?month=${month}&year=${year}&skip=${skip}&take=${take}`);
    const data = await res.json();

    if (data.length === 0) {
        showMoreBtn.disabled = true;
        showMoreBtn.innerText = "No more transactions";
        return;
    }

    data.forEach((tx) => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${formatDate(tx.date)}</td>
            <td>${tx.category}</td>
            <td>$${tx.amount.toFixed(2)}</td>
        `;
        tableBody.appendChild(row);
    });

    skip += take;
}

function getMonthName(month) {
    const date = new Date(2025, month - 1);
    return date.toLocaleString("default", { month: "long" });
}

function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString("en-GB"); // e.g. 14/06/2025
}

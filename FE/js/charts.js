export function renderCharts() {
    const operations = JSON.parse(sessionStorage.getItem("thisMonthAndYearOperations"));    //fetching operations
    if (!operations) {
        console.error("No operations loaded");
        return;
    }

    function getISOWeek(date) {
        const tempDate = new Date(date);
        tempDate.setHours(0, 0, 0, 0);

        // Thursday is used to determine the week (ISO 8601)
        tempDate.setDate(tempDate.getDate() + 3 - ((tempDate.getDay() + 6) % 7));

        const firstThursday = new Date(tempDate.getFullYear(), 0, 4);
        const weekNumber = 1 + Math.round(((tempDate - firstThursday) / 86400000 - 3 + ((firstThursday.getDay() + 6) % 7)) / 7);

        return weekNumber;
    }

    function groupOperationsByWeek(operations, selectedMonth, selectedYear) {
        const startDate = new Date(selectedYear, selectedMonth - 1, 1);
        const endDate = new Date(selectedYear, selectedMonth, 0); // last day of the month

        // Get ISO weeks covered by the month
        const weekSet = new Set();
        for (let d = new Date(startDate); d <= endDate; d.setDate(d.getDate() + 1)) {
            weekSet.add(getISOWeek(new Date(d)));
        }

        // Initialize empty week structure
        const grouped = {};
        for (const weekNum of weekSet) {
            grouped[`week${weekNum}`] = [];
        }

        // Assign operations into correct weeks
        operations.forEach(op => {
            const date = new Date(op.dateTime);
            const weekNum = getISOWeek(date);
            const key = `week${weekNum}`;
            if (grouped[key]) {
                grouped[key].push(op);
            }
        });

        return grouped;
    }


    function getWeeklyTotals(grouped) {
        const income = [];
        const expenses = [];
        const labels = [];

        const sortedWeeks = Object.keys(grouped).sort((a, b) => {
            const numA = parseInt(a.replace("week", ""));
            const numB = parseInt(b.replace("week", ""));
            return numA - numB;
        });

        for (const week of sortedWeeks) {
            let incomeSum = 0;
            let expenseSum = 0;

            grouped[week].forEach(op => {
                if (op.isExpense) expenseSum += op.amountLcy;
                else incomeSum += op.amountLcy;
            });

            income.push(incomeSum);
            expenses.push(expenseSum);
            labels.push(week.replace("week", "Week "));
        }

        return { income, expenses, labels };
    }

    const month = parseInt(sessionStorage.getItem("month"));
    const year = parseInt(sessionStorage.getItem("year"));

    const grouped = groupOperationsByWeek(operations, month, year);
    const { income, expenses, labels } = getWeeklyTotals(grouped);

    const data = {
        labels: labels.map(w => w.replace("week", "Week ")), // Optional cleaner labels
        datasets: [
            {
                label: "Income",
                data: income,
                borderColor: "green",
                backgroundColor: "rgba(0, 128, 0, 0.2)",
            },
            {
                label: "Expenses",
                data: expenses,
                borderColor: "red",
                backgroundColor: "rgba(255, 0, 0, 0.2)",
            }
        ]
    };

    const lineConfig = {
        type: "line",
        data: data,
        options: {
            responsive: true,
            interaction: {
                mode: "index",
                intersect: false,
            },
            stacked: false,
            plugins: {
                title: {
                    display: true,
                },
            },
            scales: {
                y: {
                    type: "linear",
                    display: true,
                    position: "left",
                }
            },
        },
    };

    const lineCtx = document.getElementById("lineChart")?.getContext("2d");
    if (lineCtx) {
        new Chart(lineCtx, lineConfig);
    } else {
        console.error("No canvas with id='lineChart'");
    }

    // 🍩 ДОУГНАТ ГРАФИКА (Expenses by Category)
    const doughnutLabels = ["Food", "Transport", "Health", "Lifestyle"];
    const doughnutData = {
        labels: doughnutLabels,
        datasets: [
            {
                label: "Expenses",
                data: [400, 250, 150, 300],
                backgroundColor: [
                    "rgba(255, 99, 132, 0.7)",
                    "rgba(54, 162, 235, 0.7)",
                    "rgba(255, 206, 86, 0.7)",
                    "rgba(75, 192, 192, 0.7)",
                ],
                borderWidth: 1,
            },
        ],
    };

    const doughnutConfig = {
        type: "doughnut",
        data: doughnutData,
        options: {
            responsive: true,
            plugins: {
                title: {
                    display: true,
                    text: "Expenses by Category",
                },
            },
        },
    };

    const doughnutCtx = document.getElementById("pieChart")?.getContext("2d");
    if (doughnutCtx) {
        new Chart(doughnutCtx, doughnutConfig);
    } else {
        console.error("No canvas with id='doughnutChart'");
    }
};

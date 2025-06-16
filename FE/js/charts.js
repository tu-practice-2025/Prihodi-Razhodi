import { getOperations } from './data/api.js';
import { state } from './data/state.js';

window.onload = async function () {
    const userId = 1; //hardcoded, should be changed to state values
    const month = 6; //hardcoded, should be changed to state values
    const year = 2025; //hardcoded, should be changed to state values

    if (!userId || !month || !year) {
        console.error("Missing user, month, or year");  // cheks for invalid data
        return;
    }

    const operations = await getOperations(userId, month, year);    //fetching operations
    if (!operations) {
        console.error("No operations loaded");
        return;
    }

    function groupOperationsByWeek(operations) {    //grouping operations by week
        const weeks = { //initializing arrays
            week1: [],
            week2: [],
            week3: [],
            week4: [],
            week5: []
        };

        operations.forEach(op => {  //separation by weeks in arrays
            const date = new Date(op.dateTime);
            const day = date.getDate();

            if (day <= 7) weeks.week1.push(op);
            else if (day <= 14) weeks.week2.push(op);
            else if (day <= 21) weeks.week3.push(op);
            else if (day <= 28) weeks.week4.push(op);
            else weeks.week5.push(op);
        });

        return weeks;
    }

    function getWeeklyTotals(grouped) { //summing incomes and expenses
        const income = [];
        const expenses = [];

        for (let i = 1; i <= 5; i++) {
            const week = grouped[`week${i}`];
            let incomeSum = 0;
            let expenseSum = 0;

            week.forEach(op => {
                if (op.isExpense) expenseSum += op.amountLcy;
                else incomeSum += op.amountLcy;
            });

            income.push(incomeSum);
            expenses.push(expenseSum);
        }

        return { income, expenses };
    }

    const grouped = groupOperationsByWeek(operations);
    const { income, expenses } = getWeeklyTotals(grouped);

    const data = {
        labels: ["Week 1", "Week 2", "Week 3", "Week 4", "Week 5"],
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
                },
                y1: {
                    type: "linear",
                    display: true,
                    position: "right",
                    grid: {
                        drawOnChartArea: false,
                    },
                },
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

window.onload = function () {
    // 📊 ЛИНЕЙНА ГРАФИКА (Income & Expenses)
    const lineLabels = [
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
    ];

    const lineData = {
        labels: lineLabels,
        datasets: [
            {
                label: "Income",
                data: [3000, 3200, 2800, 3500, 4000, 3800, 4200],
                borderColor: "green",
                backgroundColor: "rgba(0, 128, 0, 0.2)",
                yAxisID: "y",
            },
            {
                label: "Expenses",
                data: [2000, 1800, 2200, 2500, 2600, 2400, 2700],
                borderColor: "red",
                backgroundColor: "rgba(255, 0, 0, 0.2)",
                yAxisID: "y1",
            },
        ],
    };

    const lineConfig = {
        type: "line",
        data: lineData,
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
                    text: "Income & Expenses Over Time",
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

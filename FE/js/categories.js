document.addEventListener("DOMContentLoaded", () => {
  fetch("data.json")
    .then(response => {
      if (!response.ok) throw new Error("Network response was not ok");
      return response.json();
    })
    .then(data => {
      updateCategoryAmount(data.categoryAmount);
      updateGoalChart(data.goalPercentage);
      renderTransactions(data.transactions);
    })
    .catch(error => {
      console.error("Fetch error:", error);
    });
});

function updateCategoryAmount(amount) {
  const amountEl = document.querySelector(".amount-info .amount");
  amountEl.textContent = `$${amount.toFixed(2)}`;
}

function updateGoalChart(percentage) {
  const pie = document.querySelector(".pie");
  const centerText = document.querySelector(".center-text");
  centerText.textContent = `${percentage}%`;
  pie.style.background = `conic-gradient(#666 0% ${percentage}%, #eee ${percentage}% 100%)`;
}

function renderTransactions(transactions) {
  const container = document.querySelector(".transactions-section");
  container.innerHTML = "<h2>Transactions</h2>";

  transactions.forEach(tx => {
    const txDiv = document.createElement("div");
    txDiv.classList.add("transaction");

    const idSpan = document.createElement("span");
    idSpan.textContent = tx.id;

    const merchantSpan = document.createElement("span");
    merchantSpan.classList.add("merchant");
    merchantSpan.textContent = tx.merchant;

    const amountSpan = document.createElement("span");
    amountSpan.classList.add("amount");
    amountSpan.textContent = (tx.amount > 0 ? "+" : "") + tx.amount.toFixed(2);
    amountSpan.classList.add(tx.amount >= 0 ? "positive" : "negative");

    txDiv.appendChild(idSpan);
    txDiv.appendChild(merchantSpan);
    txDiv.appendChild(amountSpan);

    container.appendChild(txDiv);
  });
}

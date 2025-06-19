import {
  saveAllCards,
  saveBudgets,
  saveAllOperations,
  setMonthYear,
  saveUser,
  saveAllAccounts,
  saveCategories,
  saveEmail,
} from "./data/sessionStorage.js";
import { filterAndDisplay } from "./data/filters.js";
import {
  getOperations,
  getCategories,
  getBudgets,
  getAccounts,
  getCards,
  getUser,
} from "./data/hooks.js";
import { renderCharts } from "./charts.js";

document.addEventListener("DOMContentLoaded", async () => {
  await initialLoading();
  renderCharts();
  display();

  sendReportBtn.addEventListener("click", async () => {
    try {
      const email = localStorage.getItem("email");
      const user = await getUser(email);

      if (!user || !user.id) {
        alert("User not found.");
        return;
      }

      const response = await fetch(
        `https://localhost:7121/api/email/${user.id}`
      );
      if (!response.ok) throw new Error("Failed to send email");

      alert("Report sent successfully!");
    } catch (err) {
      console.error(err);
      alert("There was a problem sending the report.");
    }
  });
});

async function initialLoading() {
  saveEmail("vasko10.vs38@gmail.com");

  const email = localStorage.getItem("email");
  const user = await getUser(email);
  if (!user) {
    console.error("No user found");
    return;
  }

  saveUser(user);

  const userId = user.id;
  const [accounts, categories, operations, budgets] = await Promise.all([
    getAccounts(userId),
    getCategories(),
    getOperations(
      userId,
      sessionStorage.getItem("month"),
      sessionStorage.getItem("year")
    ),
    getBudgets(userId),
  ]);

  if (accounts) saveAllAccounts(accounts);
  if (categories) saveCategories(categories);
  if (operations) saveAllOperations(operations);
  if (budgets) saveBudgets(budgets);

  filterAndDisplay();

  if (accounts?.length) {
    const cardPromises = accounts.map((account) => getCards(account.id));
    const cardResults = await Promise.all(cardPromises);
    const allCards = cardResults.flat().filter(Boolean);
    saveAllCards(allCards);
  }
}

export function display() {
  const income = sessionStorage.getItem("income");
  document.getElementById("incomeDisplay").textContent = income
    ? `${income} BGN`
    : "0 BGN";

  const expenses = sessionStorage.getItem("expenses");
  document.getElementById("expensesDisplay").textContent = expenses
    ? `${expenses} BGN`
    : "0 BGN";

  const balance = sessionStorage.getItem("balance");
  document.getElementById("balanceDisplay").textContent = balance
    ? `${balance} BGN`
    : "0 BGN";
}

document.addEventListener("DOMContentLoaded", function () {
  const openBtn = document.getElementById("openInsightsBtn");
  const modal = document.getElementById("insightsModal");
  const closeBtn = document.getElementById("closeModalBtn");
  const content = document.getElementById("insightsContent");

  openBtn.addEventListener("click", async () => {
    const user = await getUser(localStorage.getItem("email"));
    modal.style.display = "block";
    content.innerHTML = "<p>Loading insights...</p>";

    try {
      const response = await fetch(
        `https://localhost:7121/api/airesponse/${user.id}`
      );
      if (!response.ok) throw new Error("Failed to fetch insights");

      const { value } = await response.json(); // parse JSON → grab .value
      if (typeof value !== "string") {
        throw new Error("API did not return a string in .value");
      }
      content.innerHTML = value.replace(/\n/g, "<br><br>");
    } catch (err) {
      content.innerHTML = `<p style="color: red;">Error: ${err.message}</p>`;
    }
  });

  closeBtn.addEventListener("click", () => {
    console.log(2);
    modal.style.display = "none";
  });

  window.addEventListener("click", (event) => {
    if (event.target === modal) {
      console.log(3);
      modal.style.display = "none";
    }
  });
});

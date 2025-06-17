import { saveAllCards, saveBudgets, saveAllOperations, setMonthYear, saveUser, saveAllAccounts, saveCategories, saveEmail } from './data/sessionStorage.js';
import { filterAndDisplay } from './data/filters.js';
import { getOperations, getCategories, getBudgets, getAccounts, getCards, getUser} from './data/hooks.js'
import { renderCharts } from './charts.js';

document.addEventListener("DOMContentLoaded", async () => {
    await initialLoading();
    renderCharts();
    display();

    const testBtn = document.getElementById("testButton");
    if (testBtn) {
        testBtn.addEventListener("click", test);
    }

    function test(){
        // setYear(2025);
        // setMonth(6);
        // filterOperations();
        // filterAndDisplay();
    
    
        // deleteBudget(5)
        // .then(data => {
        //     console.log("Posted budget successfully:", data);
        // })
        // .catch(err => {
        //     console.error("Error:", err);
        // });
    }
});

async function initialLoading() {
    saveEmail("totalyrealemail@totalyrealdomain.comtotalyre1");
    setMonthYear();
    
    const email = localStorage.getItem('email');
    const user = await getUser(email);
    if (!user) {
        console.error("No user found");
        return;
    }

    saveUser(user);


    // const userId = user.id;

    // const [accounts, categories, operations, budgets] = await Promise.all([
    //     getAccounts(userId),
    //     getCategories(),
    //     getOperations(userId, sessionStorage.getItem("month"), sessionStorage.getItem("year")),
    //     getBudgets(userId)
    // ]);

    // if (accounts) saveAllAccounts(accounts);
    // if (categories) saveCategories(categories);
    // if (operations) saveAllOperations(operations);
    // if (budgets) saveBudgets(budgets);

    // filterAndDisplay();

    // if (accounts?.length) {
    // const cardPromises = accounts.map(account => getCards(account.id));
    // const cardResults = await Promise.all(cardPromises);

    // const allCards = cardResults.flat().filter(Boolean);

    // saveAllCards(allCards);
    // }
}

export function display() {
    const income = sessionStorage.getItem('income');
    document.getElementById("incomeDisplay").textContent = income ? `${income} BGN` : "0 BGN";

    const expenses = sessionStorage.getItem('expenses');
    document.getElementById("expensesDisplay").textContent = expenses ? `${expenses} BGN` : "0 BGN";

    const balance = sessionStorage.getItem('balance');
    document.getElementById("balanceDisplay").textContent = balance ? `${balance} BGN` : "0 BGN";
}

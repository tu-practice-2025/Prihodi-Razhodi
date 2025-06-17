// export const state = {
//     email: localStorage.getItem('email'),
//     currentMonth: sessionStorage.getItem('month'),
//     currentYear: sessionStorage.getItem('year'),
//     operations: sessionStorage.getItem('operations')
// };

export function saveUserId(userId){
    sessionStorage
}

export function setMonthYear() {
    const date = new Date();
    const month = date.getMonth() + 1;
    const year = date.getFullYear();
    sessionStorage.setItem('month', month);
    sessionStorage.setItem('year', year);
}

export function setMonth(month) {
    state.currentMonth = month;
    sessionStorage.setItem('month', month);
}

export function setYear(year) {
    state.currentYear = year;
    sessionStorage.setItem('year', year);
}

export function saveAllOperations(operations) {
    const toSave = JSON.stringify(operations);
    sessionStorage.setItem('allOperations', toSave);
}

export function saveThisMonthAndYearOperations(operations) {
    const toSave = JSON.stringify(operations);
    sessionStorage.setItem('thisMonthAndYearOperations', toSave);
}

export function saveIncomeAndExpenses(income, expenses) {
    sessionStorage.setItem('income', income);
    sessionStorage.setItem('expenses', expenses);
}

export function saveBalance(balance) {
    sessionStorage.setItem('balance', balance);
}

export function saveBudgets(budgets) {
    const toSave = JSON.stringify(budgets);
    sessionStorage.setItem('budgets', toSave);
}

export function saveCategories(categories) {
    const toSave = JSON.stringify(categories);
    sessionStorage.setItem('categories', toSave);
}

export function saveAllAccounts(accounts) {
    const toSave = JSON.stringify(accounts);
    sessionStorage.setItem('accounts', toSave);
}

export function saveAllCards(cards) {
    const toSave = JSON.stringify(cards);
    sessionStorage.setItem('cards', toSave);
}

export function saveUser(user) {
    const toSave = JSON.stringify(user);
    sessionStorage.setItem('user', toSave);
}

export function saveEmail(email) {
    localStorage.setItem('email', email)
}

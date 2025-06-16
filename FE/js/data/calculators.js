import { saveBalance, saveIncomeAndExpenses } from './sessionStorage.js';

export function calculate(){
    calculateAndSaveIncomeAndExpenses();
    calculateAndSaveBalances();
}

export function calculateAndSaveIncomeAndExpenses() {
    let incomeSum = 0;
    let expensesSum = 0;

    let operations = JSON.parse(sessionStorage.getItem("thisMonthAndYearOperations"));

    for (let i = 0; i <= operations.length - 1; i++) { //has to ensure that the ids are set up correctly and there are no missing IDs
        if(operations[i].isExpense) {
            expensesSum += operations[i].amountLcy;
        } else {
            incomeSum += operations[i].amountLcy;
        }
    }
    saveIncomeAndExpenses(incomeSum, expensesSum);
}

export function calculateAndSaveBalances() {
    let balanceSum = 0;

    let accounts = JSON.parse(sessionStorage.getItem("accounts"));

    for (let i = 0; i <= accounts.length - 1; i++) {
        balanceSum += accounts[i].balance;
    }

    saveBalance(balanceSum);
}
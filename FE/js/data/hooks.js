export async function getOperations(userId, month, year) {
    try {
        const res = await fetch(
            `https://localhost:7121/api/operation?userId=${userId}&month=${month}&year=${year}`
        );
        if (!res.ok) throw new Error("Failed to load operations");
        return await res.json();
    } catch (err) {
        console.error("Error loading operations", err);
        return null;
    }
}

export async function getCategories() {
    try {
        const res = await fetch(`https://localhost:7121/api/Expenses/`);
        if (!res.ok) throw new Error("Failed to load categories");
        return await res.json();
    } catch (err) {
        console.error("Error loading categories", err);
        return [];
    }
}

export async function getBudgets(userId) {
    try {
        const res = await fetch(`https://localhost:7121/api/budget/${userId}`);
        if (!res.ok) throw new Error("Failed to load budgets");
        return await res.json();
    } catch (err) {
        console.error("Error loading budgets", err);
        return [];
    }
}

export async function postBudget(budget) {
    try {
        const res = await fetch(`https://localhost:7121/api/budget`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(budget),
        });
        if (!res.ok) throw new Error("Failed to post budget");
        return await res.json();
    } catch (err) {
        console.error("Error posting budget:", err);
        return null;
    }
}

export async function putBudgets(budget) {
    try {
        const res = await fetch(`https://localhost:7121/api/budget/`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(budget),
        });
        if (!res.ok) throw new Error("Failed to update budget");
        return await res.json();
    } catch (err) {
        console.error("Error putting budget", err);
        return null;
    }
}

export async function deleteBudget(budgetId) {
    try {
        const res = await fetch(
            `https://localhost:7121/api/budget/${budgetId}`,
            {
                method: "DELETE",
            }
        );
        if (!res.ok) throw new Error("Failed to delete budget");
        console.log(res);
        return await res.json();
    } catch (err) {
        console.error("Error putting budget", err);
        return null;
    }
}

export async function getAccounts(userId) {
    try {
        const res = await fetch(`https://localhost:7121/api/account/${userId}`);
        if (!res.ok) throw new Error("Failed to load accounts");
        return await res.json();
    } catch (err) {
        console.error("Error loading accounts", err);
        return null;
    }
}

export async function getCards(accountId) {
    try {
        const res = await fetch(`https://localhost:7121/api/card/${accountId}`);
        if (!res.ok) throw new Error("Failed to load cards");
        return await res.json();
    } catch (err) {
        console.error("Error loading cards", err);
        return null;
    }
}

export async function getUser(email) {
    try {
        const res = await fetch(`https://localhost:7121/api/user`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email }),
        });
        if (!res.ok) throw new Error("Failed to load user");
        return await res.json();
    } catch (err) {
        console.error("Error loading user:", err);
        return null;
    }
}

export async function getIncomeSumary(userId, month, year) {
    try {
        console.log("Getting income summary");

        const res = await fetch(
            `https://localhost:7121/api/summary/income?userId=${userId}&month=${month}&year=${year}`
        );
        if (!res.ok) throw new Error("Failed to load income");
        return await res.json();
    } catch (err) {
        console.error("Error loading cards", err);
        return null;
    }
}

export async function getExpensesSumary(userId, month, year) {
    try {
        console.log("Getting expenses summary");
        const res = await fetch(
            `https://localhost:7121/api/summary/expenses?userId=${userId}&month=${month}&year=${year}`
        );
        if (!res.ok) throw new Error("Failed to load expenses");
        return await res.json();
    } catch (err) {
        console.error("Error loading expenses", err);
        return null;
    }
}

export async function getBalanceSumary(userId, month, year) {
    try {
        const res = await fetch(`https://localhost:7121/api/summary/balance/1`);
        if (!res.ok) throw new Error("Failed to load balance");
        return await res.json();
    } catch (err) {
        console.error("Error loading balance", err);
        return null;
    }
}

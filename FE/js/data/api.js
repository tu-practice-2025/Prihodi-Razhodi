export async function getOperations(userId, month, year) {
    try {
        const res = await fetch(`https://localhost:7121/api/operation?userId=${userId}&month=${month}&year=${year}`);
        if (!res.ok) throw new Error("Failed to load operations");
        return await res.json();
    } catch (err) {
        console.error("Error loading operations", err);
        return null;
    }
}

export async function getCategories() {
    try {
        const res = await fetch(`api/categories/`);
        if (!res.ok) throw new Error("Failed to load categories");
        return await res.json();
    } catch (err) {
        console.error("Error loading categories", err);
        return [];
    }
}

export async function getBudgets(userId) {
    try{
        const res = await fetch(`/api/budget/${userId}`);
        if (!res.ok) throw new Error("Failed to load budgets");
        return await res.json();
    } catch (err) {
        console.error("Error loading budgets", err);
        return [];
    }
}

export async function postBudget(budget) {
    try{
        const res = await fetch(`/api/budget`, {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(budget)
        });
        return await res.json();
    } catch (err) {
        console.error("Error posting budget:", err);
        return null;
    }
}

export async function putBudgets(budget) {
    try {
        const res = await fetch(`/api/budget/`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(budget)
        });
        if (!res.ok) throw new Error("Failed to update budget");
        return await res.json();
    } catch (err) {
        console.error("Error putting budget", err);
        return null;
    }
}

export async function deleteBudgets(budgetId) {
    try {
        const res = await fetch(`/api/budget/${budgetId}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(budget)
        });
        if (!res.ok) throw new Error("Failed to delete budget");
        return await res.json();
    } catch (err) {
        console.error("Error putting budget", err);
        return null;
    }
}

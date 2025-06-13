function showBudgets() {
    fetch("/api/planning")
        .then((res) => res.json())
        .then((data) => {
            const entriesList = document.getElementById("entriesList");

            data.forEach(
                ({
                    amount,
                    currency,
                    is_income,
                    start_date,
                    end_date,
                    category_code,
                }) => {
                    const type = is_income ? "Income" : "Expense";

                    const li = document.createElement("li");
                    li.classList.add(
                        is_income ? "income-entry" : "expense-entry"
                    );

                    const textSpan = document.createElement("span");
                    textSpan.textContent = `${type}: ${amount} ${currency} \– Categories: ${category_code} – From: ${start_date} To: ${end_date}`;
                    const editButton = document.createElement("button");
                    editButton.innerHTML = "✏️</br>Edit";
                    editButton.className = "edit-btn";
                    editButton.addEventListener("click", () =>
                        editEntry(entry, {
                            amount,
                            category,
                            date,
                            description,
                            isExpense,
                        })
                    );
                    const deleteButton = document.createElement("button");
                    deleteButton.innerHTML = "❌</br>Delete";
                    deleteButton.className = "delete-btn";
                    deleteButton.addEventListener("click", () =>
                        entry.remove()
                    );
                    li.appendChild(textSpan);
                    li.appendChild(editButton);
                    li.appendChild(deleteButton);

                    entriesList.appendChild(li);
                }
            );
        })
        .catch((err) => {
            console.error("Error loading budgets:", err);
        });
}

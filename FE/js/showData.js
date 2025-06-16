document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("enterData");
    const entriesList = document.getElementById("entriesList");

    // Fetch and show all entries on load
    function showBudgets() {
        entriesList.innerHTML = ""; // Clear existing list
        fetch("/api/planning")
            .then((res) => {
                if (!res.ok) throw new Error("Failed to load entries");
                return res.json();
            })
            .then((data) => {
                data.forEach((entry) => {
                    appendEntry(entry);
                });
            })
            .catch((err) => console.error("Error loading budgets:", err));
    }

    // Append a single entry to the list with buttons
    function appendEntry(entry) {
        const entryElement = document.createElement("li");
        entryElement.classList.add(
            entry.is_income ? "income-entry" : "expense-entry"
        );

        const typeText = entry.is_income ? "Приход" : "Разход";

        const textSpan = document.createElement("span");
        textSpan.innerHTML = `${typeText} - ${entry.category_code}: ${entry.amount} ${entry.currency} <br>Start date: ${entry.start_date}<br>End date: ${entry.end_date}`;

        const editButton = document.createElement("button");
        editButton.innerHTML = "✏️</br>Edit";
        editButton.className = "edit-btn";
        editButton.addEventListener("click", () =>
            editEntry(entryElement, entry)
        );

        const deleteButton = document.createElement("button");
        deleteButton.innerHTML = "❌</br>Delete";
        deleteButton.className = "delete-btn";
        deleteButton.addEventListener("click", () =>
            deleteEntry(entry.id, entryElement)
        );

        entryElement.appendChild(textSpan);
        entryElement.appendChild(editButton);
        entryElement.appendChild(deleteButton);

        entriesList.appendChild(entryElement);
    }

    // Form submit - add new entry
    form.addEventListener("submit", function (e) {
        e.preventDefault();

        const amount = document.getElementById("amount").value.trim();
        const category = document.getElementById("category").value;
        const start_date = form.start_date.value;
        const end_date = form.end_date.value;
        const isExpense = document.getElementById("entryToggle").checked;

        if (!amount || !category || !start_date || !end_date) {
            alert("Моля, попълнете всички полета.");
            return;
        }

        const entryData = {
            amount: parseFloat(amount),
            currency: form.currency.value || "BGN",
            category_code: category,
            is_income: !isExpense,
            start_date,
            end_date,
        };

        addEntry(entryData);
    });

    // POST new entry to server
    function addEntry(data) {
        fetch("/api/planning/{id}", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data),
        })
            .then((res) => {
                if (!res.ok) throw new Error("Failed to add entry");
                return res.json();
            })
            .then((newEntry) => {
                appendEntry(newEntry);
                form.reset();
                document.getElementById("entryToggle").checked = false;
            })
            .catch((err) => console.error("Error adding entry:", err));
    }

    // DELETE entry by id
    function deleteEntry(id, element) {
        fetch(`/api/planning/${id}`, {
            method: "DELETE",
        })
            .then((res) => {
                if (!res.ok) throw new Error("Failed to delete entry");
                element.remove();
            })
            .catch((err) => console.error("Error deleting entry:", err));
    }

    // Fill form for editing and remove entry from list to update later
    function editEntry(entryElement, data) {
        document.getElementById("amount").value = data.amount;
        document.getElementById("category").value = data.category_code;
        document.getElementById("currency").value = data.currency;
        form.start_date.value = data.start_date;
        form.end_date.value = data.end_date;
        document.getElementById("entryToggle").checked = !data.is_income; // checked ==  expense

        entryElement.remove();
    }

    showBudgets();
});

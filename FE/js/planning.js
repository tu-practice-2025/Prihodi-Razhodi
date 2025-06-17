document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("enterData");
    const entriesList = document.getElementById("entriesList");

    const API_BASE_URL = "https://localhost:7121/api/budget";
    const USER_ID = 1;
    const YEAR = 2025;

    let isEditing = false;
    let currentEditingElement = null;

    // Load existing entries
    fetch(`${API_BASE_URL}/${USER_ID}`)
        .then((res) => res.json())
        .then((data) => {
            data.forEach((entry) => addEntryToDOM(entry));
        });

    form.addEventListener("submit", function (e) {
        e.preventDefault();

        const amount = document.getElementById("amount").value.trim();
        const currency = document.getElementById("currency").value;
        const category = document.getElementById("category").value;
        const month = document.getElementById("month").value;
        const isExpense = document.getElementById("entryToggle").checked;

        if (!amount || !category || !currency || !month) {
            alert("Please, complete all required fields.");
            return;
        }

        const parsedAmount = parseFloat(amount);
        const parsedMonth = parseInt(month);

        if (isEditing && currentEditingElement) {
            const payload = {
                id: parseInt(currentEditingElement.dataset.id),
                amount: parsedAmount,
                currency: currency,
                categoryCode: category, // send code, not description
                month: parsedMonth,
                year: YEAR,
                userId: USER_ID,
            };

            fetch(API_BASE_URL, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(payload),
            }).then((response) => {
                if (!response.ok) {
                    alert("Failed to update entry.");
                    return;
                }
                // Reload updated list
                fetch(`${API_BASE_URL}/${USER_ID}`)
                    .then((res) => res.json())
                    .then((data) => {
                        entriesList.innerHTML = "";
                        data.forEach((entry) => addEntryToDOM(entry));
                    });
                isEditing = false;
                currentEditingElement = null;
                form.reset();
                document.getElementById("entryToggle").checked = false;
            });
        } else {
            const postPayload = {
                amount: parsedAmount,
                currency: currency,
                categoryCode: category,
                month: parsedMonth,
                year: YEAR,
                userId: USER_ID,
            };

            fetch(API_BASE_URL, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(postPayload),
            }).then((response) => {
                if (!response.ok) {
                    alert("Failed to add entry.");
                    return;
                }
                // Reload after successful insert
                fetch(`${API_BASE_URL}/${USER_ID}`)
                    .then((res) => res.json())
                    .then((data) => {
                        entriesList.innerHTML = "";
                        data.forEach((entry) => addEntryToDOM(entry));
                    });
                form.reset();
                document.getElementById("entryToggle").checked = false;
            });
        }
    });

    function addEntryToDOM(entry) {
        const li = document.createElement("li");
        li.classList.add(entry.currency ? "expense-entry" : "income-entry");
        li.dataset.id = entry.id;

        const textSpan = document.createElement("span");
        textSpan.innerHTML = `${entry.currency ? "Expense" : "Income"} - ${
            entry.categoryDescription
        }: ${entry.amount} ${entry.currency} <br>Planned for: ${entry.month}`;

        const editButton = document.createElement("button");
        editButton.innerHTML = "✏️</br>Edit";
        editButton.className = "edit-btn";
        editButton.addEventListener("click", () => {
            document.getElementById("amount").value = entry.amount;
            document.getElementById("currency").value = entry.currency;
            document.getElementById("category").value = entry.categoryCode; // use code
            document.getElementById("month").value = entry.month;
            document.getElementById("entryToggle").checked = entry.currency
                ? true
                : false;

            isEditing = true;
            currentEditingElement = li;
            form.scrollIntoView({ behavior: "smooth" });
        });

        const deleteButton = document.createElement("button");
        deleteButton.innerHTML = "❌</br>Delete";
        deleteButton.className = "delete-btn";
        deleteButton.addEventListener("click", () => {
            if (confirm("Are you sure you want to delete this entry?")) {
                fetch(`${API_BASE_URL}/${entry.id}`, {
                    method: "DELETE",
                }).then((response) => {
                    if (!response.ok) {
                        alert("Failed to delete entry.");
                        return;
                    }
                    li.remove();
                });
            }
        });

        li.appendChild(textSpan);
        li.appendChild(editButton);
        li.appendChild(deleteButton);
        entriesList.appendChild(li);
    }
});

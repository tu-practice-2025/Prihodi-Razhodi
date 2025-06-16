document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("enterData");
    const entriesList = document.getElementById("entriesList");

    let isEditing = false;
    let currentEditingElement = null;

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

        const type = isExpense ? "Expense" : "Income";

        const entry = document.createElement("li");
        entry.classList.add(isExpense ? "expense-entry" : "income-entry");

        const textSpan = document.createElement("span");
        textSpan.innerHTML = `${type} - ${category}: ${amount} ${currency} <br>Planned for: ${month}`;

        const editButton = document.createElement("button");
        editButton.innerHTML = "✏️</br>Edit";
        editButton.className = "edit-btn";
        editButton.addEventListener("click", () => {
            editEntry(entry, {
                amount,
                currency,
                category,
                isExpense,
                month
            })
        });

        const deleteButton = document.createElement("button");
        deleteButton.innerHTML = "❌</br>Delete";
        deleteButton.className = "delete-btn";
        deleteButton.addEventListener("click", () => {
            const confirmDelete = confirm("Are you sure you want to delete this entry?");
            if(confirmDelete){
                entry.remove();
            }
        });

        entry.appendChild(textSpan);
        entry.appendChild(editButton);
        entry.appendChild(deleteButton);

        if(isEditing && currentEditingElement){
            currentEditingElement.remove();
            isEditing = false;
            currentEditingElement = null;
        }

        entriesList.appendChild(entry);
        form.reset();
        document.getElementById("entryToggle").checked = false;
    });

    function editEntry(entryElement, data) {
        // Попълваме формата с данните на записа
        document.getElementById("amount").value = data.amount;
        document.getElementById("currency").value = data.currency;
        document.getElementById("category").value = data.category;
        document.getElementById("entryToggle").checked = data.isExpense;
        document.getElementById("month").value = data.month;

        isEditing = true;
        currentEditingElement = entryElement;
        form.scrollIntoView({behavior: "smooth"});
    }
});

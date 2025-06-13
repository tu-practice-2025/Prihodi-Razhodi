document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("enterData");
    const entriesList = document.getElementById("entriesList");

    form.addEventListener("submit", function (e) {
        e.preventDefault();

        const amount = document.getElementById("amount").value.trim();
        const category = document.getElementById("category").value;
        const start_date = document.getElementById("date").value;
        const end_date = document.getElementById("date").value;
        const isExpense = document.getElementById("entryToggle").checked;

        if (!amount || !category || !start_date || !end_date) {
            alert("Моля, попълнете всички полета.");
            return;
        }

        const type = isExpense ? "Разход" : "Приход";

        const entry = document.createElement("li");
        entry.classList.add(isExpense ? "expense-entry" : "income-entry");

        const textSpan = document.createElement("span");
        textSpan.innerHTML = `${type} - ${category}: ${amount} лв <br>Start date: ${start_date}<br>End date: ${end_date}`;

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
        editButton.addEventListener("click", () =>
            editEntry(entry, {
                amount,
                category,
                start_date,
                end_date,
                isExpense,
            })
        );

        const deleteButton = document.createElement("button");
        deleteButton.innerHTML = "❌</br>Delete";
        deleteButton.className = "delete-btn";
        deleteButton.addEventListener("click", () => entry.remove());

        entry.appendChild(textSpan);
        entry.appendChild(editButton);
        entry.appendChild(deleteButton);

        entriesList.appendChild(entry);
        form.reset();
        document.getElementById("entryToggle").checked = false;
    });

    function editEntry(entryElement, data) {
        // Попълваме формата с данните на записа
        document.getElementById("amount").value = data.amount;
        document.getElementById("category").value = data.category;
        document.getElementById("start_date").value = data.date;
        document.getElementById("end").value = data.date;
        document.getElementById("entryToggle").checked = data.isExpense;

        // Премахваме стария елемент
        entryElement.remove();
    }
});

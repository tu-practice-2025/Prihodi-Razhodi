document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("enterData");
    const entriesList = document.getElementById("entriesList");

    const API_BASE_URL = "https://localhost:7121/api/budget";
    const USER_ID = 1;
    const YEAR = 2025;

    let isEditing = false;
    let currentEditingElement = null;

    // Load existing entries
    function loadEntries() {
        fetch(`${API_BASE_URL}/${USER_ID}`)
            .then((res) => {
                if (!res.ok)
                    throw new Error(`Failed to load entries: ${res.status}`);
                return res.json();
            })
            .then((data) => {
                entriesList.innerHTML = "";
                data.forEach((entry) => addEntryToDOM(entry));
            })
            .catch((error) => {
                console.error("Error loading entries:", error);
                // alert("Could not load entries. See console for details.");
            });
    }
    loadEntries();

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

        if (isNaN(parsedAmount) || isNaN(parsedMonth)) {
            alert("Amount and month must be valid numbers.");
            return;
        }

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
            })
                .then((response) => {
                    if (!response.ok)
                        throw new Error(
                            `Failed to update entry: ${response.status}`
                        );
                    return JSON.stringify(response);
                })
                .then(() => {
                    loadEntries();
                    isEditing = false;
                    currentEditingElement = null;
                    form.reset();
                    document.getElementById("entryToggle").checked = false;
                })
                .catch((error) => {
                    console.error(error);
                    //alert("Failed to update entry.");
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
            })
                .then((response) => {
                    if (!response.ok)
                        throw new Error(
                            `Failed to add entry: ${response.status}`
                        );
                    return response.json();
                })
                .then(() => {
                    loadEntries();
                    form.reset();
                    document.getElementById("entryToggle").checked = false;
                })
                .catch((error) => {
                    console.error(error);
                    // alert("Failed to add entry.");
                });
        }
    });

    function addEntryToDOM(entry) {
        const li = document.createElement("li");
        li.classList.add(entry.currency ? "expense-entry" : "income-entry");
        li.dataset.id = entry.id;

        // Clear previous content
        li.textContent = "";

        // Create text content with line break safely
        const textSpan = document.createElement("span");
        textSpan.textContent = `${entry.currency ? "Expense" : "Income"} - ${
            entry.categoryDescription
        }: ${entry.amount} ${entry.currency} Planned for: ${entry.month}`;

        const br = document.createElement("br");

        // Edit button
        const editButton = document.createElement("button");
        editButton.innerHTML = "✏️ Edit";
        editButton.className = "edit-btn";
        editButton.addEventListener("click", () => {
            document.getElementById("amount").value = entry.amount;
            document.getElementById("currency").value = entry.currency;
            document.getElementById("category").value = entry.categoryCode; // use code
            document.getElementById("month").value = entry.month;
            document.getElementById("entryToggle").checked = !!entry.currency;

            isEditing = true;
            currentEditingElement = li;
            form.scrollIntoView({ behavior: "smooth" });
        });

        // Delete button
        const deleteButton = document.createElement("button");
        deleteButton.innerHTML = "❌ Delete";
        deleteButton.className = "delete-btn";
        deleteButton.addEventListener("click", () => {
            if (confirm("Are you sure you want to delete this entry?")) {
                fetch(`${API_BASE_URL}/${entry.id}`, {
                    method: "DELETE",
                })
                    .then((response) => {
                        if (!response.ok)
                            throw new Error(
                                `Failed to delete entry: ${response.status}`
                            );
                        li.remove();
                    })
                    .catch((error) => {
                        console.error("Delete error:", error);
                        //alert("Failed to delete entry.");
                    });
            }
        });

        li.appendChild(textSpan);
        li.appendChild(br);
        li.appendChild(editButton);
        li.appendChild(deleteButton);
        entriesList.appendChild(li);
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("enterData");
    const entriesList = document.getElementById("entriesList");

    const API_BASE_URL = "https://localhost:7121/api/budget";
    const USER_ID = 1;
    const YEAR = 2025;

    let isEditing = false;
    let currentEditingElement = null;

    const categoryCodeToEnglish = {
        "REST": "Food",
        "TRPT": "Transport",
        "HOME": "Household",
        "HLTH": "Health",
        "EDUC": "Education",
        "CLTH": "Clothes",
        "SPRT": "Lifestyle"
    };

    // Load existing entries
    function loadEntries() {
        const month = sessionStorage.getItem("month");
        const monthParam = month ? `?month=${month}` : "";

        fetch(`${API_BASE_URL}/${USER_ID}${monthParam}`)
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
            });
    }
    loadEntries();

    form.addEventListener("submit", function (e) {
        e.preventDefault();

        const amount = document.getElementById("amount").value.trim();
        const category = document.getElementById("category").value;
        const currency = "BGN";

        if (!amount || !category) {
            alert("Please, complete all required fields.");
            return;
        }

        const parsedAmount = parseFloat(amount);

        if (isNaN(parsedAmount)) {
            alert("Amount must be a valid number.");
            return;
        }

        if (isEditing && currentEditingElement) {
            const payload = {
                id: parseInt(currentEditingElement.dataset.id),
                amount: parsedAmount,
                currency: currency,
                categoryCode: category,
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
                        throw new Error(`Failed to update entry: ${response.status}`);
                    return response.json();
                })
                .then(() => {
                    loadEntries();
                    isEditing = false;
                    currentEditingElement = null;
                    form.reset();
                })
                .catch((error) => {
                    console.error(error);
                });
        } else {
            const postPayload = {
                amount: parsedAmount,
                currency: currency,
                categoryCode: category,
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
                        throw new Error(`Failed to add entry: ${response.status}`);
                    return response.json();
                })
                .then(() => {
                    loadEntries();
                    form.reset();
                })
                .catch((error) => {
                    console.error(error);
                });
        }
    });

    function addEntryToDOM(entry) {
        const li = document.createElement("li");
        li.classList.add("expense-entry");
        li.dataset.id = entry.id;

        const textSpan = document.createElement("span");
        const monthNames = [
            "", "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        ];
        const monthName = monthNames[entry.month];

        const englishCategory = categoryCodeToEnglish[entry.categoryCode] || entry.categoryDescription;

        textSpan.textContent = `Expense - ${englishCategory}: ${entry.amount} ${entry.currency} Planned for: ${monthName}`;

        const br = document.createElement("br");

        const editButton = document.createElement("button");
        editButton.innerHTML = "✏️ Edit";
        editButton.className = "edit-btn";
        editButton.addEventListener("click", () => {
            document.getElementById("amount").value = entry.amount;
            document.getElementById("category").value = entry.categoryCode;

            isEditing = true;
            currentEditingElement = li;
            form.scrollIntoView({ behavior: "smooth" });
        });

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
                            throw new Error(`Failed to delete entry: ${response.status}`);
                        li.remove();
                    })
                    .catch((error) => {
                        console.error("Delete error:", error);
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

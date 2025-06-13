const entriesList = document.getElementById("entriesList");

function addEntry() {
    const amount = parseFloat(document.getElementById("amount").value);
    const category = document.getElementById("category").value;
    const date = document.getElementById("date").value;
    const description = document.getElementById("description").value;

    if (isNaN(amount) || !category || !date) {
        alert("Please fill out all fields correctly.");
        return;
    }

    const li = document.createElement("li");
    li.className = "entry";

    const infoDiv = document.createElement("div");
    infoDiv.className = "info";
    infoDiv.innerHTML = `<strong>${category.replace("-", " ")}</strong><br>${date}<br>${description}`;

    const amountDiv = document.createElement("div");
    amountDiv.className = "amount " + (category.includes("income") ? "income" : "expense");
    amountDiv.innerText = (category.includes("income") ? "+ $" : "- $") + Math.abs(amount).toFixed(2);

    const deleteBtn = document.createElement("button");
    deleteBtn.innerHTML = '<img src="delete.webp" alt="Delete" style="width: 20px; height: 20px;">';
    deleteBtn.onclick = () => entriesList.removeChild(li);

    const editBtn = document.createElement("button");
    editBtn.innerText = '<img src="edit.png" alt="Edit" style="width: 20px; height: 20px;">';
    editBtn.onclick = () => alert("Edit functionality not implemented.");

    li.appendChild(infoDiv);
    li.appendChild(amountDiv);
    li.appendChild(deleteBtn);
    li.appendChild(editBtn);

    entriesList.prepend(li);
}
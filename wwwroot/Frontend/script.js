const url_api = "/api/home";
let selectedUser = null;

document.addEventListener("DOMContentLoaded", function () {
  loadUsers();

  document.getElementById("addBtn").addEventListener("click", showAddForm);
  document.getElementById("editBtn").addEventListener("click", showEditForm);
  document.getElementById("deleteBtn").addEventListener("click", confirmDelete);
  document.getElementById("userForm").addEventListener("submit", saveUser);
  document
    .getElementById("confirmDeleteBtn")
    .addEventListener("click", deleteUser);
});

function loadUsers() {
  fetch(url_api)
    .then((response) => response.json())
    .then((data) => {
      const user_table = document.getElementById("tbody_users");
      user_table.innerHTML = "";
      var myCounter = 1;
      data.forEach((user) => {
        const row = document.createElement("tr");
        row.innerHTML = `
                    <td style="display: none;">${user.id}</td>
										<td>${myCounter}</td>
                    <td>${user.firstName}</td>
                    <td>${user.lastName}</td>
                    <td>${user.email}</td>
                `;
        row.addEventListener("click", () => selectUser(user, row));
        user_table.appendChild(row);
        myCounter++;
      });
    });
}

function selectUser(user, row) {
  selectedUser = user;
  hideAddForm();
  // Highlight selected row
  document
    .querySelectorAll("tr")
    .forEach((tr) => tr.classList.remove("table-active"));
  row.classList.add("table-active");

  // Enable buttons
  document.getElementById("editBtn").disabled = false;
  document.getElementById("deleteBtn").disabled = false;
}

function showAddForm() {
  clearForm();
  document.getElementById("form_container").style.display = "block";
}
function hideAddForm() {
  clearForm();
  document.getElementById("form_container").style.display = "none";
}

function showEditForm() {
  if (selectedUser) {
    document.getElementById("id").value = selectedUser.id;
    document.getElementById("firstName").value = selectedUser.firstName;
    document.getElementById("lastName").value = selectedUser.lastName;
    document.getElementById("email").value = selectedUser.email;
    document.getElementById("form_container").style.display = "block";
  }
}

function saveUser(event) {
  event.preventDefault();

  const id = document.getElementById("id").value;

  const firstName = document.getElementById("firstName").value;
  const lastName = document.getElementById("lastName").value;
  const email = document.getElementById("email").value;

  if (id) {
    // Edit user (PUT)
    const user = { id, firstName, lastName, email };
    fetch(url_api, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(user),
    }).then((response) => {
      if (response.ok) {
        loadUsers();
        clearForm();
        hideAddForm();
      }
    });
  } else {
    // Add new user (POST)
    const user = { firstName, lastName, email };
    fetch(url_api, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(user),
    }).then((response) => {
      if (response.ok) {
        loadUsers();
        clearForm();
        hideAddForm();
      }
    });
  }
}

function confirmDelete() {
  const deleteModal = new bootstrap.Modal(
    document.getElementById("deleteModal")
  );
  deleteModal.show();
}

function deleteUser() {
  const userId = selectedUser.id;
  

  fetch(`${url_api}/${userId}`, {
    method: "DELETE",
  }).then((response) => {
    if (response.ok) {
      loadUsers();
      selectedUser = null;
      document.getElementById("editBtn").disabled = true;
      document.getElementById("deleteBtn").disabled = true;
    } else {
      console.error("Failed to delete user:", response.statusText);
    }
  });

  const deleteModal = bootstrap.Modal.getInstance(
    document.getElementById("deleteModal")
  );
  deleteModal.hide();
}

function clearForm() {
  document.getElementById("id").value = "";
  document.getElementById("firstName").value = "";
  document.getElementById("lastName").value = "";
  document.getElementById("email").value = "";
  document.getElementById("form_container").style.display = "none";
}

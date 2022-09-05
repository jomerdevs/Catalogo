window.onload = () => {
    userList();

}

function userList() {

    // We make this function dinamic in generic.js file, so it can be reused in any view that requires to list data inside a table
    showList({
        url: "http://localhost:6700/User/List",
        id: "userTable",
        headers: ["First Name", "Last Name", "Email", "Phone"],
        properties: ["FirstName", "LastName", "Email", "Phone"]
    });

}

function Search() {

    let search = document.getElementById("filterUserInput").value;

    showList({
        url: "http://localhost:6700/User/FilterUserList/?search=" + search,
        id: "userTable",
        headers: ["First Name", "Last Name", "Email", "Phone"],
        properties: ["FirstName", "LastName", "Email", "Phone"]

    });
}
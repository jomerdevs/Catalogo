window.onload = () => {
    CategoryList();

}

function CategoryList() {

    // We make this function dinamic in generic.js file, so it can be reused in any view that requires to list data inside a table
    showList({
        url: "http://localhost:6700/UserCategory/ListCat",
        id: "categoryTable",
        headers: ["Category Name", "Description", "Created"],
        properties: ["Name", "Description", "Created"]
    });

}

function Search() {

    let search = document.getElementById("filterCategoryInput").value;

    showList({
        url: "http://localhost:6700/UserCategory/FilterCategoryList/?search=" + search,
        id: "categoryTable",
        headers: ["Category Name", "Description", "Created"],
        properties: ["Name", "Description", "Created"]

    });
}
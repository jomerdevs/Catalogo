window.onload = () => {
    CategoryList();

}

function CategoryList() {

    // We make this function dinamic in generic.js file, so it can be reused in any view that requires to list data inside a table
    showList({
        url: "UserCategory/ListCat",
        id: "categoryTable",
        headers: ["Category Name", "Description", "IsActive", "Created"],
        properties: ["Name", "Description", "IsActive", "Created"]
    });

}

function Search() {

    let search = document.getElementById("filterCategoryInput").value;

    showList({
        url: "UserCategory/FilterCategoryList/?search=" + search,
        id: "categoryTable",
        headers: ["Category Name", "Description", "IsActive", "Created"],
        properties: ["Name", "Description", "IsActive", "Created"]

    });
}
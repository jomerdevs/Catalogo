window.onload = () => {
    productList();

}

function productList() {
    
    // We make this function dinamic in generic.js file, so it can be reused in any view that requires to list data inside a table
    showList({
        url: "http://localhost:6700/UserProduct/List",
        id: "productTable",
        headers: ["Id","Reference Code", "Product Name", "Model", "Price", "Brand",]       
    });

}

function Search(){

    let search = document.getElementById("filterProductsInput").value;

    showList({
        url: "http://localhost:6700/UserProduct/FilterProductsList/?search=" + search,
        id: "productTable",
        headers: ["Id", "Reference Code", "Product Name", "Model", "Price", "Brand","Category Id"],
        properties: ["Id", "RefCode", "Name", "Model", "Price", "Brand", "CategoryId"]
        
    });
}
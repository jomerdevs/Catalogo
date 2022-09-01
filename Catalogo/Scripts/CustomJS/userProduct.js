window.onload = function(){
    ProductList();   
}

function ProductList() {
    
    // We make this function dinamic in generic.js file, so it can be reused in any view that requires to list data inside a table
    showList({
        url: "UserProduct/List",
        id: "productTable",
        headers: ["Id","Reference Code", "Product Name", "Model", "Price", "Brand", "Category"],
        properties: ["Id","RefCode", "Name", "Model", "Price", "Brand", "Category"]       
    });

}

function Search(){

    let search = document.getElementById("filterProductsInput").value;

    showList({
        url: "UserProduct/FilterProductsList/?search=" + search,
        id: "productTable",
        headers: ["Id","Reference Code", "Product Name", "Model", "Price", "Brand", "Category"],
        properties: ["Id","RefCode", "Name", "Model", "Price", "Brand", "Category"]
        
    });
}
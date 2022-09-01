function showList(configurationObj){        

    // Controller/Action
    fetch(configurationObj.url)
        .then(res => res.json())
        .then(res => {
            let content = "<table class='table table-striped'>";
            content += "<thead>";
            content += "<tr class='text-center'>";

            for (let j = 0; j < configurationObj.headers.length; j++) {
                content += "<th>" + configurationObj.headers[j] + "</th>";
            }

            content += "</tr>";
            content += "</thead>";
            content += "<tbody>";

            let row;
            let property;
            for (let i = 0; i < res.length; i++) {
                row = res[i];
                content += "<tr class='text-center'>";

                for (let j = 0; j < configurationObj.properties.length; j++) {
                    property = configurationObj.properties[j]
                    content += "<td>" + row[property] + "</td>";
                }

                content += "</tr>";
            }
            content += "</tbody>";

            content += "</table>";

            document.getElementById(configurationObj.id).innerHTML = content;
        })
}


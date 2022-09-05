function showList(configurationObj){        

    // Controller/Action
    $.ajax({
        type: 'GET',
        url: configurationObj.url,
        contentType: 'application/json',
        success: function (res) {

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
                    if (property === 'Created') {
                        row[property] = Date(row[property])
                        content += "<td>" + row[property] + "</td>";
                    } else {
                    content += "<td>" + row[property] + "</td>";
                    }
                                       
                }

                content += "</tr>";
            }
            content += "</tbody>";

            content += "</table>";

            document.getElementById(configurationObj.id).innerHTML = content;

            console.log(res);
        }, error: function (error) {
            console.log(error);
        }
    })    
}


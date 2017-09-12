$(document).ready(function () {
    $("#grid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    dataType: "json",
                    url: "/Book/Books_Read",
                }
            },
            pageSize: 20,
        },
        height: 550,
        groupable: true,
        sortable: true,
        scorable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [{
            field: "Author",
            title: "Author",
            width: "200px"
        }, {
            field: "Name",
            title: "Name",
            width: "200px"
        }, {
            type: "date", format: "{0:yyyy}",
            field: "YearOfPublishing",
            title: "Year of publishing",
            width: "200px"
        }, {
            field: "PublishingHouses",
            title: "Publishing Houses",
            width: "200px"
        }, {
            template: "<a href='/Book/Edit/#:data.Id#'>" +
                "Edit" + 
                "</a>",
            title: "Edit",
            width: "100px"
        }, {
            template: "<a href='/Book/Delete/#:data.Id#'>" +
                "Delete" +
                "</a>",
            title: "Delete",
            width: "100px"
        }]
    });
});
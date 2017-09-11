$(document).ready(function () {
    $("#grid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    dataType: "json",
                    url: "/PublishingHouse/PublishingHouses_Read",
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
            field: "Name",
            title: "Name",
            width: "200px"
        }, {
            field: "Address",
            title: "Address",
            width: "200px"
        }, {
            field: "Books",
            title: "Books",
            width: "200px"
        }, {
            template: "<a href='/PublishingHouse/Edit/#:data.Id#'>" +
                "Edit" +
                "</a>",
            title: "Edit",
            width: "100px"
        }, {
            template: "<a href='/PublishingHouse/Delete/#:data.Id#'>" +
                "Delete" +
                "</a>",
            title: "Delete",
            width: "100px"
        }]
    });
});
$(document).ready(function () {
    $("#grid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    dataType: "json",
                    url: "/Magazine/Magazines_Read",
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
            title: "Name"
        }, {
            field: "Number",
            title: "Number"
        }, {
            type: "date", format: "{0:yyyy}",
            field: "YearOfPublishing",
            title: "Year of publishing",
        }]
    });
});
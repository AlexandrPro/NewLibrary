$(document).ready(function () {
    $("#grid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    dataType: "json",
                    url: "/Brochure/Brochures_Read",
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
        columns: [ {
            field: "Name",
            title: "Name"
        }, {
            field: "TypeOfCover",
            title: "Type of cover"
        }, {
            field: "NumberOfPages",
            title: "Number of pages"
        }, {
            template: "<a href='/Brochure/Edit/#:data.Id#'>" +
                "Edit" +
                "</a>",
            title: "Edit",
        }, {
            template: "<a href='/Brochure/Delete/#:data.Id#'>" +
                "Delete" +
                "</a>",
            title: "Delete",
        }]
    });
});
﻿$(document).ready(function () {
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
            title: "Author"
        }, {
            field: "Name",
            title: "Name"
        }, {
            type: "date", format: "{0:yyyy}",
            field: "YearOfPublishing",
            title: "Year of publishing",
        }, {
            template: "<a href='/Book/Edit/#:data.Id#'>" +
                "Edit" + 
                "</a>",
            title: "Edit",
        }, {
            template: "<a href='/Book/Delete/#:data.Id#'>" +
                "Delete" +
                "</a>",
            title: "Delete",
        }]
    });
});
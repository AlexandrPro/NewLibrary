$(document).ready(function () {
    $("#multiSelect").kendoMultiSelect({
        placeholder: "Select products...",
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: {
            transport: {
                read: {
                    dataType: "json",
                    url: "/Book/PublishingHouses_Read",
                }
            }
        },
    });
});
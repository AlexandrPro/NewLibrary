$(document).ready(function () {

    //alert(JSON.stringify(selectedValues));

    $("#multiSelect").kendoMultiSelect({
        placeholder: "Select products...",
        dataTextField: "Name",
        dataValueField: "Id",
        dataSource: {
            transport: {
                read: {
                    dataType: "json",
                    url: "/PublishingHouse/Books_Read",
                }
            }
        },
        value: selectedValues,
    });
});

var selectedValues = $.ajax({
    type: "POST",
    dataType: "json",
    url: "/PublishingHouse/SelectedBooks_Read",
    data: { id: GetCurentPuclishingHouseId() },
});
selectedValues.done(function (data) {
    $("#multiSelect").data("kendoMultiSelect").value(data);
});


function GetCurentPuclishingHouseId() {
    var url = window.location.href;
    var curentId = url.substr(url.lastIndexOf('/') + 1, url.length);
    //alert(curentId);
    return curentId;
}
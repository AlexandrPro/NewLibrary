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
                    url: "/Book/PublishingHouses_Read",
                }
            }
        },
        value: selectedValues,
    });
});

var selectedValues = $.ajax({
    type: "POST",
    dataType: "json",
    url: "/Book/SelectedPublishingHouses_Read",
    data: { id: GetCurentBookId() },
});
selectedValues.done(function (data) {
    $("#multiSelect").data("kendoMultiSelect").value(data);
});


function GetCurentBookId() {
    var url = window.location.href;
    var curentId = url.substr(url.lastIndexOf('/') + 1, url.length);
    return curentId;
}
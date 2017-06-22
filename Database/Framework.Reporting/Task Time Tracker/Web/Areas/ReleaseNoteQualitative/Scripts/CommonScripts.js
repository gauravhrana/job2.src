
function GetSearchQuery(areaName, entityName) {
    var searchQuery = "";

    var searchControls = $("#searchFilterContainer .search-filter-control");
    if (searchControls.length > 0) {
        searchControls.each(function (idx, searchControlItem) {

            if (searchControlItem.name != "undefined" && searchControlItem.name != "GroupBy" && searchControlItem.name != "SubGroupBy") {
                //case for textbox
                if (searchControlItem.type == 'text') {
                    searchQuery += searchControlItem.name + "=" + searchControlItem.value + "&";
                }
                else {
                    searchQuery += searchControlItem.name + "=" + searchControlItem.value + "&";
                }
            }
        });
    }
    return searchQuery;
}

function SubmitSearch() {

    var searchQuery = GetSearchQuery(areaName, entityName);

    var requestUrl = "/" + areaName + "/" + entityName + "/IndexResult?" + searchQuery;

    // Send an AJAX request
    $.getJSON(requestUrl)
        .done(function (data) {

            //Selecting existing grid
            var kendoGrid = $("#grid").data("kendoGrid");
            var datasource = kendoGrid.dataSource;

            //Applying new source
            datasource.data(data);

        })
        .fail(function (jqXHR, textStatus, err) {
            alert('Error: ' + err);
        });
}

function ManageButtonPanel(flag) {
    if (flag) {
        $('#btnDelete').removeAttr('disabled');
        $('#btnDetails').removeAttr('disabled');
        $('#btnUpdate').removeAttr('disabled');
    } else {
        $('#btnDelete').attr('disabled', 'disabled');
        $('#btnDetails').attr('disabled', 'disabled');
        $('#btnUpdate').attr('disabled', 'disabled');
    }
}

function CheckWithHeader() {
    if ($('#grid .check-box').length == $('#grid .check-box:checked').length) {
        $('#checkAll').prop("checked", true);
    }
    else {
        $('#checkAll').prop("checked", false);
    }
    if ($('#grid .check-box:checked').length > 0) {
        ManageButtonPanel(true);
    } else {
        ManageButtonPanel(false);
    }
}

function OnInsert() {
    window.location.replace("/" + areaName + "/" + entityName + "/Create");
}

function OnDelete() {
    GenerateSuperKey(areaName, entityName, "Delete");
}

function OnDetails() {
    GenerateSuperKey(areaName, entityName, "Details");

}

function OnUpdate() {
    GenerateSuperKey(areaName, entityName, "Edit");
}

function GenerateSuperKey(areaName, entityName, action) {
    var superKeyId = "";
    var Ids = "";

    var checkedItems = $('#grid .check-box:checked');
    if (checkedItems.length > 0) {
        checkedItems.each(function (idx, chkBox) {
            Ids += chkBox.value + ", ";
        });

        // logic to change the columns according to the changed FC mode
        var requestUrl = "/" + areaName + "/" + entityName + "/GenerateSuperKey?Ids=" + Ids;

        // Send an AJAX request
        $.getJSON(requestUrl)
            .done(function (data) {
                superKeyId = data;

                window.location.replace("/" + areaName + "/" + entityName + "/" + action + "/" + superKeyId + "/true");

                return superKeyId;
            })
            .fail(function (jqXHR, textStatus, err) {
                alert('Error: ' + err);
            });
    }
}
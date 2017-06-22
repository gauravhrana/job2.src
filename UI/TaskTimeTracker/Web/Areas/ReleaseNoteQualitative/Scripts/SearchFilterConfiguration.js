
function ConfigureSearchFilter(searchFilterContainer) {
    var requestUrl = "/" + areaName + "/" + entityName + "/GetSearchFilterColumns";

    // Send an AJAX request
    $.getJSON(requestUrl)
        .done(function (data) {

            if (data.length > 0) {

                for (var i = 0; i < data.length; i++) {

                    // create row div
                    var $divParent = $("<div>", { id: "searchFiterRow" + i, class: "search-filter-row" });

                    // create label div
                    var $divLabel = $("<div>", { id: "searchFiterlabel" + i, class: "search-filter-label", style: "float: left; padding-right: 10px;" });
                    $divLabel.html(data[i].FieldConfigurationDisplayName + ": ");
                    $divLabel.width("200px");

                    // append label div
                    $divParent.append($divLabel);

                    // create contol container div
                    var $divControl = $("<div>", { id: "searchFiterControlContainer" + i, class: "search-filter-contol-container" });

                    var searchInputControl = CreateSearchFilterControl(data[i].ControlType, data[i].Name, entityName);
                    $divControl.append(searchInputControl);

                    // append container div to row  div
                    $divParent.append($divControl);

                    // append row div to container
                    searchFilterContainer.append($divParent);
                    //searchFilterContainer.append("<br/>");
                }

                // this variable should only be declared if search filter contains a date panel
                if (typeof userDateFormat != 'undefined'){
                    $("input[myAttr='DatePicker']").kendoDatePicker({
                        format: userDateFormat
                    });
                }
            }

        })
        .fail(function (jqXHR, textStatus, err) {
            alert('Error: ' + err);
        });

}

function CreateSearchFilterControl(controlType, controlName, entityName) {



    // create proper input control and append to container div
    if (controlType == "TextBox") {
        var txtBox = $("<input>", { type: "text", width: "200px", id: "txt" + controlName, class: "search-filter-control", name: controlName });
        return txtBox;
    }
    else if (controlType == "DropDownList") {

        var dropDownList = $('<select />', { id: "drp" + controlName, class: "search-filter-control", name: controlName });
        dropDownList.width("200px");        

        if (controlName == "GroupBy" || controlName == "SubGroupBy") {

            // add 'None' option first
            $('<option />', { value: "-1", text: "None" }).appendTo(dropDownList);

            $.ajax({
                type: "POST",
                url: "/API/AutoComplete.asmx/GetGroupByList",
                data: "{entityName:'" + entityName + "', mode : '10019'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async:   false,
                success: function (msg) {
                    if (msg.d.length > 0) {
                        var data = msg.d;

                        // add options to drop down list box
                        for (var i = 0; i < data.length; i++) {
                            $('<option />', { value: data[i].Name, text: data[i].FieldConfigurationDisplayName}).appendTo(dropDownList);
                        }
                    }
                }
            });
        }
        else {

            // add 'All' option first
            $('<option />', { value: "-1", text: "All" }).appendTo(dropDownList);

            $.ajax({
                type: "POST",
                url: "/API/AutoComplete.asmx/GetAutoCompleteListForDropDown",
                data: "{dropDownName : '" + controlName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (msg) {
                    if (msg.d.length > 0) {
                        var data = msg.d;

                        // add options to drop down list box
                        for (var i = 0; i < data.length; i++) {
                            $('<option />', { value: data[i].Value, text: data[i].Text }).appendTo(dropDownList);
                        }
                    }
                }
            });

        }

        return dropDownList;       

    }
    else if (controlType == "DatePanel") {

        // create contol container div
        var divControl = $("<div>");

        var txtFromDate = $("<input>", { type: "text", width: "120px", myAttr: 'DatePicker', id: "dtPickerFrom" + controlName, class: "search-filter-control", name: controlName + "Min" });
        var txtToDate = $("<input>", { type: "text", width: "120px", myAttr: 'DatePicker', id: "dtPickerTo" + controlName, class: "search-filter-control", name: controlName + "Max" });
        
        divControl.append(txtFromDate);
        divControl.append(txtToDate);

        return divControl;
    }

}

function MangeColumnVisibilityByFCMode() {

    var searchQuery = GetSearchQuery(areaName, entityName);
    var fcModeId = $('#drpFCMode').val();

    // logic to change the columns according to the changed FC mode
    var requestUrl = "/" + areaName + "/" + entityName + "/ReloadKendoGridConfiguration?fcModeId=" + fcModeId + "&" + searchQuery;

    // Send an AJAX request
    $.getJSON(requestUrl)
        .done(function (data) {

            var grid = $("#grid").data("kendoGrid");

            for (var i = 0; i < grid.columns.length; i++) {

                // if not template column then only manage visibility
                if (grid.columns[i].title != " ") {
                    var fieldName = grid.columns[i].field;

                    var isVisible = false;

                    var grid = $("#grid").data("kendoGrid");

                    // check if column exists in visible columns returned from server or not.
                    $.each(data, function (i, elem) {
                        if (elem === fieldName) {
                            isVisible = true;
                        }
                    });

                    if (isVisible) {
                        grid.showColumn(fieldName);
                    }
                    else {
                        grid.hideColumn(fieldName);
                    }
                }
            }

        })
        .fail(function (jqXHR, textStatus, err) {
            alert('Error: ' + err);
        });
}

function SetupTabs(hidTabClientId) {

    var tabIndex = 0;
    tabIndex = $(this).find("#" + hidTabClientId).val();

    if (tabIndex == null) {
        tabIndex = 0;
    }

    var activeIndex = parseInt(tabIndex) + 1;

    $(".searchTable tr").hide();

    $("#tabs").tabs({
        active: tabIndex,
        activate: function (event, ui) {

            tabIndex = $("#tabs").tabs('option', 'active');

            $("#" + hidTabClientId).val(tabIndex);

            if (ui.newTab[0].id.indexOf("liAll") >= 0) {
                //$("[class*=DisplayColumn]").show();

                // show all rows including hidden
                $(".searchTable tr").show();
            }
            else if (ui.newTab[0].id.indexOf("liMISC") >= 0) {
                $("[class*=hiddebByUP]").show();
            }
            else {
                activeIndex = parseInt(tabIndex) + 1;
                $(".DisplayColumn" + activeIndex).show();
                $(".DisplayColumn" + activeIndex + " tr").show();
            }
        },

        beforeActivate: function (event, ui) {
            $(".searchTable tr").hide();
        }
    });

    //var tabLength = $('#tabs >ul >li').size();
    var tabLength = $('#tabs >ul >li').length;

    // All tab selected
    if (activeIndex == tabLength) {
        $(".searchTable tr").show();
    }
    else if (activeIndex == (parseInt(tabLength) - 1)) {
        $("[class*=hiddebByUP]").show();
    }
    else {
        $(".DisplayColumn" + activeIndex).show();
        $(".DisplayColumn" + activeIndex + " tr").show();
    }

    $("#tabs").removeClass("ui-widget ui-widget-content ui-corner-all");
}

function HideSearchControlWithRowId(rowId, name, settingCateogry) {

    $("#" + rowId).removeAttr('class').attr('class', 'hiddebByUP');
    $("#" + rowId + " .hoverLinkCheckBox").hide().removeClass("hoverLinkCheckBox");
    $("#" + rowId).hide();

    $.ajax({
        type: "POST",
        url: "/Default.aspx/UpdateSearchControlParameterVisibility",
        data: "{'name': '" + name + "', 'category': '" + settingCateogry + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnGetMemberSuccess,
        error: OnGetMemberError
    });

}

function OnGetMemberSuccess(data, status) {
}

function OnGetMemberError(request, status, error) {
    alert("error updating preference: " + error);
}

function SetDevBoxValue(kendoDropDownId, devTextBoxId) {

    var kendoDropDown = document.getElementById(kendoDropDownId);
    var devTextBox = document.getElementById(devTextBoxId);
    if (devTextBox != null && kendoDropDown != null) {
        devTextBox.value = kendoDropDown.value;
    }
}
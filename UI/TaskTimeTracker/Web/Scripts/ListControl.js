function CheckChanged(chkBox, gridViewId, buttonPanelId) {

    // check number of checked check boxes
    var numChecked = $("#" + gridViewId + " input[type=checkbox]:checked").length;
    
    if (numChecked > 0 || chkBox.checked == true) {
        // enable all buttons
        $("#" + buttonPanelId + " input[type=submit]").removeAttr("disabled");
        // change color to red
        $("#" + buttonPanelId + " input[type=submit]").css("background", "#B40404");
    }
    else {
        // disable all buttons
        $("#" + buttonPanelId + " input[type=submit]").attr("disabled", "disabled");
        // change color to grey
        $("#" + buttonPanelId + " input[type=submit]").css("background", "#808080");

        // enable insert button
        $("#" + buttonPanelId + " input[name*='ButtonInsert']").removeAttr("disabled");
        // change color to red
        $("#" + buttonPanelId + " input[name*='ButtonInsert']").css("background", "#B40404");
    }
}

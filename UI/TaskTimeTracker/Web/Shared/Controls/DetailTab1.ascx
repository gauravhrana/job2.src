<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailTab1.ascx.cs" Inherits="Shared.UI.Web.Controls.DetailTab1Control" %>


<div id="divContainer" runat="server" >
    
</div>

<asp:HiddenField ID="hdnAccordionIndex" Value="0" runat="server" />

<script>


    $(document).ready(function () {

        $("#<%=divContainer.ClientID%>").accordion({
            heightStyle: "content",
            collapsible: true,
            activate: function (event, ui) {
                activeIndex = $("#<%=divContainer.ClientID%>").accordion("option", "active");
                    $("#<%=hdnAccordionIndex.ClientID%>").val(activeIndex);
                },
                beforeActivate: function (event, ui) {

                    // The accordion believes a panel is being opened
                    if (ui.newHeader[0]) {
                        var currHeader = ui.newHeader;
                        var currContent = currHeader.next('.ui-accordion-content');
                        // The accordion believes a panel is being closed
                    } else {
                        var currHeader = ui.oldHeader;
                        var currContent = currHeader.next('.ui-accordion-content');
                    }

                    // Since we've changed the default behavior, this detects the actual status
                    var isPanelSelected = currHeader.attr('aria-selected') == 'true';

                    //alert(isPanelSelected);                   

                    var currentIndex = currHeader.attr("mytabindex")

                    // Toggle the panel's header
                    currHeader.toggleClass('ui-corner-all', isPanelSelected)
                        .toggleClass('accordion-header-active ui-state-active ui-corner-top', !isPanelSelected)
                        .attr('aria-selected', ((!isPanelSelected).toString()));

                    // Toggle the panel's icon
                    currHeader.children('.ui-icon')
                        .toggleClass('ui-icon-triangle-1-e', isPanelSelected)
                        .toggleClass('ui-icon-triangle-1-s', !isPanelSelected);

                    // Toggle the panel's content
                    currContent.toggleClass('accordion-content-active', !isPanelSelected)

                    if (isPanelSelected) {
                        currContent.slideUp();
                    }
                    else {
                        currContent.slideDown();
                    }

                    var finalIndexs = "";

                    var activeIndex = $('input[id$=hdnAccordionIndex]').val();

                    if (activeIndex == '') {
                        activeIndex = '0';
                    }

                    var isExists = false;
                    var arrIndexes = activeIndex.split(",");

                    $.each(arrIndexes, function (index, myTabIndex) {


                        if (myTabIndex == currentIndex) {
                            // if recently opened then only add in final indexes
                            if (!isPanelSelected) {
                                finalIndexs += myTabIndex + ",";

                            }
                            isExists = true;
                        }
                        else {
                            finalIndexs += myTabIndex + ",";
                        }
                    });

                    if (!isExists) {
                        finalIndexs += currentIndex + ",";
                    }

                    $('input[id$=hdnAccordionIndex]').val(finalIndexs);

                    return false; // Cancels the default action
                }
            });

        var activeIndex = $('input[id$=hdnAccordionIndex]').val();

        if (activeIndex == '') {
            activeIndex = '0';
        }

        // first accordion gets open by default
        var isFirstOpen = false;
        var arrIndexes = activeIndex.split(",");
        $.each(arrIndexes, function (index, myTabIndex) {
            if (myTabIndex != '') {

                // check if last time first accordion tab was open or not.
                if (myTabIndex == 0) {
                    isFirstOpen = true;
                }


                var selectedHeader = $("#<%=divContainer.ClientID%> [mytabindex='" + myTabIndex + "']");
                    var selectedContent = selectedHeader.next('.ui-accordion-content');

                    // Toggle the panel's header
                    selectedHeader.removeClass('ui-corner-all')
                        .addClass('accordion-header-active ui-state-active ui-corner-top')
                        .attr('aria-selected', 'true');

                    // Toggle the panel's icon
                    selectedHeader.children('.ui-icon')
                        .removeClass('ui-icon-triangle-1-e')
                        .addClass('ui-icon-triangle-1-s');

                    // Toggle the panel's content
                    selectedContent.addClass('accordion-content-active')

                    selectedContent.slideDown();

                }

            });

        // if first is not open then close it.
        if (!isFirstOpen) {

            var selectedHeader = $("#<%=divContainer.ClientID%> [mytabindex='0']");
                var selectedContent = selectedHeader.next('.ui-accordion-content');

                // Toggle the panel's header
                selectedHeader.addClass('ui-corner-all')
                    .removeClass('accordion-header-active ui-state-active ui-corner-top')
                    .attr('aria-selected', 'true');

                // Toggle the panel's icon
                selectedHeader.children('.ui-icon')
                    .addClass('ui-icon-triangle-1-e')
                    .removeClass('ui-icon-triangle-1-s');

                // Toggle the panel's content
                selectedContent.removeClass('accordion-content-active')

                selectedContent.slideUp();

            }
    });

</script>

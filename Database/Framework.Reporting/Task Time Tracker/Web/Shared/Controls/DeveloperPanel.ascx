<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeveloperPanel.ascx.cs"
    Inherits="Shared.UI.Web.Controls.DeveloperPanel" %>

<%@ Import Namespace="Shared.WebCommon.UI.Web" %>
 <style type="text/css">
      ul.tabs-menu {
          display: table;
          list-style-type: none;
          margin: 0;
          padding: 0;
      }

      ul.tabs-menu>li {
          float: left;
          padding: 10px;
          background-color: #0d77b6
      }

      ul.tabs-menu>li:hover {
          background-color: #afdefa;
          color:black
      }

      ul.tabs-menu>li.selected {
          background-color: #0d77b6;
          color:white;
      }

      div.tabContent {
          border: 1px solid black;
      }
     
    </style>

    <div class="menu-container-horizontal">
        <div style="float: left;">Developer Panel &nbsp;</div>
        <div id="closeBottom" class="close-container">[x]</div>
    </div>
  
<ul class='tabs-menu' style="text-wrap:none; font-size :medium; color:white; list-style-position: inside;
font-weight :bold;">
        
    <li id="tabs1" style= "border-style:solid; border-width: thin;" onclick="showStuff(this)">Application Path</li>
    <li id="tabs2" style= "border-style:solid; border-width: thin; " onclick="showStuff(this)">Application Info</li>
     </ul>
 
  <div id='tabs-1' class="tabContent">
     <div>
        <asp:Label ID="Label3" runat="server"  style="color:white"  Text="File Path: " CssClass="cbp-spmenu-horizontal-span" />
        <asp:Label ID="labelPath" runat="server" style="text-wrap:none; color:aqua; font-size :medium"></asp:Label> 
        <br /><br />
        <asp:Label ID="Label5" runat="server"  style="color:white" Text="Master Page: " CssClass="cbp-spmenu-horizontal-span" />
        <asp:Label ID="lblMaster" runat="server" style="text-wrap:none; color:aqua; font-size :medium"> </asp:Label>
       </div>
  </div>

  <div id="tabs-2" class="tabContent" style = "display : none">
   <div>
         <div style="width: 100%;">
                <div style="float: left; width: 25%; vertical-align: top;">
                    <asp:Label ID="Label2" runat="server"  style="color:white" Text="Application Mode: " CssClass="cbp-spmenu-horizontal-span" />
                    <asp:DropDownList ID="drpApplicationMode2" runat="server" style="color:black" AutoPostBack="true" OnSelectedIndexChanged="drpApplicationMode_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
           
                <div style="float: left; width: 25%; vertical-align: top;">
                    <asp:Label ID="Label1" runat="server" style="color:white" Text=" Embellishments: " CssClass="cbp-spmenu-horizontal-span" />
                                    <asp:DropDownList ID="drpGridLines" runat="server" style="color:black">
                        <asp:ListItem Value="True">True</asp:ListItem>
                        <asp:ListItem Value="False">False</asp:ListItem>
                    </asp:DropDownList>
            </div>
               

      <div style="vertical-align: top;"> 
        <asp:Label ID="Info" runat="server" Text="Mail Information:" CssClass="cbp-spmenu-horizontal-span" Font-Size="Medium"
             style="vertical-align: top; color:white"></asp:Label>
        <asp:ListBox ID="drpListBox" runat="server" style="color:black" SelectionMode="Multiple">
            <asp:ListItem Value="Email Session Info">Session Info</asp:ListItem>
            <asp:ListItem Value="Email Application Info">Application Info</asp:ListItem>
            <asp:ListItem Value="Email SQL Trace">SQL Trace Info</asp:ListItem>
        </asp:ListBox>
        <asp:Button ID="goButton" runat="server" Text="Send mail" Style="color: black" OnClick="goButton_Click" />
            </div>
        </div>

          
            </div>
  </div>
 
    
<script>

    function showStuff(element) {
        var tabContents = document.getElementsByClassName('tabContent');
        for (var i = 0; i < tabContents.length; i++) {
            tabContents[i].style.display = 'none';
        }
        var tabContentIdToShow = element.id.replace(/(\d)/g, '-$1');
        document.getElementById(tabContentIdToShow).style.display = 'block';
    }

    $('ul.tabs-menu').each(function () {
        // For each set of tabs, we want to keep track of
        // which tab is active and it's associated content
        var $active, $content, $links = $(this).find('a');

        // Bind the click event handler
        $(this).on('click', 'a', function (e) {
            // Make the old tab inactive.
            $active.removeClass('active');
            $content.hide();
            // Make the tab active.
            $active.addClass('active');
            $content.show();

        });
    });
    </script>
<script>
   
    $("#<%=drpGridLines.ClientID%>").change(function () {

        var str = "True";

        $("#<%=drpGridLines.ClientID%> option:selected").each(function () {
            str = $(this).text();
        });

        UpdatePreference(str);

        if (str == "True") {

            $(".searchFilterNoGridLines").addClass("searchFilterGridLines").removeClass("searchFilterNoGridLines");
            $(".searchTextBoxContainerInVisible").addClass("searchTextBoxContainerVisible").removeClass("searchTextBoxContainerInVisible");

            $('#nav').css('background-color', 'pink');
            $('#nav').css('border-width', '2px');
            $('#nav').css('border-style', 'solid');

            $('div').css('border', '1px');
            $('div').css('border-style', 'dotted');
            $('div').css('border-color', 'blue');

            $('div').css('border-style', 'dotted');
            $('footer').css('background-color', 'lightseagreen');

            $('#sectionA').css('background', 'aqua');
            $('#sectionB').css('background', 'lightcoral');

            $('#divC').css('background', 'lightsalmon');
            $('#divSearchParam').css('background', 'wheat');

            //$('div.row').css('border-style', 'dotted');

        } else {
            $(".searchFilterGridLines").addClass("searchFilterNoGridLines").removeClass("searchFilterGridLines");

            $('#nav').css('background-color', '');
            $('#nav').css('border-style', '');
            $('#nav').css('border-width', '');

            $('#sectionA').css('background', '');
            $('#sectionB').css('background', '');
            $('#divC').css('background', '');
            $('#divSearchParam').css('background', '');


            $('div').css('border-style', 'none');
            $('footer').css('background-color', '');
    	
            $(".searchTextBoxContainerVisible").addClass("searchTextBoxContainerInVisible").removeClass("searchFilterGridLines");
        }
    });

    function UpdatePreference(prefValue) {

        function OnGetMemberSuccess(data, status) {
        }

        function OnGetMemberError(request, status, error) {
            alert("error updating Grid Lines UP Value: " + error);
        }

        $.ajax({
            type: "POST",
            url: "/Default.aspx/UpdateUserPreferenceForGridLines",
            data: "{'value': '" + prefValue + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnGetMemberSuccess,
            error: OnGetMemberError
        });
    }


 </script>






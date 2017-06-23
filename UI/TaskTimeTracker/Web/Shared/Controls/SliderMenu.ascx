
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SliderMenu.ascx.cs" Inherits="Shared.UI.Web.Controls.SliderMenuControl" %>

<%@ Import Namespace="Shared.WebCommon.UI.Web" %>

<%@ Register TagPrefix="sm" TagName="SubMenu" Src="~/Shared/Controls/SubMenu/SubMenu.ascx" %>
<%@ Register Src="~/Shared/Controls/DeveloperPanel.ascx" TagPrefix="sm" TagName="DeveloperPanel" %>


<nav class="cbp-spmenu cbp-spmenu-vertical cbp-spmenu-left" id="cbp_spmenu_s1" runat="server">

    <h3 style="padding-right: 1px;">Menu
		<span class="pull-right" style="padding-top: 10px;padding-right: 3px;" id="showLeft">
			<span>
				<i class="glyphicon glyphicon-align-left"></i>
			</span>
		</span>
	</h3>

	<table width="255" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td>
				<br />
			</td>
		</tr>
		<tr>
			<td>
				<a href="/PMT/PMT/Home" style="padding-right: 3px;padding-top: 2px;" title="Project Management">Project Management<span class="pull-right"><img alt="Project Management" src="/Content/BM/_images/project-planning.png"/></span></a>
			</td>
		</tr>
		<tr>
			<td>
				<a href="/TE/TE/Home" style="padding-right: 7px;" title="Time Entry">Time Entry<span class="pull-right">TE</span></a>
			</td>
		</tr>
		<tr>
			<td>
				<a href="/PDTMGMDEVT/PDTMGMDEVT/Home" style="padding-right: 3px;padding-top: 2px;" title="Product Management">Product Management<span class="pull-right"><img alt="Product Management" src="/Content/BM/_images/product.png"/></span></a>
			</td>
		</tr>
		
		<tr>
			<td>
				<a href="/CapitalMarkets/Home" style="padding-right: 1px;padding-top: 2px;" title="Capital Markets">Capital Markets<span class="pull-right"><img alt="Capital Markets" src="/Content/BM/CapitalMarkets/Images/CapitalMarkets.png"/></span></a>
			</td>
		</tr>
		<tr>
			<td>
				<a href="/DayCare/Home" style="padding-right: 3px;padding-top: 2px;" title="Day Care">Day Care<span class="pull-right"><img alt="Day Care" src="/Content/BM/DayCare/Images/daycare.png"/></span></a>
			</td>
		</tr>
		<tr>
			<td>
				<a href="/ReferenceData/Home" style="padding-right: 7px;" title="Reference Data">Reference Data<span class="pull-right">RD</span></a>
			</td>
		</tr>

        <tr>
			<td>
				<a href="/Legal/Home" style="padding-right: 7px;" title="Legal">Legal<span class="pull-right">LE</span></a>
			</td>
		</tr>
        <tr>
			<td>
				<a href="/ApplicationAdministration/Home" style="padding-right: 7px;" title="Application Administration">ApplicationAdministration<span class="pull-right">AA</span></a>
			</td>
		</tr>
        <tr>
			<td>
				<a href="/Prototype/Home" style="padding-right: 3px;padding-top: 2px;" title="Prototype">Prototype<span class="pull-right"><img alt="Prototype" src="/Content/MS/Prototype/Images/prototype.jpg"/></span></a>
			</td>
		</tr>
         <tr>
			<td>
				<a href="/SA/SA/Home" style="padding-right: 7px;" title="System Administration">System Administration<span class="pull-right">SA</span></a>
			</td>
		</tr>
         
	</table>

	<div class="accordian" id="ServerAccordin" runat="server">
	</div>

</nav>

<nav class="cbp-spmenu cbp-spmenu-vertical cbp-spmenu-right" id="cbp-spmenu-s2">
    <h3>Menu</h3>
    <asp:LinkButton ID="lnkHelp" runat="server" Height="30" Width="30" BorderStyle="None"
        ToolTip="Help Page" OnClick="lnkHelp_Click">
        <asp:Image ID="helpimg" runat="server" BorderStyle="None" ToolTip="Help"></asp:Image>
    </asp:LinkButton>
    <a href="#">Celery seakale</a>
    <a href="#">Dulse daikon</a>
    <a href="#">Zucchini garlic</a>
    <a href="#">Catsear azuki bean</a>
    <a href="#">Dandelion bunya</a>
    <a href="#">Rutabaga</a>
</nav>

<nav class="cbp-spmenu-panel cbp-spmenu-vertical cbp-spmenu-right2" id="cbp-spmenu-s5">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td width="25px" valign="top" style="padding-top: 50px;">
                <div id="showRight2" class="slider-container" style="text-align: left;">
                    <button type="button" class="btn btn-default btn-lg">
                        <i class="glyphicon glyphicon-align-right"></i>
                    </button>
                </div>
            </td>
            <td rowspan="2" class="cbp-spmenu-inner-panel" valign="top">
                <h3>Sub Menu</h3>
                <sm:SubMenu ID="oSubMenu" runat="server" />
                <a href="#">Celery seakale</a>
                <a href="#">Dulse daikon</a>
                <a href="#">Zucchini garlic</a>
                <a href="#">Catsear azuki bean</a>
                <a href="#">Dandelion bunya</a>
                <a href="#">Rutabaga</a>
            </td>
        </tr>
    </table>
</nav>

<nav class="cbp-spmenu cbp-spmenu-horizontal cbp-spmenu-top" id="cbp_spmenu_s3" runat="server">
    <div class="menu-container-horizontal">
        <div style="float: left;">Menu &nbsp;</div>
        <div id="closeTop" class="close-container">[x]</div>
    </div>
    <div>
        <a href="#">Celery seakale</a>
        <a href="#">Dulse daikon</a>
        <a href="#">Zucchini garlic</a>
        <a href="#">Catsear azuki bean</a>
        <a href="#">Dandelion bunya</a>
        <a href="#">Rutabaga</a>

        <table>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Menu Category: " CssClass="cbp-spmenu-horizontal-span" />
                </td>
                <td align="left">
                    <asp:DropDownList ID="drpMenuCategory2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpMenuCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>

    </div>
</nav>

<nav class="cbp-spmenu cbp-spmenu-horizontal cbp-spmenu-bottom" id="cbp_spmenu_s4" runat="server">
    <sm:DeveloperPanel runat="server" ID="DeveloperPanel" />
    </nav>


<script type="text/javascript">
    
    $(document).ready(function () {

        //var testLink = "~/Default.aspx";
        //var xTitle = GetPageTitle(testLink);
        //alert(xTitle);

        $(".accordian h3").click(function () {
            //slide up all the link lists
            $(".accordian ul ul").slideUp();
            //slide down the link list below the h3 clicked - only if its closed
            if (!$(this).next().is(":visible")) {

                if ($(this)[0].outerText == "Recent URLs") {
                    //$(this).next()[0].outerHTML
                    LoadRecentURLs($(this).next());
                }
                else
                {
                    $(this).next().slideDown();
                }
            }
        });
    });


    function formatItemLI(item) {
        var strHtml = "<a href='" + item.URL.replace("~", "") + "'>" + GetPageTitle(item.URL) + "</a>";
        return strHtml;
    }

    function GetPageTitle(strLink)
    {
        var strURL = strLink;
        var strIndex1 = 0;
        var strIndex2 = 0;
        var lastIndex = strURL.lastIndexOf("/");

        if (lastIndex == 1)
        {
            strURL = "Home";
        }
        else
        {
            if(strURL.indexOf(".aspx") != -1)
            {
                strIndex2 = strURL.lastIndexOf("/", lastIndex - 1);
                strIndex1 = lastIndex;
            }
            else if(lastIndex != -1)
            {
                strURL = strURL.substring(0, lastIndex);
                strIndex1 = strURL.lastIndexOf("/", lastIndex - 1);
                strIndex2 = strURL.lastIndexOf("/", strIndex1 - 1);
            }

            if (strIndex2 != -1 && strIndex2 != 0)
            {
                strURL = strURL.substring(strIndex2 + 1);
            }

            strURL = strURL.replace("/", " - ");
            strURL = strURL.replace(".aspx", "");
        }

        return strURL;
    }
    
</script>

<script type="text/javascript">
    
    function LoadRecentURLs(parentItem) {

        if(parentItem[0].children.length == 0)
        {
            var xAudit = <%: SessionVariables.RequestProfile.AuditId %>;
		    var apiUrl = 'http://localhost:50881/api/UserLogins?userid=' + xAudit;

		    // Send an AJAX request
		    $.getJSON(apiUrl)
                .done(function (data) {

                    // On success, 'data' contains a list of client.
                    $.each(data, function (key, item) {
                        // Add a list item for the product.

                        var innerLI = $("<li>" + formatItemLI(item) +"</li>");  // Create with DOM
                        parentItem.append(innerLI);

                    });
                    parentItem.slideDown();
                })
                .fail(function (jqXHR, textStatus, err) {
                    parentItem.text('Error: ' + err);
                });
		}
		else
		{
		    parentItem.slideDown();
		}
    }
    
</script>

<script type="text/javascript" src="/Scripts/SlideAndPushMenu/classie.js"></script>
<script type="text/javascript" src="/Scripts/SlideAndPushMenu/slide.helper.js"></script>

<script type="text/javascript">

    $(document).ready(function() {

        var menuLeft       = document.getElementById('<%= cbp_spmenu_s1.ClientID %>');
	    var menuRight      = document.getElementById('cbp-spmenu-s2');
	    var menuRight2     = document.getElementById('cbp-spmenu-s5');
	    var menuTop        = document.getElementById('<%= cbp_spmenu_s3.ClientID %>');
    	var menuBottom     = document.getElementById('<%= cbp_spmenu_s4.ClientID %>');

	    slide_helper(menuLeft, menuRight, menuRight2, menuTop, menuBottom);

	});

</script>


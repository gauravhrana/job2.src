<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SlideAndPushMenu.ascx.cs"
    Inherits="Shared.UI.Web.Controls.SlideAndPushMenu" %>
<%--For Slide And Push Menu--%>
<link rel="stylesheet" type="text/css" href="/Styles/SlideAndPushMenu/component.css" />
<script src="/Scripts/SlideAndPushMenu/modernizr.custom.js"></script>
<%--For Accordin--%>
<link href="/Styles/Nunito.css" rel="stylesheet" type="text/css" />
<link href="/Styles/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
<link href="/Styles/Accordin.css" rel="stylesheet" type="text/css" />
<!-- prefix free to deal with vendor prefixes -->
<script src="/Scripts/Accordin/prefixfree-1.0.7.js" type="text/javascript" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".accordian h3").click(function () {
            //slide up all the link lists
            $(".accordian ul ul").slideUp();
            //slide down the link list below the h3 clicked - only if its closed
            if (!$(this).next().is(":visible")) {
                $(this).next().slideDown();
            }
        })
    })
</script>
<nav class="cbp-spmenu cbp-spmenu-vertical cbp-spmenu-left" id="cbp_spmenu_s1" runat="server">
	<h3>Menu</h3>
	<a href="#">Celery seakale</a>
	<a href="#">Dulse daikon</a>
	<a href="#">Zucchini garlic</a>
	<div class="accordian" id="serverAccordin" runat="server">
    </div>
</nav>
<nav class="cbp-spmenu cbp-spmenu-vertical cbp-spmenu-right" id="cbp-spmenu-s2">
	<h3>Menu</h3>
	<a href="#">Celery seakale</a>
	<a href="#">Dulse daikon</a>
	<a href="#">Zucchini garlic</a>
	<a href="#">Catsear azuki bean</a>
	<a href="#">Dandelion bunya</a>
	<a href="#">Rutabaga</a>
</nav>
<nav class="cbp-spmenu cbp-spmenu-horizontal cbp-spmenu-top" id="cbp_spmenu_s3" runat="server">
	<h3>Menu</h3>
	<a href="#">Celery seakale</a>
	<a href="#">Dulse daikon</a>
	<a href="#">Zucchini garlic</a>
	<a href="#">Catsear azuki bean</a>
	<a href="#">Dandelion bunya</a>
	<a href="#">Rutabaga</a>
                        
</nav>
<nav class="cbp-spmenu cbp-spmenu-horizontal cbp-spmenu-bottom" id="cbp_spmenu_s4"
    runat="server">
    <h3>Menu</h3>
	<a href="#">Celery seakale</a>
	<a href="#">Dulse daikon</a>
	<a href="#">Zucchini garlic</a>
	<a href="#">Catsear azuki bean</a>    
	<table>
            <tr>
                <td>
                    <asp:Label ID="lblApplicationMode" runat="server" Text="Application Mode: " />
                </td>
                <td align="left">
                    <asp:DropDownList ID="drpApplicationMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpApplicationMode_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>    
</nav>
<script src="/Scripts/SlideAndPushMenu/classie.js"></script>
<script>
    var menuLeft = document.getElementById('<%= cbp_spmenu_s1.ClientID %>'),
		menuRight = document.getElementById('cbp-spmenu-s2'),
		menuTop = document.getElementById('<%= cbp_spmenu_s3.ClientID %>'),
		menuBottom = document.getElementById('<%= cbp_spmenu_s4.ClientID %>'),
		showLeft = document.getElementById('showLeft'),
		showRight = document.getElementById('showRight'),
		showTop = document.getElementById('showTop'),
		showBottom = document.getElementById('showBottom'),
		showLeftPush = document.getElementById('showLeftPush'),
		showRightPush = document.getElementById('showRightPush'),
        showTopPush = document.getElementById('showTopPush'),
        showBottomPush = document.getElementById('showBottomPush'),
		body = document.body;

    showLeft.onclick = function () {
        classie.toggle(this, 'active');
        classie.toggle(menuLeft, 'cbp-spmenu-open');
        disableOther('showLeft');
    };
    showRight.onclick = function () {
        classie.toggle(this, 'active');
        classie.toggle(menuRight, 'cbp-spmenu-open');
        disableOther('showRight');
    };
    showTop.onclick = function () {
        classie.toggle(this, 'active');
        classie.toggle(menuTop, 'cbp-spmenu-open');
        disableOther('showTop');
    };
    showBottom.onclick = function () {
        classie.toggle(this, 'active');
        classie.toggle(menuBottom, 'cbp-spmenu-open');
        disableOther('showBottom');
    };
    showLeftPush.onclick = function () {
        classie.toggle(this, 'active');
        classie.toggle(body, 'cbp-spmenu-push-toright');
        classie.toggle(menuLeft, 'cbp-spmenu-open');
        disableOther('showLeftPush');
    };
    showRightPush.onclick = function () {
        classie.toggle(this, 'active');
        classie.toggle(body, 'cbp-spmenu-push-toleft');
        classie.toggle(menuRight, 'cbp-spmenu-open');
        disableOther('showRightPush');
    };
    showTopPush.onclick = function () {
        classie.toggle(this, 'active');
        classie.toggle(body, 'cbp-spmenu-push-tobottom');
        classie.toggle(menuTop, 'cbp-spmenu-open');
        disableOther('showTopPush');
    };
    showBottomPush.onclick = function () {
        classie.toggle(this, 'active');
        classie.toggle(body, 'cbp-spmenu-push-totop');
        classie.toggle(menuBottom, 'cbp-spmenu-open');
        disableOther('showBottomPush');
    };

    function disableOther(button) {
        if (button !== 'showLeft') {
            classie.toggle(showLeft, 'disabled');
        }
        if (button !== 'showRight') {
            classie.toggle(showRight, 'disabled');
        }
        if (button !== 'showTop') {
            classie.toggle(showTop, 'disabled');
        }
        if (button !== 'showBottom') {
            classie.toggle(showBottom, 'disabled');
        }
        if (button !== 'showLeftPush') {
            classie.toggle(showLeftPush, 'disabled');
        }
        if (button !== 'showRightPush') {
            classie.toggle(showRightPush, 'disabled');
        }
        if (button !== 'showTopPush') {
            classie.toggle(showTopPush, 'disabled');
        }
        if (button !== 'showBottomPush') {
            classie.toggle(showBottomPush, 'disabled');
        }
    }
		</script>

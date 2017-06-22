<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="TestAccordin.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.TestPages.TestAccordin" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="/Styles/Nunito.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Accordin2.css" rel="stylesheet" type="text/css" />
    <!-- prefix free to deal with vendor prefixes -->
    <script src="http://thecodeplayer.com/uploads/js/prefixfree-1.0.7.js" type="text/javascript"
        type="text/javascript"></script>
    <!-- jQuery -->
    <script src="http://thecodeplayer.com/uploads/js/jquery-1.7.1.min.js" type="text/javascript"></script>
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
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="accordian" id="serverAccordin" runat="server">
    </div>
    <div id="MainContent_serverAccordin" class="accordian">
        <ul>
            <li>
                <h3>
                    DashBoard</h3>
                <ul>
                    <li><a href="#">Anchor 1</a></li><li><a href="#">Anchor 2</a></li><li><a href="#">Anchor
                        3</a></li></ul>
            </li>
            <li>
                <h3>
                    Tasks</h3>
                <ul>
                    <li><a href="#">Anchor 1</a></li><li><a href="#">Anchor 2</a></li><li><a href="#">Anchor
                        3</a></li></ul>
            </li>
            <li>
                <h3>
                    Calendar</h3>
                <ul>
                    <li><a href="#">Anchor 1</a></li><li><a href="#">Anchor 2</a></li><li><a href="#">Anchor
                        3</a></li></ul>
            </li>
            <li>
                <h3>
                    Heart</h3>
                <ul>
                    <li><a href="#">Anchor 1</a></li><li><a href="#">Anchor 2</a></li><li><a href="#">Anchor
                        3</a></li></ul>
            </li>
        </ul>
    </div>
    </div>
</asp:Content>

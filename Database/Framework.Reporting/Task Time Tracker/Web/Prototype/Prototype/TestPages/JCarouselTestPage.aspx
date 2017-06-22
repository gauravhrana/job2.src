<%@ Page Title="JCarousel Test Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JCarouselTestPage.aspx.cs"
    Inherits="Shared.UI.Web.ApplicationManagement.Development.TestPages.JCarouselTestPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Styles/Carousel.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <%--<div class="mycarousel">
        <div class="mycarouselInnerdiv">
            <div class="mycarouselItemDiv">Item 1</div>
            <div class="mycarouselItemDiv">Item 2</div>
            <div class="mycarouselItemDiv">Item 3</div>
            <div class="mycarouselItemDiv">Item 4</div>
            <div class="mycarouselItemDiv">Item 5</div>
        </div>
    </div>--%>
    <div class="carouselContainer">
        <div class="mycarousel-wrapper">
            <div class="mycarousel">
                <div class="mycarouselInnerdiv">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="mycarouselItemDiv">
                                <table>
                                    <tr>
                                        <td>ClientId: </td>
                                        <td>
                                            <asp:Label ID="lblClientId" runat="server" Text='<%# Eval("ClientId")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Name: </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>SortOrder: </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("SortOrder")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <a href="#" class="jcarousel-control-prev">&lsaquo;</a>
            <a href="#" class="jcarousel-control-next">&rsaquo;</a>
        </div>
    </div>
    <script src="../../../../Scripts/jquery.jcarousel.min.js"></script>
    <script>
        $(function () {
            $('.mycarousel').jcarousel({
                // Configuration goes here
            });

            $('.jcarousel-control-prev')
            .on('jcarouselcontrol:active', function () {
                $(this).removeClass('inactive');
            })
            .on('jcarouselcontrol:inactive', function () {
                $(this).addClass('inactive');
            })
            .jcarouselControl({
                target: '-=1'
            });

            $('.jcarousel-control-next')
                .on('jcarouselcontrol:active', function () {
                    $(this).removeClass('inactive');
                })
                .on('jcarouselcontrol:inactive', function () {
                    $(this).addClass('inactive');
                })
                .jcarouselControl({
                    target: '+=1'
                });

        });
    </script>

</asp:Content>

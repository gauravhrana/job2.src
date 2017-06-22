<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="eSettingsRepeater.ascx.cs"
    Inherits="Shared.UI.Web.Controls.eSettingsRepeater" %>
<table style="width: 100%">
    <tr>
        <td>
            <asp:RadioButtonList ID="rbtnList" runat="server" RepeatDirection="Horizontal"
                OnSelectedIndexChanged="rbtnList_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="Horizontal Layout" Value="Stack" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Vertical Layout" Value="Grid"></asp:ListItem>
                <asp:ListItem Text="Carousel Layout" Value="Carousel"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td style="text-align: right;">
            <asp:Label ID="lblFCMode" CssClass="bold-label" runat="server" Text="Field Configuration Mode: "></asp:Label><asp:Label ID="lblFCModeName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
<div id="divTabContainer" runat="server">
</div>
<table style="width: 100%">
    <tr>
        <td>
            <asp:Repeater ID="ReadOnlyRepeater" runat="server" OnItemDataBound="ItemBound">
                <HeaderTemplate>
                    <table >
                        <tr>
                            <td style="padding: 2px;">
                </HeaderTemplate>
                <ItemTemplate>
                    <table class="DetailControlBorder" >
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdncol" runat="server" Value=' <%# Eval("Name")%> ' />
                                <asp:DataList ID="InnerRepeater" runat="server" RepeatDirection="Horizontal" RepeatColumns="4">
                                    <ItemTemplate>
                                        <div style="padding: 4px;">
                                            <div style="float: left; text-align: right; width: 130px; overflow: visible">
                                                <%# Eval("Name")%>
                                                    &nbsp;&nbsp;
                                            </div>
                                            <div style="float: right; width: 130px;">
                                                <%# Eval("Value")%>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                    </td> 
                    </tr> 
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Repeater ID="EditableRepeater" runat="server" OnItemDataBound="ItemBound">
                <HeaderTemplate>
                    <table >
                        <tr>
                            <td>
                </HeaderTemplate>
                <ItemTemplate>
                    <table class="DetailControlBorder" style="margin-bottom: 10px; width: 100%">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdncol" runat="server" Value=' <%# Eval("Name")%> ' />
                                <asp:HiddenField ID="hdnfcid" runat="server" Value=' <%# Eval("FieldConfigurationId")%> ' />
                                <asp:HiddenField ID="hdnfcmid" runat="server" Value=' <%# Eval("FieldConfigurationModeId")%> ' />
                                <asp:DataList ID="InnerRepeater" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                    <ItemTemplate>
                                        <div style="padding: 4px;">
                                            <div style="float: left; text-align: right; width: 130px; overflow: visible;">
                                                <asp:Label ID="lblcolname" CssClass="ralabel" Text='<%# Eval("Name")%>' runat="server"></asp:Label>
                                                &nbsp;&nbsp;
                                            </div>
                                            <div style="float: right; width: 130px;">
                                                <asp:TextBox ID="txt" runat="server" Width="120px" Text=' <%# Eval("Value")%>'></asp:TextBox>
                                            </div>
                                            <div style="display: none;">
                                                <asp:Label ID="lblColumnName" Text='<%# Eval("ColumnName")%>' runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                    </td>
                        </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Repeater ID="EditableRepeaterHiddenColuumns" runat="server" OnItemDataBound="ItemBound">
                <HeaderTemplate>
                    <table >
                        <tr>
                            <td>
                </HeaderTemplate>
                <ItemTemplate>
                    <table class="DetailControlBorder" style="margin-bottom: 10px;">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdncol" runat="server" Value=' <%# Eval("Name")%> ' />
                                <asp:HiddenField ID="hdnfcid" runat="server" Value=' <%# Eval("FieldConfigurationId")%> ' />
                                <asp:HiddenField ID="hdnfcmid" runat="server" Value=' <%# Eval("FieldConfigurationModeId")%> ' />
                                <asp:DataList ID="InnerRepeater" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                    <ItemTemplate>
                                        <div style="padding: 4px;">
                                            <div style="float: left; text-align: right; width: 130px; overflow: visible;">
                                                <asp:Label ID="lblcolname" CssClass="ralabel" Text='<%# Eval("Name")%>' runat="server"></asp:Label>
                                                &nbsp;&nbsp;
                                            </div>
                                            <div style="float: right; width: 130px;">
                                                <asp:TextBox ID="txt" runat="server" Width="120px" Text=' <%# Eval("Value")%>'></asp:TextBox>
                                            </div>
                                            <div style="display: none;">
                                                <asp:Label ID="lblColumnName" Text='<%# Eval("ColumnName")%>' runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                    </td>
                        </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>
<table >
    <tr>
        <td>
            <asp:DataList ID="ReadOnlyGridPanel" runat="server" OnItemDataBound="ItemBound" RepeatDirection="Horizontal" RepeatColumns="4">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <table class="DetailControlBorder">
                        <tr>
                            <td style="padding: 2px;">
                                <asp:HiddenField ID="hdncol" runat="server" Value=' <%# Eval("Name")%> ' />
                                <asp:DataList ID="InnerRepeater" runat="server" RepeatDirection="Horizontal" RepeatColumns="4">
                                    <HeaderTemplate>
                                        <table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: right; width: 160px;">
                                                <%# Eval("Name")%>:  &nbsp;
                                            </td>
                                            <td>
                                                <%# Eval("Value")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:DataList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DataList ID="EditableGridPanel"  runat="server" OnItemDataBound="ItemBound" RepeatColumns="4" RepeatDirection="Horizontal">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <table class="DetailControlBorder">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdncol" runat="server" Value=' <%# Eval("Name")%> ' />
                                <asp:HiddenField ID="hdnfcid" runat="server" Value=' <%# Eval("FieldConfigurationId")%> ' />
                                <asp:HiddenField ID="hdnfcmid" runat="server" Value=' <%# Eval("FieldConfigurationModeId")%> ' />
                                <asp:DataList ID="InnerRepeater" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                    <HeaderTemplate>
                                        <table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="padding: 2px;">
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblcolname" Text='<%# Eval("Name")%>' runat="server"></asp:Label>
                                                &nbsp;

                                            </td>
                                            <td style="width: 130px;">
                                                <asp:TextBox ID="txt" runat="server" Width="120px" Text=' <%# Eval("Value")%>'></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:DataList>
            <asp:DataList ID="EditableGridPanelHiddenColumns" 
                runat="server" OnItemDataBound="ItemBound" RepeatColumns="4" RepeatDirection="Horizontal">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <table class="DetailControlBorder">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdncol" runat="server" Value=' <%# Eval("Name")%> ' />
                                <asp:HiddenField ID="hdnfcid" runat="server" Value=' <%# Eval("FieldConfigurationId")%> ' />
                                <asp:HiddenField ID="hdnfcmid" runat="server" Value=' <%# Eval("FieldConfigurationModeId")%> ' />
                                <asp:DataList ID="InnerRepeater" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                    <HeaderTemplate>
                                        <table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="padding: 2px;">
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblcolname" Text='<%# Eval("Name")%>' runat="server"></asp:Label>
                                                &nbsp;

                                            </td>
                                            <td style="width: 130px;">
                                                <asp:TextBox ID="txt" runat="server" Width="120px" Text=' <%# Eval("Value")%>'></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:DataList>
        </td>
    </tr>
</table>
<table >
    <tr>
        <td style="align-content: center;">
            <div class="carouselContainer" id="ReadOnlyCarouselContainer" runat="server">
                <div class="mycarousel-wrapper">
                    <div class="mycarousel">
                        <div class="mycarouselInnerdiv">
                            <asp:Repeater ID="ReadOnlyCarouselRepeater" runat="server" OnItemDataBound="ItemBoundCarosel">
                                <ItemTemplate>
                                    <div class="mycarouselItemDiv">
                                        <asp:HiddenField ID="hdncol" runat="server" Value=' <%# Eval("Name")%> ' />
                                        <asp:Repeater ID="InnerRepeater" runat="server">
                                            <ItemTemplate>
                                                <div style="padding: 4px;">
                                                    <div style="float: left; text-align: right; width: 80px; overflow: visible">
                                                        <%# Eval("Name")%>
                                                    &nbsp;&nbsp;
                                                    </div>
                                                    <div style="float: right; width: 130px;">
                                                        <%# Eval("Value")%>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <a href="#" class="jcarousel-control-prev">&lsaquo;</a>
                    <a href="#" class="jcarousel-control-next">&rsaquo;</a>
                    <!-- Pagination -->
                    <p class="jcarousel-pagination">
                        <!-- Pagination items will be generated in here -->
                    </p>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td style="align-content: center;">
            <div class="carouselContainer" id="EditableCarouselContainer" runat="server">
                <div class="mycarousel-wrapper">
                    <div class="mycarousel">
                        <div class="mycarouselInnerdiv">
                            <asp:Repeater ID="EditableCarouselRepeater" runat="server" OnItemDataBound="ItemBoundCarosel">
                                <ItemTemplate>
                                    <div class="mycarouselItemDiv">
                                        <table>
                                            <asp:HiddenField ID="hdncol" runat="server" Value=' <%# Eval("Name")%> ' />
                                            <asp:HiddenField ID="hdnfcid" runat="server" Value=' <%# Eval("FieldConfigurationId")%> ' />
                                            <asp:HiddenField ID="hdnfcmid" runat="server" Value=' <%# Eval("FieldConfigurationModeId")%> ' />
                                            <asp:Repeater ID="InnerRepeater" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="float: left; text-align: right; width: 160px;">
                                                            <asp:Label ID="lblcolname" CssClass="ralabel" Text='<%# Eval("Name")%>' runat="server"></asp:Label>
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td style="float: right; width: 130px;">
                                                            <asp:TextBox ID="txt" runat="server" Width="120px" Text=' <%# Eval("Value")%>'></asp:TextBox>

                                                            <div style="display: none;">
                                                                <asp:Label ID="lblColumnName" Text='<%# Eval("ColumnName")%>' runat="server"></asp:Label>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <a href="#" class="jcarousel-control-prev">&lsaquo;</a>
                    <a href="#" class="jcarousel-control-next">&rsaquo;</a>
                    <!-- Pagination -->
                    <p class="jcarousel-pagination">
                        <!-- Pagination items will be generated in here -->
                    </p>
                </div>
            </div>
        </td>
    </tr>
</table>

<asp:LinkButton ID="btnUpdateReturn" runat="server" Text="Update and Return " OnClick="btnUpdateReturn_Click" />
<br />
<br />
<asp:LinkButton ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
<asp:LinkButton ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />


<script src="/Scripts/jquery.jcarousel.js"></script>
<script>

    function CreateCarousels() {

        $('.mycarousel')
            .jcarousel({
                // Configuration goes here
                wrap: 'both',
                animation: {
                    duration: 100,
                    easing: 'linear',
                    complete: function () {
                        var leftStyleValue = $('.mycarouselInnerdiv').css('left');
                        leftStyleValue = leftStyleValue.replace("px", "");
                        var startingElement = (leftStyleValue / 343) - 1;
                        startingElement = Math.floor(startingElement * -1);
                        //alert(startingElement);
                        $("#carouselPage" + startingElement).addClass('activePage');
                        startingElement = startingElement + 1;
                        $("#carouselPage" + startingElement).addClass('activePage');
                        startingElement = startingElement + 1;
                        $("#carouselPage" + startingElement).addClass('activePage');
                    }
                }
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

        $('.jcarousel-pagination')
            .on('jcarouselpagination:active', 'a', function () {
                $(this).addClass('activePage');

            })
            .on('jcarouselpagination:inactive', 'a', function () {
                //$(this).removeClass('active');
                $('.activePage').removeClass('activePage');
            })
            .jcarouselPagination({
                // Options go here
                'perPage': 1,
                item: function (page) {
                    return '<a id="carouselPage' + page + '" href="#' + page + '">' + page + '</a>';
                }
            });

        $("#carouselPage1").addClass('activePage');
        $("#carouselPage2").addClass('activePage');
        $("#carouselPage3").addClass('activePage');

    }

    $(function () {
        CreateCarousels();
    });
</script>

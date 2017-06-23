<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Controls.ListControl" %>
<%@ Register TagPrefix="dc" TagName="ExportMenu" Src="~/Shared/Controls/ExportMenu.ascx" %>
<%@ Register TagPrefix="npager" TagName="NumberedPager" Src="~/Shared/Controls/CustomPager/NumberedPager.ascx" %>


<table class="table table-bordered" style="background: lightcyan;">

    <tr>
        <td align="right" valign="middle">

            <div id="divGridActionBar" runat="server" class="success">

                <asp:Panel ID="dynBarContainer" runat="server">

                    <asp:Table ID="Table1" runat="server" CssClass="maintable table table-bordered">
                        <asp:TableRow>
                            <asp:TableCell Width="60">&nbsp; View : </asp:TableCell>

                            <asp:TableCell HorizontalAlign="Left" Width="40">
                                <asp:HiddenField ID="hdnFieldConfigurationModeCategory" runat="server" />
                                <asp:DropDownList ID="ddlFieldConfigurationMode" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlFieldConfigurationMode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </asp:TableCell>

                            <asp:TableCell HorizontalAlign="Left">
                                <asp:LinkButton ID="lnkColumnChooser" runat="server" BorderStyle="None" ToolTip="Column Chooser"
                                    Text="CC" OnClick="lnkColumnChooser_Click">        
                                </asp:LinkButton>
                            </asp:TableCell>

                            <asp:TableCell HorizontalAlign="Right">
                                <table>
                                    <tr>
                                        <td>
                                            <div style="float: left;" id="divSortingOptions" runat="server">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Full Table Sort" Selected="True" Value="FTSort"></asp:ListItem>
                                                    <asp:ListItem Text="View Sort" Value="VSort"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </td>
                                        <td align="right">
                                            <asp:Panel ID="pnlFormatting" runat="server">
                                                <table cellpadding="0" cellspacing="0" border="0" align="right">
                                                    <tr>
                                                        <td align="right"></td>
                                                        <td width="20"></td>
                                                        <td align="right">
                                                            <div style="float: right;" id="fontpanel" runat="server">
                                                                Font Size:
                                                                        <asp:LinkButton ID="lnkfontsmall" runat="server" Style="font-size: 12px; color: Blue; font-weight: bold;"
                                                                            OnClick="lnkfontsmall_Click">A</asp:LinkButton>
                                                                <asp:LinkButton ID="lnkfontmedium" runat="server" Style="font-size: 14px; color: Blue; font-weight: bold;"
                                                                    OnClick="lnkfontmedium_Click">A</asp:LinkButton>
                                                                <asp:LinkButton ID="lnkfontlarger" runat="server" Style="font-size: 16px; color: Blue; font-weight: bold;"
                                                                    OnClick="LnkfontlargerClick">A</asp:LinkButton>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableCell>

                            <asp:TableCell HorizontalAlign="Right" Style="padding-right: 15px;">
                                <dc:ExportMenu ID="myExportMenu" runat="server" />
                            </asp:TableCell>

                        </asp:TableRow>
                    </asp:Table>

                </asp:Panel>

            </div>
        </td>
    </tr>

    <tr>
        <td>
            <div id="griddiv" runat="server">

                <div id="gridContainer" class="gridContainer">

                    <asp:GridView ID="MainGridView"
                        AutoGenerateColumns="false"
                        AllowPaging="true"
                        AllowSorting="true"
                        runat="server"
                        OnSorting="MainGridView_Sorting"
                        OnRowCreated="MainGridView_RowCreated"
                        OnPageIndexChanging="MainGridView_PageIndexChanging"
                        OnRowDataBound="MainGridView_RowDataBound"
                        OnSorted="MainGridView_Sorted"
                        ShowFooter="true"
                        CssClass="table table-bordered table-condensed table-hover table-striped">

                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle />
                                <ItemStyle />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="All"
                                        onclick="CheckChanged(this);"
                                        AutoPostBack="true"
                                        OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center; width: auto">
                                        <asp:CheckBox ID="CheckBox1" runat="server" Visible="true" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Left" Width="30px" BorderWidth="1px" BorderColor="Black" />
                                <ItemStyle HorizontalAlign="Left" Width="30px" BorderWidth="1px" BorderColor="Black" />
                                <HeaderTemplate>
                                    Image
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center; width: auto">
                                        <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Content/images/expand_blue.jpg" CommandName='<%# "~/ApplicationDevelopment/FunctionalityImage/ShowImage.aspx?imageid=" + Eval("FunctionalityImageId")  %>' OnClick="Image1_Click" />                                        
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>

                        <PagerSettings Visible="false" />

                    </asp:GridView>

                </div>

                <!-- Custom Paging Control -->
                <div id="divPagingContainer" runat="server" cssclass="table table-bordered table-hover table-striped table-condensed">
                    <table class="table table-bordered">
                        <tr>
                            <td>
                                <div style="float: left;">
                                    <npager:NumberedPager ID="numberedPager" runat="server" />
                                </div>
                                <div style="text-align: right; height: 25px;" id="pagingPanel" runat="server">
                                    <asp:PlaceHolder ID="plcPaging" runat="server" />
                                    <asp:Label ID="litPagingSummary" runat="server" />
                                    &nbsp;
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>

            </div>

            <asp:Panel ID="pnlButtonPanel" runat="server">
                <div style="float: left; background: whitesmoke; padding-top: 2px; padding-bottom: 2px;" id="buttonPanel" runat="server">
                    <asp:Button ID="ButtonInsert" runat="server" Text="Insert" OnClick="ButtonInsert_Click" CssClass="btn btn-default" />
                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" Enabled="false" OnClick="ButtonDelete_Click" CssClass="btn btn-default" />
                    <asp:Button ID="ButtonDetails" runat="server" Enabled="false" Text="Details" OnClick="ButtonDetails_Click" CssClass="btn btn-default" />
                    <asp:Button ID="ButtonUpdate" runat="server" Text="Update" Enabled="false" OnClick="ButtonUpdate_Click" CommandArgument='<%= MainGridView.PageIndex %>' CssClass="btn btn-default" />
                    <asp:Button ID="ButtonCommonUpdate" runat="server" Enabled="false" Text="Common Update" OnClick="ButtonCommonUpdate_Click" CssClass="btn btn-default" />
                    <asp:Button ID="ButtonInlineUpdate" runat="server" Enabled="false" Text="Inline Update" OnClick="ButtonInlineUpdate_Click" CssClass="btn btn-default" />
                </div>
                <div style="float: right; background: whitesmoke; padding-top: 2px; padding-bottom: 2px;"
                    id="advancedButtonpanel" runat="server">
                    <asp:Button ID="ButtonTestData" runat="server" Text="Set Test" OnClick="ButtonTestData_Click" CssClass="btn btn-default" />
                    <asp:Button ID="ButtonRealData" runat="server" Text="Set Real" OnClick="ButtonRealData_Click" CssClass="btn btn-default" />
                    <asp:Button ID="ButtonRenumber" runat="server" Text="Renumber" OnClick="ButtonRenumber_Click" CssClass="btn btn-default" />
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlRenumber" runat="server" Visible="false">
                <asp:Table runat="server" ID="tblMain" CssClass="searchfilter">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">Renumber Criteria</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Seed:</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="txtSeed" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Increment:</asp:TableCell><asp:TableCell>
                            <asp:TextBox runat="server" ID="txtIncrement" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                            <asp:Button runat="server" ID="btnSearch" Text="Renumber" OnClick="btnRenumber_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>

        </td>
    </tr>
</table>


<script>
    function OpenPopup(url) {
        $("#win1").html("");
        var contentforWindow;
        contentforWindow = "<table><tr><td colspan=2>";
        contentforWindow +='<img width="1000" height="500" src="'+url+'"> </img>';
        contentforWindow +='</td> </tr>';
        contentforWindow +='<tr><td><span id="spnImageText" /></td> <td align="right"> ';
        contentforWindow += '<button  class="k-button">close</button>';
        contentforWindow += '</td>  </tr> </table>';
        
        var window = $("#win1");
        window.html(contentforWindow).kendoWindow();
        window.kendoWindow({
            actions: {},
            modal: true,            
            width: 1000,
            height:500,
            resizable: false,
            animation: false
           
            });
        
        var kendoWindow = window.data("kendoWindow");
        kendoWindow.content(contentforWindow);
        window.parent().find(".k-window-action").css("visibility", "hidden");
        
        kendoWindow.center().open();
        
        $(".k-button").click(function () {               
                $(".k-window-content").each(function () {
                    $(this).data("kendoWindow").close();
                });
            });        
    }

</script>

<div id="win1">
</div>


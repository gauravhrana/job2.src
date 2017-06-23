<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Controls.List" %>
<%@ Register TagPrefix="dc" TagName="ExportMenu" Src="~/Shared/Controls/ExportMenu.ascx" %>
<%@ Register TagPrefix="npager" TagName="NumberedPager" Src="~/Shared/Controls/CustomPager/NumberedPager.ascx" %>
<script type="text/javascript">

    function CheckChanged(chkBox) {

        var gv = document.getElementById("<%= MainGridView.ClientID %>");
        var inputList = gv.getElementsByTagName("input");
        var numChecked = 0;

        for (var i = 0; i < inputList.length; i++) {
            if (inputList[i].type == "checkbox" && inputList[i].checked) {
                numChecked = numChecked + 1;
            }
        }
         if(document.getElementById('<%=ButtonDelete.ClientID%>')!=null && document.getElementById('<%=ButtonUpdate.ClientID%>')!=null && 
        document.getElementById('<%=ButtonCommonUpdate.ClientID%>')!=null && document.getElementById('<%=ButtonInlineUpdate.ClientID%>')!=null)
        {
        if (numChecked > 0) {
       
            document.getElementById('<%=ButtonDelete.ClientID%>').disabled = false;
            document.getElementById('<%=ButtonUpdate.ClientID%>').disabled = false;
            document.getElementById('<%=ButtonDetails.ClientID%>').disabled = false;
            document.getElementById('<%=ButtonCommonUpdate.ClientID%>').disabled = false;
            document.getElementById('<%=ButtonInlineUpdate.ClientID%>').disabled = false;

            document.getElementById('<%=ButtonDelete.ClientID%>').style.background = "#B40404";
            document.getElementById('<%=ButtonUpdate.ClientID%>').style.background = "#B40404";
            document.getElementById('<%=ButtonDetails.ClientID%>').style.background = "#B40404";
            document.getElementById('<%=ButtonCommonUpdate.ClientID%>').style.background = "#B40404";
            document.getElementById('<%=ButtonInlineUpdate.ClientID%>').style.background = "#B40404";
            //             document.getElementById('<%=ButtonUpdate.ClientID%>').style.display = "";
            //             document.getElementById('<%=ButtonDetails.ClientID%>').style.display = "";
        }
        else {

            document.getElementById('<%=ButtonDelete.ClientID%>').disabled = true;
            document.getElementById('<%=ButtonUpdate.ClientID%>').disabled = true;
            document.getElementById('<%=ButtonDetails.ClientID%>').disabled = true;
            document.getElementById('<%=ButtonCommonUpdate.ClientID%>').disabled = true;
            document.getElementById('<%=ButtonInlineUpdate.ClientID%>').disabled = true;

            document.getElementById('<%=ButtonDelete.ClientID%>').style.background = "#808080";
            document.getElementById('<%=ButtonUpdate.ClientID%>').style.background = "#808080";
            document.getElementById('<%=ButtonDetails.ClientID%>').style.background = "#808080";
            document.getElementById('<%=ButtonCommonUpdate.ClientID%>').style.background = "#808080";
            document.getElementById('<%=ButtonInlineUpdate.ClientID%>').style.background = "#808080";
            //             document.getElementById('<%=ButtonUpdate.ClientID%>').style.display = "none";
            //             document.getElementById('<%=ButtonDetails.ClientID%>').style.display = "none";
        }
        }
    }


    function CheckAllChanged(chkBox) {

        if (document.getElementById('ListControlItem_oList_ButtonDelete') != null && document.getElementById('ListControlItem_oList_ButtonUpdate') != null &&
        document.getElementById('ListControlItem_oList_ButtonCommonUpdate') != null && document.getElementById('ListControlItem_oList_ButtonInlineUpdate') != null) {
            if (chkBox.checked == true) {
                document.getElementById('<%=ButtonDelete.ClientID%>').disabled = false;
                document.getElementById('<%=ButtonUpdate.ClientID%>').disabled = false;
                document.getElementById('<%=ButtonDetails.ClientID%>').disabled = false;
                document.getElementById('<%=ButtonCommonUpdate.ClientID%>').disabled = false;
                document.getElementById('<%=ButtonInlineUpdate.ClientID%>').disabled = false;

                document.getElementById('<%=ButtonDelete.ClientID%>').style.background = "#B40404";
                document.getElementById('<%=ButtonUpdate.ClientID%>').style.background = "#B40404";
                document.getElementById('<%=ButtonDetails.ClientID%>').style.background = "#B40404";
                document.getElementById('<%=ButtonCommonUpdate.ClientID%>').style.background = "#B40404";
                document.getElementById('<%=ButtonInlineUpdate.ClientID%>').style.background = "#B40404";
                //             document.getElementById('<%=ButtonDelete.ClientID%>').style.display = "";
                //             document.getElementById('<%=ButtonUpdate.ClientID%>').style.display = "";
                //             document.getElementById('<%=ButtonDetails.ClientID%>').style.display = "";
            }
            if (chkBox.checked == false) {
                document.getElementById('<%=ButtonDelete.ClientID%>').disabled = true;
                document.getElementById('<%=ButtonUpdate.ClientID%>').disabled = true;
                document.getElementById('<%=ButtonDetails.ClientID%>').disabled = true;
                document.getElementById('<%=ButtonCommonUpdate.ClientID%>').disabled = true;
                document.getElementById('<%=ButtonInlineUpdate.ClientID%>').disabled = true;

                document.getElementById('<%=ButtonDelete.ClientID%>').style.background = "#808080";
                document.getElementById('<%=ButtonUpdate.ClientID%>').style.background = "#808080";
                document.getElementById('<%=ButtonDetails.ClientID%>').style.background = "#808080";
                document.getElementById('<%=ButtonCommonUpdate.ClientID%>').style.background = "#808080";
                document.getElementById('<%=ButtonInlineUpdate.ClientID%>').style.background = "#808080";
                //             document.getElementById('<%=ButtonDelete.ClientID%>').style.display = "none";
                //             document.getElementById('<%=ButtonUpdate.ClientID%>').style.display = "none";
                //             document.getElementById('<%=ButtonDetails.ClientID%>').style.display = "none";
            }
        }

    }
</script>
<table cellpadding="0" cellspacing="0" style="font-weight: bold; color: Black" border="0"
    width="100%">
    <tr>
        <td align="right" valign="middle">
            <div id="divGridActionBar" runat="server">
                <asp:Panel ID="dynBarContainer" runat="server" Height="35px" Style="padding-top: 10px;
                    width: 100%">
                    <asp:Table ID="Table1" runat="server" class="maintable">
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
                                <asp:Panel ID="pnlFormatting" runat="server">
                                    <table cellpadding="0" cellspacing="0" border="0" align="right">
                                        <tr>
                                            <td align="right">
                                                <div style="float: left;" id="sortingOptions" runat="server">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Full Table Sort" Selected="True" Value="FTSort"></asp:ListItem>
                                                        <asp:ListItem Text="View Sort" Value="VSort"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </td>
                                            <td width="20">
                                            </td>
                                            <td align="right">
                                                <div style="float: right;" id="fontpanel" runat="server">
                                                    Font Size:
                                                    <asp:LinkButton ID="lnkfontsmall" runat="server" Style="font-size: 12px; color: Blue;
                                                        font-weight: bold;" OnClick="lnkfontsmall_Click">A</asp:LinkButton>
                                                    <asp:LinkButton ID="lnkfontmedium" runat="server" Style="font-size: 14px; color: Blue;
                                                        font-weight: bold;" OnClick="lnkfontmedium_Click">A</asp:LinkButton>
                                                    <asp:LinkButton ID="lnkfontlarger" runat="server" Style="font-size: 16px; color: Blue;
                                                        font-weight: bold;" OnClick="lnkfontlarger_Click">A</asp:LinkButton>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
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
                <div id="gridContainer" style="width: 100%;">
                    <asp:GridView ID="MainGridView" BorderWidth="1px" BorderColor="Black" PageSize="100" Width="100%" AllowPaging="true" AllowSorting="true"
                        CellPadding="2" CellSpacing="2" AutoGenerateColumns="false" runat="server" OnSorting="MainGridView_Sorting"
                        OnRowCreated="MainGridView_RowCreated" OnPageIndexChanging="MainGridView_PageIndexChanging"
                        OnSelectedIndexChanged="MainGridView_SelectedIndexChanged" OnRowDataBound="MainGridView_RowDataBound"
                        OnDataBound="MainGridView_DataBound" OnSorted="MainGridView_Sorted" ShowFooter="true">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Left" Width="30px" BorderWidth="1px" BorderColor="Black" />
                                <ItemStyle HorizontalAlign="Left" Width="30px" BorderWidth="1px" BorderColor="Black" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="All" onclick="CheckAllChanged(this);"
                                        AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                        
                                        
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center; width: auto">
                                        <asp:CheckBox ID="CheckBox1" onclick="CheckChanged(this);" runat="server" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </div>
                <div style="width: 100%; vertical-align: middle; padding-right: 5px; ">
                    <table width="100%" border="2px solid">
                        <tr valign="middle">
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
            <asp:Panel ID="pnlPaging" runat="server">
                <div style="float: left; width: 100%; background: whitesmoke; padding-top: 2px; padding-bottom: 2px;"
                    id="buttonPanel" runat="server">
                    <asp:LinkButton ID="lnkButtonPanelClose" ToolTip="Close" runat="server" OnClick="lnkButtonPanelClose_Click">[X]</asp:LinkButton>
                    <asp:Button ID="ButtonInsert" runat="server" Text="Insert" OnClick="ButtonInsert_Click"
                        Style="background-color: #B40404; font-weight: bold; font-size: small; color: White;" />
                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" Enabled="false" OnClick="ButtonDelete_Click"
                        Style="background-color: #808080; font-weight: bold; font-size: small; color: White;" />
                    <asp:Button ID="ButtonDetails" runat="server" Enabled="false" Text="Details" OnClick="ButtonDetails_Click"
                        Style="background-color: #808080; font-weight: bold; font-size: small; color: White;" />
                    <asp:Button ID="ButtonUpdate" runat="server" Text="Update" Enabled="false" OnClick="ButtonUpdate_Click"
                        Style="background-color: #808080; font-weight: bold; font-size: small; color: White;" />
                    <asp:Button ID="ButtonCommonUpdate" runat="server" Enabled="false" Text="Common Update"
                        OnClick="ButtonCommonUpdate_Click" Style="background-color: #808080; font-weight: bold;
                        font-size: small; color: White;" />
                    <asp:Button ID="ButtonInlineUpdate" runat="server" Enabled="false" Text="Inline Update"
                        OnClick="ButtonInlineUpdate_Click" Style="background-color: #808080; font-weight: bold;
                        font-size: small; color: White;" /></div>
                <div style="float: left; width: 100%; background: whitesmoke; padding-top: 2px; padding-bottom: 2px;"
                    id="advancedButtonpanel" runat="server">
                    <asp:LinkButton ID="lnkAdvancedPanelClose" ToolTip="Close" runat="server" OnClick="lnkAdvancedPanelClose_Click">[X]</asp:LinkButton>
                    <asp:Button ID="ButtonTestData" runat="server" Text="Set Test" OnClick="ButtonTestData_Click"
                        Style="background-color: #B40404; font-weight: bold; font-size: small; color: White;" />
                    <asp:Button ID="ButtonRealData" runat="server" Text="Set Real" OnClick="ButtonRealData_Click"
                        Style="background-color: #B40404; font-weight: bold; font-size: small; color: White;" />
                    <asp:Button ID="ButtonRenumber" runat="server" Text="Renumber" OnClick="ButtonRenumber_Click"
                        Style="background-color: #B40404; font-weight: bold; font-size: small; color: White;" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlRenumber" runat="server" Visible="false">
                <asp:Table runat="server" CellPadding="5" ID="tblMain" CssClass="searchfilter" Width="600">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">Renumber Criteria</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Seed:</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="txtSeed" />
                        </asp:TableCell></asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Increment:</asp:TableCell><asp:TableCell>
                            <asp:TextBox runat="server" ID="txtIncrement" />
                        </asp:TableCell></asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                            <asp:Button runat="server" ID="btnSearch" Text="Renumber" OnClick="btnRenumber_Click" />
                        </asp:TableCell></asp:TableRow>
                </asp:Table>
            </asp:Panel>
        </td>
    </tr>
</table>

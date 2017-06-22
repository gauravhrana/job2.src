<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Shared.UI.Web.Controls.List" %>

<%@ Register TagPrefix="dc" TagName="ExportMenu" Src="~/Shared/Controls/ExportMenu.ascx" %>
<table cellpadding="0" cellspacing="0"  style="font-weight: bold; color: Black" class="maintable" border="0" >
    <tr style="background: lightblue;">
            <td align="right">
                <span class="exportmenuContainer">
                   <asp:DropDownList ID="ddlApplicationEntityFieldLabelMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlApplicationEntityFieldLabelMode_SelectedIndexChanged">
                    </asp:DropDownList>
                    <dc:ExportMenu ID="myExportMenu" runat="server" />
                </span>
            </td>
        </tr>
    <tr>
        
        <td>
        <div id="griddiv" runat="server" >
            <asp:GridView ID="MainGridView" PageSize="100" Width="1000px" AllowPaging="true" AllowSorting="true"
                cellpadding="2" cellspacing="2" 
                AutoGenerateColumns="false" runat="server" OnSorting="GridView_Sorting" OnRowCreated="GridView_RowCreated"                
                OnPageIndexChanging="GridView_PageIndexChanging" 
                onselectedindexchanged="MainGridView_SelectedIndexChanged"
                OnRowDataBound="MainGridView_RowDataBound"
                OnSorted="GridView_Sorted"
                ShowFooter="true">
                <Columns>
                    <asp:TemplateField >
                        <HeaderStyle HorizontalAlign="Left" Width="30px" />
                        <ItemStyle HorizontalAlign="Left" Width="30px" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" Text="All" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />                               
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div style=" text-align:center; width:auto">
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>                                        
                </Columns>                              
            </asp:GridView>            
        </div>

        <asp:Panel ID="pnlPaging" runat="server">
            <div style="float:left; width:1000px; background: whitesmoke;">
                        <asp:Button ID="ButtonDelete" runat="server" Text="Delete" 
                                onclick="ButtonDelete_Click" style=" background-color:#B40404; font-weight:bold; font-size:small; color:White;" /> 
                                <asp:Button ID="ButtonDetails" runat="server" Text="Details" 
                                onclick="ButtonDetails_Click" style=" background-color:#B40404; font-weight:bold; font-size:small; color:White;" />
                                <asp:Button ID="ButtonUpdate" runat="server" Text="Update" 
                                onclick="ButtonUpdate_Click" style=" background-color:#B40404; font-weight:bold; font-size:small; color:White;" /> 
                                <asp:Button ID="ButtonCommonUpdate" runat="server" Text="Common Update" 
                                onclick="ButtonCommonUpdate_Click" style=" background-color:#B40404; font-weight:bold; font-size:small; color:White;" /> 
                                <asp:Button ID="ButtonInlineUpdate" runat="server" Text="Inline Update" 
                                onclick="ButtonInlineUpdate_Click" style=" background-color:#B40404; font-weight:bold; font-size:small; color:White;" /> 
                                <asp:Button ID="ButtonTestData" runat="server" Text="Set Test" 
                                onclick="ButtonTestData_Click" style=" background-color:#B40404; font-weight:bold; font-size:small; color:White;" />
                                <asp:Button ID="ButtonRealData" runat="server" Text="Set Real" 
                                onclick="ButtonRealData_Click" style=" background-color:#B40404; font-weight:bold; font-size:small; color:White;" /> 
                                <asp:Button ID="ButtonRenumber" runat="server" Text="Renumber" 
                                onclick="ButtonRenumber_Click" style=" background-color:#B40404; font-weight:bold; font-size:small; color:White;" /> 
                                
                   </div>
                                
            <div style="float:right;">
                <asp:PlaceHolder ID="plcPaging" runat="server" />
                <asp:Label ID="litPagingSummary" runat="server" />
                <asp:Label ID="lblCacheStatus" runat="server" />

            </div>
            </asp:Panel>

           
        </td>
    </tr>
    <tr>
    <td>
     <asp:Panel ID="pnlFormatting" runat="server">
     <div style="float:right;">
            Font Size: 
            <asp:LinkButton ID="lnkfontsmall" runat="server" style=" font-size:12px; color:Blue; font-weight:bold; " OnClick="lnkfontsmall_Click">A</asp:LinkButton>
                <asp:LinkButton ID="lnkfontmedium" runat="server" style=" font-size:14px; color:Blue; font-weight:bold;" OnClick="lnkfontmedium_Click">A</asp:LinkButton>
                <asp:LinkButton ID="lnkfontlarger" runat="server" style=" font-size:16px; color:Blue; font-weight:bold;" OnClick="lnkfontlarger_Click">A</asp:LinkButton>
                </div>
                <div style="float:left;" >
                   <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Full Table Sort" Selected="True" Value="FTSort"></asp:ListItem>
                <asp:ListItem Text="View Sort" Value="VSort"></asp:ListItem>
            </asp:RadioButtonList>                
            </div>
            </asp:Panel>
    </td>
    </tr>
</table>

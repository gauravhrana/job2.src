<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailsWithChildren.ascx.cs"
    EnableTheming="false" Inherits="Shared.UI.Web.Controls.DetailsWithChildrenControl" %>

<table class="table">
    <tr runat="server" id="AEFLModeContainer" visible="false">
        <td align="right">
            <asp:DropDownList ID="ddlFieldConfigurationMode" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlFieldConfigurationMode_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <div id="griddiv" runat="server">

                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Full Table Sort" Selected="True" Value="FTSort"></asp:ListItem>
                    <asp:ListItem Text="View Sort" Value="VSort"></asp:ListItem>
                </asp:RadioButtonList>

                <asp:GridView ID="GridView" CssClass="table table-bordered table-condensed table-hover table-striped inline-block" 
                    PageSize="250"
                    AllowPaging="true" 
                    AllowSorting="true" 
                    AutoGenerateColumns="false" 
                    runat="server"
                    OnSorting="GridView_Sorting"
                    OnRowCreated="GridView_RowCreated"
                    GridLines="None"
                    OnPageIndexChanging="GridView_PageIndexChanging"
                    OnSelectedIndexChanged="GridView_SelectedIndexChanged"
                    OnRowDataBound="GridView_RowDataBound"
                    OnSorted="GridView_Sorted" ShowFooter="false"
                    EnableTheming="false" HeaderStyle-HorizontalAlign="Center">
                    
                    <Columns>                        
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" runat="server" Text="All" AutoPostBack="true" Visible="false"
                                    OnCheckedChanged="chkSelectAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="text-align: center; width: auto">
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div style="float: left;" runat="server" id="ButtonContainer" class="btn-block">
                <asp:Button ID="ButtonDelete" runat="server" Text="Delete" OnClick="ButtonDelete_Click" CssClass="btn btn-default" />
                <asp:Button ID="ButtonDetails" runat="server" Text="Details" OnClick="ButtonDetails_Click" CssClass="btn btn-default"/>
                <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" CssClass="btn btn-default"/>
            </div>

            <div style="float: right; visibility: hidden;" runat="server" id="PagingContainer">
                <asp:PlaceHolder ID="plcPaging" runat="server" />
                <asp:Label ID="litPagingSummary" runat="server" />
                <asp:Label ID="lblCacheStatus" runat="server" />
            </div>
        </td>
    </tr>
    <tr>
        <td></td>
    </tr>
</table>

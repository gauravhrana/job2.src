<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="EntityTestData.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.TestData.EntityTestData" %>

<%@ Register TagPrefix="dc" TagName="ExportMenu" Src="~/Shared/Controls/ExportMenu.ascx" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/ApplicationManagement/Development/TestData/Controls/List.ascx" %>
<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="~/Shared/ApplicationManagement/Development/TestData/Controls/SearchFilter.ascx" %>
<%@ Register TagPrefix="srx" Namespace="Shared.UI.WebFramework" Assembly="Shared.UI.WebFramework" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    Entity Test Data
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table cellpadding="5" style="font-weight: bold; color: Black" class="maintable"
        border="0">
        <tr>
            <td>
                <sr:SearchFilter ID="oSearchFilter" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="TestAndAuditGrid" AllowPaging="false" Width="500px" AutoGenerateColumns="false"
                    runat="server" OnPageIndexChanging="GridView_PageIndexChanging" OnRowCommand="GridView_RowCommand"
                    OnRowDataBound="GridView_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Entity" HeaderText="Entity" />
                        <asp:BoundField DataField="NoOfTestRecords" HeaderText="No Of Test Records" />
                        <asp:BoundField DataField="NoOfAuditRecords" HeaderText="No Of Audit Records" />
                        <asp:TemplateField HeaderText="View Test Data">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkview1" runat="server" Text='<%# Eval("Entity", " View {0} Test Records") %>'
                                    CommandName='<%# Eval("Entity") %>' CommandArgument="Test" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Show Audit History">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkview2" runat="server" Text='<%# Eval("Entity", " Show {0} Audit History") %>'
                                    CommandName='<%# Eval("Entity") %>' CommandArgument="Audit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

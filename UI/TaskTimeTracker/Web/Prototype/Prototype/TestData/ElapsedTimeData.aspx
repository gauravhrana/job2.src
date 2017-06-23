<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.Master" AutoEventWireup="true"
    CodeBehind="ElapsedTimeData.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.TestData.ElapsedTimeData" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    Entity Test Data
</asp:Content>

<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
</asp:Content>

<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">
    <table style="font-weight: bold; color: Black" class="maintable"
        border="0">
        <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Computer Name:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtComputerNamee" runat="server" CssClass="form-control" />
                            
                        </td>
                            
                    </tr>
         <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Connection Key:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtConnectionKey" runat="server" CssClass="form-control" />
                            
                        </td>
                            
                    </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="DataGrid" AllowPaging="false" Width="500px" AutoGenerateColumns="true"
                    runat="server">

                   
                </asp:GridView>

            </td>
        </tr>
    </table>
</asp:Content>

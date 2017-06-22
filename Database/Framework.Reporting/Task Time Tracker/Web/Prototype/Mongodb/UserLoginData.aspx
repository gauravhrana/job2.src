<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Prototype/Default.master" CodeBehind="UserLoginData.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.Mongodb.UserLoginData" %>

<%@ Register Src="~/Shared/Controls/SearchActionBar.ascx" TagName="SearchActionBar"
    TagPrefix="ucSearchActionBar" %>

<%--<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>--%>

<asp:Content ID="ContentControlItem" ContentPlaceHolderID="SearchControlItem" runat="server">
    <asp:Table runat="server" CellSpacing="0" CellPadding="0" ID="tblMain" CssClass="searchfilter">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3" CssClass="searchFilterHeaderContainer">
                <ucSearchActionBar:SearchActionBar ID="oSearchActionBar" runat="server" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="ralabel">
                  <asp:LinkButton runat="server" Text="UserLoginDetails" OnClick="lnkUserLogin_Click" ID="lnkUserLogin">

                  </asp:LinkButton>

            </asp:TableCell>
            <asp:TableCell>

                <asp:LinkButton runat="server" Text="UserLoginHistoryDetails" OnClick="lnkUserLoginHistory_Click" ID="lnkUserLoginHistory"></asp:LinkButton>

            </asp:TableCell>

             <asp:TableCell>

                 <asp:LinkButton runat="server" Text="UserLoginStatus" OnClick="lnkUserLoginStatus_Click" ID="lnkUserLoginStatus"></asp:LinkButton>

            </asp:TableCell>
        </asp:TableRow>
        
        
          
    </asp:Table>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ListControlItem" runat="server">
    <table>
        <tr>
            <td colspan="3">
               <asp:GridView ID="gridUserLogin" AutoGenerateColumns="true" runat="server"></asp:GridView>               
            </td>
        </tr>
    </table>

</asp:Content>




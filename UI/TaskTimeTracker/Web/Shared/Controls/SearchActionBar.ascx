<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchActionBar.ascx.cs" Inherits="Shared.UI.Web.Controls.SearchActionBar" %>

<table class="searchFilterHeader" style="border-color: brown; border-width: 1px; background-color: lightcoral; border-style: solid;" >
    
    <tr class="header" runat="server" id="trHeader">
        <td >	        
            <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Content/Images/expand_blue.jpg" AlternateText="Show Details" />&nbsp;&nbsp;
            
            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Label ID="lblHeader" runat="server" Text="Search"></asp:Label>
            
            <div class="pull-right">
                <asp:LinkButton ID="lnkSearchKey" ToolTip="Saved Query" runat="server" OnClick="lnkSearchKey_Click">Q</asp:LinkButton>
                <%--<asp:HyperLink ID="hypSavedQuery" Text="Q" NavigateUrl="~/Shared/Admin/SystemIntegrity/Searchkey/Default.aspx?EN=" runat="server" />--%>
                <asp:HyperLink ID="hypSettings" ToolTip="Settings" Text="S" NavigateUrl="~/Shared/Admin/SearchSettings.aspx?EN=" runat="server" />
                <asp:LinkButton ID="lnkClose" ToolTip="Close" runat="server" OnClick="lnkClose_Click">[X]</asp:LinkButton>
            </div>

        </td>
    </tr>

</table>

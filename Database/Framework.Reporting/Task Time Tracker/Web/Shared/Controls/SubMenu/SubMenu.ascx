<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubMenu.ascx.cs" Inherits="Shared.UI.Web.Controls.SubMenu.SubMenu" %>

<table runat="server" id="tblSubMenu" >
    <tr>
        <td>
            <div 
                runat="server" align="right" id="divTopBar">
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Content/images/expand_blue.jpg" AlternateText="Show Details" ImageAlign="Left" />
                <asp:HyperLink ID="hypSettings" ToolTip="Settings" Text="S" NavigateUrl="~/Shared/Admin/MenuSettings.aspx" runat="server" />
                <asp:LinkButton ID="lnkClose" ToolTip="Close" runat="server" OnClick="lnkClose_Click">[X]</asp:LinkButton>&nbsp;&nbsp;
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <div runat="server" id="divSubMenu">
                <asp:PlaceHolder ID="plcSubMenuList" runat="server"></asp:PlaceHolder>
            </div>
        </td>
    </tr>
</table>

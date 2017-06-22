<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Relation.ascx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole.Controls.Relation" %>

<div>
    <asp:Table  cellpadding="2"  cellspacing="2" border="0" runat="server" ID="tblMain2">
        <asp:TableRow>
            <asp:TableCell Width="100">ApplicationRoles associated with the Person:</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow >
            <asp:TableCell Width="100">
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>


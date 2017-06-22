<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RelationshipTool.ascx.cs" Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole.Controls.RelationshipTool" %>

<div>
    <table border="1" width="300">
        <tr>
            <td width="200" valign="top">Person</td>
            <td width="*">
                <asp:Label ID="lblPerson" runat="server" />
            </td>
        </tr>
    </table>
</div>
<br />
<div>
    <table  border="1" width="300" id="tblMain1" runat="server">
        <tr>
            <td>Possible ApplicationRoles:</td>
            <td width="*">
                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstSource"></asp:ListBox>
            </td>
            <td width="50" >
                <asp:Button runat="server" Text="-->" ID="btnLeft" onclick="btnLeft_Click"  />
                <asp:Button runat="server" Text="<--" ID="btnRight" onclick="btnRight_Click" />
            </td>
            <td>Current Assigned ApplicationRoles:</td>
            <td width="*">
                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstTarget"></asp:ListBox>
            </td>
        </tr>
        <tr>    
            <td colspan="5" align="right">
                <asp:Button runat="server" Text="Save" ID="btnSave" OnClick="btnSave_Click"/>
            </td>
        </tr>
    </table>
</div>
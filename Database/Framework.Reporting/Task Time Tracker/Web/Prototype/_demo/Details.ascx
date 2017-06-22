<%@ Control Language="C#" Inherits="Shared.UI.Web.Controls.Demo.Details" %>

<table    bgcolor="REd">                    
    <tr>
        <td width="100">DemoId:</td>
        <td><asp:Label ID="lblLayerId" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Name:</td>
        <td><asp:Label ID="lblName" runat="server"></asp:Label> </td>
    </tr>
    <tr>
        <td>Description</td>
        <td><asp:Label ID="lblDescription" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>SortOrder</td>
        <td><asp:Label ID="lblSortOrder" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Extra</td>        
        <td><asp:Label ID="Label1" runat="server" >demo</asp:Label></td>
    </tr>
</table>

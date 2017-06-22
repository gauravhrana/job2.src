
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.About.Controls.Details" %>

<asp:Repeater ID="rptrReleaseLog" runat="server">
<HeaderTemplate>



</HeaderTemplate>
<ItemTemplate>
Release Notes for
<asp:Label ID="lblName" runat="server" EnableViewState="true" Text= '<%# Bind("Name") %>'></asp:Label>&nbsp;&nbsp;
<asp:Label ID="lblVersionNo" runat="server" EnableViewState="true" Text= '<%# Bind("VersionNo") %>'></asp:Label>&nbsp;&nbsp;
<asp:Label ID="lblReleaseDate" runat="server" EnableViewState="true" Text= '<%# Bind("ReleaseDate") %>'></asp:Label>&nbsp;&nbsp;
    <br />
<asp:Repeater ID="rptrReleaseLogDetails" runat="server">
<ItemTemplate>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Label ID="lblVersionNo" runat="server" EnableViewState="true" Text= '<%# Bind("ItemNo") %>'></asp:Label>&nbsp;&nbsp;
<asp:Label ID="lblReleaseDate" runat="server" EnableViewState="true" Text= '<%# Bind("Description") %>'></asp:Label>&nbsp;&nbsp;
<br />
</ItemTemplate>
</asp:Repeater>

</ItemTemplate>
</asp:Repeater>

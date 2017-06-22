<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailButtonPanel.ascx.cs"
    Inherits="Shared.UI.Web.Controls.DetailButtonPanel" %>
<asp:Button ID="ButtonDelete" runat="server" Text="Delete" OnClick="ButtonDelete_Click"
    Style="background-color: #B40404; font-weight: bold; font-size: small; color: White;" />
<asp:Button ID="ButtonDetails" runat="server" Text="Details" OnClick="ButtonDetails_Click"
    Style="background-color: #B40404; font-weight: bold; font-size: small; color: White;" />
<asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click"
    Style="background-color: #B40404; font-weight: bold; font-size: small; color: White;" />
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultipleEditGrid.ascx.cs"
    Inherits="Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser.Controls.MultipleEditGrid" %>
<asp:Label ID="lblStatus" runat="server" Style="color: Red; font-weight: bold; font-size: medium;"></asp:Label>
<br />
<div style="overflow: auto; height: auto">
    <asp:Repeater ID="rptrApplicationUser" runat="server" EnableViewState="true">
        <HeaderTemplate>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>
                        ApplicationUser ID
                    </th>
                    <th>
                        Application User Name
                    </th>
                    <th>
                        First Name
                    </th>
                    <th>
                        Last Name
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblApplicationUserId" runat="server" EnableViewState="true" Text='<%# Bind("ApplicationUserId") %>'></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtApplicationUserName" runat="server" EnableViewState="true" Text='<%# Bind("ApplicationUserName") %>'>
                    </asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server" EnableViewState="true" Text='<%# Bind("FirstName") %>'>
                    </asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" EnableViewState="true" Text='<%# Bind("LastName") %>'>
                    </asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</div>
<br />
<div style="text-align: center;">
    <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click">Save</asp:LinkButton>
</div>

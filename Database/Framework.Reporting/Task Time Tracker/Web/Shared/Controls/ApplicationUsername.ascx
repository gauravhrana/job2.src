<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApplicationUsername.ascx.cs"
    Inherits="Shared.UI.Web.Controls.ApplicationUserNameControl" %>
<table width="95%"  >
    <tr>
        <td align="right" class="ralabel">
            Application User Title
        </td>
        <td>
            <asp:DropDownList ID="drpApplicationUserTitle" runat="server" 
                OnSelectedIndexChanged="drpApplicationUserTitle_SelectedIndexChanged"
                AppendDataBoundItems="true"
                Width="155">
            </asp:DropDownList>
            <asp:TextBox ID="txtApplicationUserTitle" runat="server" Visible="false"></asp:TextBox>
        </td>
        <td>
            <asp:PlaceHolder ID="dynApplicationUserTitle" runat="server" />
        </td>
    </tr>
    <tr>
        <td align="right" class="ralabel">
            Application User Name
        </td>
        <td>
            <asp:TextBox ID="txtApplicationUserName" EnableViewState="true" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:PlaceHolder ID="dynApplicationUserName" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
    <tr>
        <td align="right" class="ralabel">
            Email Address
        </td>
        <td>
            <asp:TextBox ID="txtEmailAddress" EnableViewState="true" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:PlaceHolder ID="dynEmailAddress" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
    <tr>
        <td align="right" class="ralabel">
            Last Name
        </td>
        <td>
            <asp:TextBox ID="txtLastName" EnableViewState="true" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:PlaceHolder ID="dynLastName" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
    <tr>
        <td align="right" class="ralabel">
            Middle Name
        </td>
        <td>
            <asp:TextBox ID="txtMiddleName" EnableViewState="true" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:PlaceHolder ID="dynMiddleName" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
    <tr>
        <td align="right" class="ralabel">
            First Name
        </td>
        <td>
            <asp:TextBox ID="txtFirstName" EnableViewState="true" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:PlaceHolder ID="dynFirstName" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
</table>

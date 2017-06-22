<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogInSettings.aspx.cs"
    Inherits="Shared.UI.Web.Admin.LogInSettings" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    Login Page:
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%--<script type="text/javascript">

        function MyCustomValidation(objSource, objArgs) {
            // Get value.
            var number = objArgs.Value;
            // Check value and return result.
            var length =
            for(
            if (number % 7 == 0) {
                objArgs.IsValid = true;
            }
            else {
                objArgs.IsValid = false;
            }
        }

    </script>--%>
    <table cellpadding="5" style="font-weight: bold; color: Black" width="600" border="0">
        <tr>
            <td width="200">
                Would you like to do testing?
            </td>
            <td align="left">
                <asp:DropDownList ID="drpIsTesting" runat="server" />
            </td>
        </tr>
        <tr>
            <td width="200">
                Theme
            </td>
            <td align="left">
                <asp:DropDownList ID="drpTheme" runat="server" />
            </td>
        </tr>
        <tr>
            <td width="200">
                Please enter a value for auditId or leave it as default
            </td>
            <td align="left">
                <asp:TextBox ID="txtAuditId" runat="server" />
                <asp:PlaceHolder ID="dynAuditId" runat="server" />
                <asp:CustomValidator ID="vldCode" runat="server" ErrorMessage="This value doesn't exist in the Database."
                    ValidateEmptyText="False" OnServerValidate="vldCode_ServerValidate" ControlToValidate="txtAuditId" />
            </td>
        </tr>
        <tr>
            <td width="200">
                Country
            </td>
            <td align="left">
                <asp:DropDownList ID="drpCountry" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="200">
                Default Click Action
            </td>
            <td align="left">
                <asp:DropDownList ID="drpDefaultClickAction" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Value="Detail">Detail</asp:ListItem>
                    <asp:ListItem Selected="True" Value="Update">Update</asp:ListItem>
                    <asp:ListItem Selected="True" Value="InlineUpdate">Inline Update</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td width="200">
                Update Info Style
            </td>
            <td align="left">
                <asp:DropDownList ID="drpUpdateInfoStyle" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Value="UpdateInfoStyle1">UpdateInfoStyle1</asp:ListItem>
                    <asp:ListItem Selected="True" Value="UpdateInfoStyle2">UpdateInfoStyle2</asp:ListItem>
                    <asp:ListItem Selected="True" Value="UpdateInfoStyle3">UpdateInfoStyle3</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td width="200">
            </td>
            <td align="center">
                <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_OnClick"
                    CausesValidation="true" />
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_OnClick" />
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeBehind="SampleByPerson.aspx.cs" Inherits="Shared.UI.Web.BucketExample.SampleByPerson" %>

<asp:Content ID="DefaultContent" ContentPlaceHolderID="MainContent" runat="server">
    <table border="1" width="300">
        <tr>
            <td width="200" valign="top">Person</td>
            <td width="*">
                <asp:DropDownList AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpParent_OnSelectedIndexChanged" ID="drpParent"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <table border="1" width="300">
        <tr>
            <td>Possible ApplicationRoles:</td>
            <td width="*">
                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstSource"></asp:ListBox>
            </td>
            <td width="50">
                <asp:Button runat="server" Text="-->" ID="btnLeft" OnClick="btnLeft_Click" />
                <asp:Button runat="server" Text="<--" ID="btnRight" OnClick="btnRight_Click" />
            </td>
            <td>Current Assigned ApplicationRoles:</td>
            <td width="*">
                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstTarget"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td colspan="5" align="right">
                <asp:Button runat="server" Text="Save" ID="btnSave" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

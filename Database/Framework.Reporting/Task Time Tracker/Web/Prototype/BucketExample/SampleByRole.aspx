<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeBehind="SampleByRole.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.BucketExample.SampleByRole" %>

<asp:Content ID="DefaultContent" ContentPlaceHolderID="MainContent" runat="server">
    <table border="1" width="300">
        <tr>
            <td width="200" valign="top">ApplicationRole</td>
            <td width="*">
                <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="drpParent_OnSelectedIndexChanged" runat="server" ID="drpParent"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <table  border="1" width="300">
        <tr>
            <td>All Persons:</td>
            <td width="*">
                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstSource"></asp:ListBox>
            </td>
            <td width="50" >
                <asp:Button runat="server" Text="-->" ID="btnLeft" onclick="btnLeft_Click"  />
                <asp:Button runat="server" Text="<--" ID="btnRight" onclick="btnRight_Click" />
            </td>
            <td>Current Persons Holding the position:</td>
            <td width="*">
                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstTarget"></asp:ListBox>
            </td>
        </tr>
        <tr>    
            <td colspan="5" align="right">
                <asp:Button runat="server" Text="Save" ID="btnSave" onclick="btnSave_Click"  />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCheck" runat="server" />
            </td>
        </tr>
    </table>

</asp:Content>
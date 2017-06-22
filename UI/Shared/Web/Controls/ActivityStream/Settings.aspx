<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs"
    Inherits="Shared.UI.Web.ActivityStream.Settings" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
    Settings:
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table cellpadding="5" style="font-weight: bold; color: Black" width="95%" border="0">
        <tr>
            <td>
                <div style="overflow: auto; height: auto;">
                    <asp:PlaceHolder ID="plcUpdateList" runat="server"></asp:PlaceHolder>
                </div>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                <asp:Label ID="lblErrorStatus" runat="server" />
                <asp:PlaceHolder ID="plcLoadActivityStream" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="ImportXML.aspx.cs" Inherits="Shared.UI.Web.Import.ImportXML" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    Import : Import from XML File
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table style="font-weight: bold; color: Black" width="400" border="0">
        <tr>
            <td>
                <table width="300" cellpadding="5" border="0">
                    <tr>
                        <td width="100">
                            <asp:Label ID="lblSystemEntity" Text="Entity:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSystemEntity" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSystemEntity" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="100">
                            <asp:Label ID="lblFile" Text="File:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="XMLUpload" runat="server" />
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFile" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="btnInsert" runat="server" Text="Upload" OnClick="btnInsert_Click" />&nbsp;
                <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel"
                    OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

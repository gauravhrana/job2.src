<%@ Page Title="Session Objects" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SessionObjects.aspx.cs" Inherits="Shared.UI.Web.Admin.SessionObjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    Session Variables
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table cellpadding="5" style="font-weight: bold; color: Black; overflow:visible" width="100%;" border="0">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="2" cellspacing="4" border="0" width="100%">
                    <tr valign="top">
                        <td width="150px">
                            Session Objects
                        </td>
                        <td width="150px">
                            Object Type
                        </td>
                        <td align="left">
                            Object Value
                        </td>
                    </tr>
                    <tr valign="top">
                        <td width="150px">
                            <div id="divObjects" runat="server">
                            </div>
                        </td>
                        <td width="150px">
                            <asp:Label ID="lblObjectType" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="left">
                            <div id="divValue" runat="server" style="overflow: auto">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

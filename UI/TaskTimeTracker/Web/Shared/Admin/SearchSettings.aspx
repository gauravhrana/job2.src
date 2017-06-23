<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="SearchSettings.aspx.cs"
    Inherits="Shared.UI.Web.Admin.SearchSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    <asp:HyperLink ID="hprEntity" runat="server"></asp:HyperLink>
    
    : Search Settings
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding-top: 10px;">
     <table cellpadding="0" cellspacing="0" width="600">
    <tr class="header" style="color: White; font-family: Arial;">
        <td  valign="top" style="padding-top: 8px; padding-bottom:8px; padding-left: 10px; padding-right: 10px;">
            <asp:Label ID="lblHeader" runat="server" Text="Search Settings : "></asp:Label>       
            <asp:Label ID="lblEntity" runat="server"></asp:Label>           
        </td>
    </tr>
</table>
        <asp:Repeater ID="SearchSettingsRepeater" runat="server">
            <HeaderTemplate>
           

                <table class="DetailControlBorder" width="600">
                    <tr>
                        <td>
            </HeaderTemplate>
            <ItemTemplate>
                <table>
                    <tr style="width: 400px;">
                        <td style="text-align: right; width: 200px;">
                            <asp:Label ID="lblKey" runat="server" Text='<%# Eval("UserPreferenceKey")%>' />
                        </td>
                        <td style="width: 200px;">
                            <asp:TextBox ID="txtValue" runat="server" Width="200px" Text='<%# Eval("Value")%>' />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            <FooterTemplate>
                </td> </tr> </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:LinkButton ID="btnReturn" Text="Return" OnClick="btnReturn_Click" Style="padding-right: 10px;"
            runat="server" />
        <asp:LinkButton ID="btnSave" Text="Save" OnClick="btnSave_Click" Style="padding-right: 10px;"
            runat="server" />
    </div>
</asp:Content>

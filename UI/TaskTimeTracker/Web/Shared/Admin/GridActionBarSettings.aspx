<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="GridActionBarSettings.aspx.cs"
    Inherits="Shared.UI.Web.Admin.GridActionBarSettings" %>

<%@ Register TagName="List" TagPrefix="dc" Src="~/Shared/Controls/List/List.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function colorChanged(sender) {
            sender.get_element().style.color = "#" + sender.get_selectedColor();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ListControlItem" runat="server">
    <table style="font-weight: bold; color: Black" width="600" border="0">
        <tr>
            <td>
                Background Color:
            </td>
            <td align="left">
                <div style="float: left;">
                    <asp:TextBox ID="txtBackgroundColor" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Panel ID="TopBarBackgroundColorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                        margin: 0 3px; float: left" runat="server" />
                </div>
                <ajaxToolkit:ColorPickerExtender ID="defaultTopBackCPE" runat="server" TargetControlID="txtBackgroundColor"
                    OnClientColorSelectionChanged="colorChanged" SampleControlID="TopBarBackgroundColorSample" />
            </td>
        </tr>
        <tr>
            <td>
                Foreground Color:
            </td>
            <td align="left">
                <div style="float: left;">
                    <asp:TextBox ID="txtForegroundColor" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Panel ID="TopBarForegroundColorSample" Style="width: 18px; height: 18px; border: 1px solid #000;
                        margin: 0 3px; float: left" runat="server" />
                </div>
                <ajaxToolkit:ColorPickerExtender ID="ColorPickerExtender1" runat="server" TargetControlID="txtForegroundColor"
                    OnClientColorSelectionChanged="colorChanged" SampleControlID="TopBarForegroundColorSample" />
            </td>
        </tr>
        <tr>
            <td>
                Font Family:-
            </td>
            <td align="left">
                <asp:TextBox ID="txtFontfamily" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Font Size:-
            </td>
            <td align="left">
                <asp:TextBox ID="txtFontSize" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="left">
                <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_OnClick"
                    CausesValidation="true" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <%--<dc:List ID="oList" runat="server" />--%>
            </td>
        </tr>
    </table>
</asp:Content>

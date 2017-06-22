<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BucketFor3.ascx.cs" Inherits="Shared.UI.Web.Controls.BucketFor3Control" %>
<table >
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td align="center">
            <table border="1" width="300" cellpadding="0" cellspacing="5">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblEntity1" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblEntity2" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr valign="top" align="left">
                    <td>
                        <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstSource" Width="250" Height="250">
                        </asp:ListBox>
                    </td>
                    <td>
                        <asp:PlaceHolder ID="dynTarget" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Button runat="server" Text="Save" ID="btnSave" 
                            onclick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
</table>

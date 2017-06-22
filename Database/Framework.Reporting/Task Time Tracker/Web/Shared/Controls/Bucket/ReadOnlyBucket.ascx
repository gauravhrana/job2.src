<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReadOnlyBucket.ascx.cs" Inherits="Shared.UI.Web.Controls.ReadOnlyBucket" %>

<table >
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtSearchSource" runat="server" Width="250"></asp:TextBox>
                    </td>
                    <td align="left">
                        <asp:Button ID="btnFind" runat="server" Text="Find" OnClick="btnFind_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left">
            <table border="1" cellpadding="0" cellspacing="5">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblPossibleTitle" runat="server" Text="Possible: "></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblCurrentTitle" runat="server" Text="Currently Associated: "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="*">
                        <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstSource" Width="250" Height="250">
                        </asp:ListBox>
                    </td>
                    <td width="*">
                        <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstTarget" Width="250" Height="250">
                        </asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button runat="server" Text="-->" ID="btnLeft" OnClick="btnLeft_Click" />
                    </td>
                    <td align="center">
                        <asp:Button runat="server" Text="<--" ID="btnRight" OnClick="btnRight_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Button runat="server" Text="Reset" ID="btnReset" 
                            onclick="btnReset_Click"  />
                        <asp:Button runat="server" Text="Select" ID="btnSelect" OnClick="btnSelect_Click" Visible="false" />
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

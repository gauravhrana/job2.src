<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DynamicUpdate.ascx.cs" Inherits="Shared.UI.Web.Controls.DynamicUpdate" %>

<div id="borderdiv" runat="server">
    <table cellpadding="5" style="font-weight: bold; color: Black" width="400" border="0">
        <tr>
            <td>
                <asp:Repeater ID="repUpdate" runat="server">
                    <ItemTemplate>
                    <p>
                        <asp:CheckBox ID="chkName" runat="server" /> &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblName" Text='<%# Eval("Name") %>' runat="server">
                        </asp:Label>
                         -
                        <asp:TextBox ID="txtName" Text="" runat="server">
                        </asp:TextBox>
                    </p>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>

</div>

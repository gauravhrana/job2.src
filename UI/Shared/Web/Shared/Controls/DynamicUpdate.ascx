<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DynamicUpdate.ascx.cs" Inherits="Shared.UI.Web.Controls.DynamicUpdate" %>

<table  class="maintable" border="0" width="400" >
    <tr>
        <td>
            <div id="borderdiv" runat="server" class="DetailControlBorder">
                <asp:Repeater ID="repUpdate" runat="server">
                    <HeaderTemplate>
                        <table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style=" text-align:center;">
                                <asp:CheckBox ID="chkName" runat="server" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td style="text-align:right;">
                                <asp:Label ID="lblName" Text='<%# Eval("Value") %>' runat="server">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" Text="" runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <div id="griddiv" runat="server">
                <div id="gridContainer">
                    <b>Records that will be modified:</b>
                    <br />
                    <br />
                    <asp:GridView ID="MainGridView" PageSize="100"  
                        AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" runat="server" CellPadding="5">
                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="Blue" ForeColor="#F7F7F7" />
                        <Columns>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </td>
    </tr>
</table>

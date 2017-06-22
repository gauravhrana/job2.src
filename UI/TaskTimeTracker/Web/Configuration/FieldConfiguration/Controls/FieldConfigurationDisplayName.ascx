<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FieldConfigurationDisplayName.ascx.cs"
    Inherits="Shared.UI.Web.Configuration.FieldConfiguration.Controls.FieldConfigurationDisplayName" %>
<table  >
    <tr>
        <td colspan="3" align="right">
            <asp:LinkButton ID="lnkAddDisplayName" runat="server" OnClick="lnkAddDisplayName_Click">Add Display Name</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="ralabel">
            <asp:GridView ID="dgvDisplayNames" runat="server" AutoGenerateColumns="false" DataKeyNames="FieldConfigurationDisplayNameId"
                OnRowDataBound="dgvDisplayNames_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="40px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelected" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FieldConfigurationDisplayNameId" HeaderText="FieldConfigurationDisplayNameId" Visible="false" />
                    <asp:TemplateField HeaderText="Language" ControlStyle-Width="150px">
                        <ItemTemplate>
                            <asp:DropDownList ID="drpLanguage" runat="server" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Value" ControlStyle-Width="250px">
                        <ItemTemplate>
                            <asp:TextBox ID="txtValue" runat="server" Text='<%# Bind("Value") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Default" HeaderStyle-Width="80px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsDefault" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="ralabel" align="right">
            <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click">Delete</asp:LinkButton>&nbsp;<asp:LinkButton
                ID="lnkUpdate" runat="server" OnClick="lnkUpdate_Click">Update</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <div id="formContainer" runat="server" visible="false">
                <table   >
                    <tr>
                        <td class="ralabel">
                            Language:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpLanguage" runat="server" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Value:
                        </td>
                        <td>
                            <asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Is Default:
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsDefault" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="right">
                            <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click">Save</asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click">Cancel</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>

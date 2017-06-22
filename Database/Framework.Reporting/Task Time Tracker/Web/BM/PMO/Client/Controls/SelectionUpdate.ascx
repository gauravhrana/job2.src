<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectionUpdate.ascx.cs" Inherits="ApplicationContainer.UI.Web.Client.Controls.SelectionUpdate" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/List/List.ascx" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table   >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblClientId" Text="ClientId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtClientId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynClientId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                         <td>
                        <asp:CheckBox ID="chkName" runat="server" />
                        </td>
                        <td class="ralabel">
                            Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                     <td>
                        <asp:CheckBox ID="chkDescription" runat="server" />
                        </td>
                        <td class="ralabel">
                            Description:
                        </td>
                        <td>
                            <textarea id="txtDescription" runat="server" cols="50" rows="3"></textarea>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDescription" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <asp:CheckBox ID="chkSortOrder" runat="server" />
                        </td>
                        <td class="ralabel">
                            SortOrder:
                        </td>
                        
                        <td>
                            <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
                        </td>
                    </tr>
                   
                </table>
            </td>
        </tr>
        <tr>
        <td>
            <asp:LinkButton ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn"/>
            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn"/>           
        </td>
        </tr>
        <tr><td>
            <asp:GridView ID="MainGridView" PageSize="100" Width="1000px"   AllowPaging="true" AllowSorting="true"
                AutoGenerateColumns="true" runat="server" ShowFooter="true">                             
            </asp:GridView>

        </td></tr>
    </table>

</div>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="eList.ascx.cs" Inherits="Shared.UI.Web.Controls.eList" %>

<table cellpadding="5" style="font-weight:bold;color:Black" width="600" border="0" class="maintable">
    <tr>
        <td>
         <div id="griddiv" runat="server">
            <asp:GridView ID="EditableGridView" AllowSorting="true"  PageSize="100" Width="1000px" runat="server" AutoGenerateColumns="false"
            AllowPaging="true"  AutoGenerateEditButton="true" EnableViewState="true"
                 OnRowCancelingEdit="EditableGridView_RowCancelingEdit" 
                 OnRowDataBound="EditableGridView_RowDataBound" 
                 OnRowEditing="EditableGridView_RowEditing" 
                 OnRowUpdating="EditableGridView_RowUpdating" ShowFooter="True" > 
                 <RowStyle Height="30px" Font-Bold="true" />
                <AlternatingRowStyle Height="30px" Font-Bold="true" />
                <HeaderStyle Height="40px" Font-Bold="true" />
                <Columns>
                
                </Columns>
            
            </asp:GridView> 
            </div>               
        </td>
    </tr>
</table>
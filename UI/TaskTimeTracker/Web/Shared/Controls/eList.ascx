<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="eList.ascx.cs" Inherits="Shared.UI.Web.Controls.eList" %>

<table class="maintable">
    <tr>
        <td>

         <div id="griddiv" runat="server">
            <asp:GridView ID="EditableGridView" 
                AllowSorting="true"  
                PageSize="100" Width="1000px" 
                runat="server" AutoGenerateColumns="false"
                AllowPaging="true"  
                AutoGenerateEditButton="true" 
                EnableViewState="true" 
                OnRowCancelingEdit="EditableGridView_RowCancelingEdit" ShowHeader="true"
                OnRowDataBound="EditableGridView_RowDataBound" 
                OnRowEditing="EditableGridView_RowEditing" 
                OnRowUpdating="EditableGridView_RowUpdating" ShowFooter="True" > 
                
                <RowStyle Height="30px" />
                
                <AlternatingRowStyle Height="30px" />
                
                <HeaderStyle Height="40px" />
                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="Blue" ForeColor="#F7F7F7" />     
                
                <Columns>
                
                </Columns>
            
            </asp:GridView> 

        </div>               

        </td>
    </tr>
</table>
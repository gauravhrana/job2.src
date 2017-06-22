<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="eSettingsList.ascx.cs" Inherits="Shared.UI.Web.Controls.eSettingsList" %>

<table cellpadding="5" style="font-weight:bold;color:Black" width="600" border="0" class="maintable">
    <tr>
        <td>
            <asp:GridView ID="ReadOnlyGridView" AllowSorting="true"  PageSize="100" Width="1000px" runat="server" AutoGenerateColumns="false"
            AllowPaging="true" ShowFooter="True"  >
                 <RowStyle Height="30px" Font-Bold="true" />
                <AlternatingRowStyle Height="30px" Font-Bold="true" />
                <HeaderStyle Height="40px" Font-Bold="true" />
               <Columns>
               <asp:TemplateField HeaderText="Id" HeaderStyle-Width="20px" ItemStyle-Width="30px">
                <ItemTemplate><%# Eval("ApplicationEntityFieldLabelId") %></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Name">
                <ItemTemplate><%# Eval("Name") %></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Value">
                <ItemTemplate><%# Eval("Value") %></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Width">
                <ItemTemplate><%# Eval("Width") %></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Formatting">
                <ItemTemplate><%# Eval("Formatting") %></ItemTemplate>
               </asp:TemplateField>
                 <asp:TemplateField HeaderText="Control Type">
                <ItemTemplate><%# Eval("ControlType") %></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="HorizontalAlignment">
                <ItemTemplate><%# Eval("HorizontalAlignment")%></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="GridViewPriority" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("GridViewPriority")%></ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField HeaderText="DetailsViewPriority" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("DetailsViewPriority")%></ItemTemplate>
               </asp:TemplateField>
               </Columns>
            </asp:GridView> 
                       
        </td>
    </tr>
     <tr>
        <td>
         <div id="griddiv" runat="server">
            <asp:GridView ID="EditableGridView" AllowSorting="true"  PageSize="100" runat="server" AutoGenerateColumns="false"
            AllowPaging="true"  AutoGenerateEditButton="false"
                 OnRowCancelingEdit="EditableGridView_RowCancelingEdit" 
                 OnRowDataBound="EditableGridView_RowDataBound" 
                 OnRowEditing="EditableGridView_RowEditing" 
                 OnRowUpdating="EditableGridView_RowUpdating" ShowFooter="True" style="table-layout:fixed;" Width="1000px" >
                 <RowStyle Height="30px" Font-Bold="true" />
                <AlternatingRowStyle Height="30px" Font-Bold="true" />
                <HeaderStyle Height="40px" Font-Bold="true" />
               <Columns>
               <asp:TemplateField HeaderText="Id" HeaderStyle-Width="20px" ItemStyle-Width="20px">
                <ItemTemplate><asp:Label ID="lblApplicationEntityFieldLabelId" runat="server" Text='<%# Bind("ApplicationEntityFieldLabelId") %>' Width="30px"></asp:Label></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Name">
                <ItemTemplate><asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' Columns="12"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Value">
                <ItemTemplate><asp:TextBox ID="txtValue" runat="server" Text='<%# Bind("Value") %>' Columns="12"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Width">
                <ItemTemplate><asp:TextBox ID="txtWidth" runat="server" Text='<%# Bind("Width") %>' Columns="6"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Formatting">
                <ItemTemplate> <asp:TextBox ID="txtFormatting" runat="server" Text='<%# Bind("Formatting") %>' Columns="6"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Control Type">
                <ItemTemplate><asp:TextBox ID="txtControlType" runat="server" Text='<%# Bind("ControlType") %>' Columns="12"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="HorizontalAlignment">
                <ItemTemplate><asp:TextBox ID="txtHorizontalAlignment" runat="server" Text='<%# Bind("HorizontalAlignment") %>' Columns="6"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="GridViewPriority">
                <ItemTemplate><asp:TextBox ID="txtGridViewPriority" runat="server" Text='<%# Bind("GridViewPriority") %>' Columns="6"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="DetailsViewPriority">
                <ItemTemplate> <asp:TextBox ID="txtDetailsViewPriority" runat="server" Text='<%# Bind("DetailsViewPriority") %>' Columns="6"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
               </Columns>
            </asp:GridView> 
            </div>               
        </td>
    </tr>
      <tr>
            <td align="right">
                <asp:LinkButton ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                 <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                <%--<asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />--%>
            </td>
        </tr>
</table>

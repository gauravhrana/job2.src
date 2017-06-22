<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="eSettingsListDynamic.ascx.cs" Inherits="Shared.UI.Web.Controls.eSettingsListDynamic" %>


<table   border="0" class="maintable">
    <tr>
    <td>
    <asp:RadioButtonList ID="rbtnList" runat="server" RepeatDirection="Horizontal" 
    OnSelectedIndexChanged="rbtnList_SelectedIndexChanged" AutoPostBack="true">
    <asp:ListItem Text="Table View" Value="GridView" ></asp:ListItem>
    <asp:ListItem Text="Panel View" Value="Repeater" Selected="True" ></asp:ListItem>
    </asp:RadioButtonList>    </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="ReadOnlyGridView" AllowSorting="true"  PageSize="100" Width="1000px" runat="server" AutoGenerateColumns="true"
            AllowPaging="true" ShowFooter="True"  >
                 <RowStyle Height="30px" />
                <AlternatingRowStyle Height="30px" />
                <HeaderStyle Height="40px" />
               <%--<Columns>
               <asp:TemplateField HeaderText="Id" HeaderStyle-Width="20px" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("FieldConfigurationId") %></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Name" HeaderStyle-Width="200px" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("Name") %></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Value" HeaderStyle-Width="200px" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("Value") %></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Width" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("Width") %></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="GridView Priority" HeaderStyle-Width="75px" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("GridViewPriority")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DetailsView Priority" HeaderStyle-Width="75px" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("DetailsViewPriority")%></ItemTemplate>
               </asp:TemplateField>--%>
               <%--<asp:TemplateField HeaderText="AEFLMode Id" HeaderStyle-Width="75px" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("FieldConfigurationModeId")%></ItemTemplate>
               </asp:TemplateField>--%>
                <%-- <asp:TemplateField HeaderText="Horizontal Alignment" HeaderStyle-Width="75px" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("HorizontalAlignment")%></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Formatting" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("Formatting") %></ItemTemplate>
               </asp:TemplateField>
                 <asp:TemplateField HeaderText="Control Type" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate><%# Eval("ControlType") %></ItemTemplate>
                </asp:TemplateField>
                
               </Columns>--%>
            </asp:GridView> 
                       
        </td>
    </tr>
    <tr>
    <td>
    <div id="borderdiv" runat="server" class="DetailControlBorder">
    
     <asp:Repeater ID="ReadOnlyRepeater" runat="server" OnItemDataBound="ItemBound">
     <HeaderTemplate>
              <table>
              <tr>
              <td>
     </HeaderTemplate>
                    <ItemTemplate>
                    <table class="DetailControlBorder">
                        <tr >
                            <td>
                            <asp:HiddenField ID="hdncol" runat="server" Value=' <%# Eval("Name")%> ' />
                            <asp:Repeater ID="InnerRepeater" runat="server">
                            <HeaderTemplate><table></HeaderTemplate>
                            <ItemTemplate>
                            <tr>
                             <td style="text-align:right;">
                              <%# Eval("Name")%> :
                            </td>
                            <td style=" width:100px;">
                               <%# Eval("Value")%>
                            </td>
                            </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            
                            </table>
                            </FooterTemplate>
                            </asp:Repeater>
                            </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterTemplate>
                        </td>
                        </tr>
                        </table>
                    </FooterTemplate>
    </asp:Repeater>
    </div>
    </td>
    </tr>
     <tr>
        <td>
         <div id="griddiv" runat="server">
            <asp:GridView ID="EditableGridView" AllowSorting="false"  PageSize="100" runat="server" AutoGenerateColumns="false"
            AllowPaging="false"  AutoGenerateEditButton="false" 
                AutoGenerateDeleteButton="false" OnRowDeleting="EditableGridView_RowDeleting"
                 OnRowCancelingEdit="EditableGridView_RowCancelingEdit" 
                 OnRowDataBound="EditableGridView_RowDataBound" 
                 OnRowEditing="EditableGridView_RowEditing" 
                 OnRowUpdating="EditableGridView_RowUpdating" ShowFooter="True" style="table-layout:fixed;" Width="1000px" >
                 <RowStyle Height="30px" />
                <AlternatingRowStyle Height="30px" />
                <HeaderStyle Height="40px" />
               <%--<Columns>--%>
               <%--<asp:TemplateField HeaderText="Id" HeaderStyle-Width="20px" ItemStyle-Width="20px">
                <ItemTemplate><asp:Label ID="lblFieldConfigurationId" runat="server" Text='<%# Bind("FieldConfigurationId") %>' Width="30px"></asp:Label></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Name" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                <ItemTemplate><asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' Columns="36"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Value" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                <ItemTemplate><asp:TextBox ID="txtValue" runat="server" Text='<%# Bind("Value") %>' Columns="36"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Width" >
                <ItemTemplate><asp:TextBox ID="txtWidth" runat="server" Text='<%# Bind("Width") %>' Columns="6"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GridView Priority" HeaderStyle-Width="75px" ItemStyle-Width="75px">
                 <ItemTemplate><asp:TextBox ID="txtGridViewPriority" runat="server" Text='<%# Bind("GridViewPriority") %>' style=" text-align:center;" Columns="6"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="DetailsView Priority" HeaderStyle-Width="75px" ItemStyle-Width="75px">
                <ItemTemplate> <asp:TextBox ID="txtDetailsViewPriority" runat="server" Text='<%# Bind("DetailsViewPriority") %>' style=" text-align:center;" Columns="6"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>--%>
                <%-- <asp:TemplateField HeaderText="AEFLMode Id" HeaderStyle-Width="75px" ItemStyle-Width="75px">
                <ItemTemplate> <asp:TextBox ID="txtFieldConfigurationModeId" runat="server" Text='<%# Bind("FieldConfigurationModeId") %>' style=" text-align:center;" Columns="6"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>--%>
                <%-- <asp:TemplateField HeaderText="Horizontal Alignment" HeaderStyle-Width="75px" ItemStyle-Width="75px">
                <ItemTemplate><asp:TextBox ID="txtHorizontalAlignment" runat="server" Text='<%# Bind("HorizontalAlignment") %>' Columns="6"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Formatting">
                <ItemTemplate> <asp:TextBox ID="txtFormatting" runat="server" Text='<%# Bind("Formatting") %>' Columns="6"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Control Type">
                <ItemTemplate><asp:TextBox ID="txtControlType" runat="server" Text='<%# Bind("ControlType") %>' Columns="12"></asp:TextBox></ItemTemplate>
                </asp:TemplateField>--%>
               <%--</Columns>--%>
            </asp:GridView> 
            </div>               
        </td>
    </tr>
    <tr>
    <td>
    <div id="Div1" runat="server" class="DetailControlBorder">
     <asp:Repeater ID="EditableRepeater" runat="server" OnItemDataBound="ItemBound">
     <HeaderTemplate>
              <table>
              <tr>
              <td>
     </HeaderTemplate>
                    <ItemTemplate>
                    <table class="DetailControlBorder">
                        <tr >
                            <td>
                            <asp:HiddenField ID="hdncol" runat="server" Value=' <%# Eval("Name")%> ' />
                            <asp:HiddenField ID="hdnfcid" runat="server" Value=' <%# Eval("FieldConfigurationId")%> ' />
                            <asp:HiddenField ID="hdnfcmid" runat="server" Value=' <%# Eval("FieldConfigurationModeId")%> ' />
                            <asp:Repeater ID="InnerRepeater" runat="server">
                            <HeaderTemplate><table></HeaderTemplate>
                            <ItemTemplate>
                            <tr>
                             <td style="text-align:right;">
                             <asp:Label ID="lblcolname" Text='<%# Eval("Name")%>' runat="server"></asp:Label>
                            </td>
                            <td style=" width:100px;">
                               <asp:TextBox ID="txt" runat="server"   Text=' <%# Eval("Value")%>' ></asp:TextBox>
                            </td>
                            </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            
                            </table>
                            </FooterTemplate>
                            </asp:Repeater>
                            </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterTemplate>
                        </td>
                        </tr>
                        </table>
                    </FooterTemplate>
    </asp:Repeater>

    </div>
    
    </td>
    </tr>

      <tr>
            <td align="right">
                 <asp:LinkButton ID="btnUpdateReturn" runat="server" Text="Update and Return " OnClick="btnUpdateReturn_Click" />
                 <br /><br />
                <asp:LinkButton ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                <%--<asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />--%>
            </td>
        </tr>
</table>

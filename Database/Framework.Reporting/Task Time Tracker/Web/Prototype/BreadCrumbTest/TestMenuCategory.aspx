<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="TestMenuCategory.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.BreadCrumbTest.TestMenuCategory" %>


<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
  
           
               
                <table>
                  
                      <tr>
                            <td>
                                        <asp:Label ID="Label1" runat="server" Text="Menu Category :      " />
                                   
                                        <asp:DropDownList ID="drpMenuCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpMenuCategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr><td></td></tr>

                </table>
                <%--<h2>
                 
                </h2>--%>
           
            <div class="clear hideSkiplink">
                <asp:Menu ID="NavigationTestMenu" runat="server" CssClass="menu" EnableViewState="false"
                    IncludeStyleBlock="false" Orientation="Horizontal">
                </asp:Menu>
            </div>
        
    
  </asp:Content>


<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.master" CodeBehind="CrossReference.aspx.cs" Inherits="Shared.UI.Web.Configuration.MenuCategoryXMenu.CrossReference" %>

<%@ Register Src="~/Shared/Controls/Bucket.ascx" TagName="Bucket" TagPrefix="uc1" %>
 <%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>   
<asp:Content ID="DefaultContent" ContentPlaceHolderID="SearchControlItem" runat="server">
    <table>              
        <tr>
            <td>
                <asp:PlaceHolder ID="dynMenuCategory" runat="server" Visible="true">
                    <table>
                        <tr>
                            <td width="150">
                                MenuCategory:
                            </td>
                            <td>
                            <asp:DropDownList ID="drpMenuCategory" Width="250" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpMenuCategory_SelectedIndexChanged">
                                </asp:DropDownList>                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <uc1:Bucket ID="BucketOfMenu" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
    <div class="clear hideSkiplink">
                <asp:Menu ID="NavigationTestMenu" runat="server" CssClass="menu" EnableViewState="false"
                    IncludeStyleBlock="false" Orientation="Horizontal">
                </asp:Menu>
            </div>
</asp:Content>
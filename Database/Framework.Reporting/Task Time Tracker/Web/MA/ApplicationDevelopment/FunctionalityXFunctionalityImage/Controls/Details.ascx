<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityImage.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  >
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityXFunctionalityImageIdText" runat="server">FunctionalityXFunctionalityImageId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityXFunctionalityImageId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynFunctionalityXFunctionalityImageId" runat="server" />
            </td>
        </tr>
       
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityText" runat="server">Functionality :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
       
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblFunctionalityImageText" runat="server">FunctionalityImage :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFunctionalityImageId" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblKeyStringText" runat="server">KeyString :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblKeyString" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
                <asp:Label ID="lblTitleText" runat="server">Title :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
                <asp:Label ID="lblDescriptionText" runat="server">Description :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                <asp:Label ID="lblSortOrderText" runat="server">SortOrder :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSortOrder" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
       <tr>
            <td class="ralabel">
                <asp:Label ID="lblCreatedByText" runat="server">CreatedBy :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedBy" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
		  <tr>
            <td class="ralabel">
                <asp:Label ID="lblCreatedDateText" runat="server"><span>CreatedDate: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
            </td>
        </tr> 
         <tr>
            <td class="ralabel">
                <asp:Label ID="lblUpdatedByText" runat="server">UpdatedBy :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUpdatedBy" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
		  <tr>
            <td class="ralabel">
                <asp:Label ID="lblUpdatedDateText" runat="server"><span>UpdatedDate: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUpdatedDate" runat="server"></asp:Label>
            </td>
        </tr>
       

    </table>
    <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
    <table>
        <tr>
            <td colspan="2">
                <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblHistory" runat="server" Text=""><b>Record History</b></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dc:List ID="oHistoryList" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>
</div>

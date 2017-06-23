<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.BatchFile.Controls.Details" %>
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
                <asp:Label ID="lblBatchFileIdText" runat="server" >BatchFileId :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBatchFileId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynBatchFileId" runat="server" />
            </td>
        </tr>
       <tr>
            <td class="ralabel">
                 <asp:Label ID="lblBatchFileSetText" runat="server">BatchFileSet :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBatchFileSet" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblSystemEntityTypeText" runat="server">SystemEntityType :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSystemEntityType" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblFileTypeText" runat="server">FileType :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFileType" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblBatchFileStatusText" runat="server">BatchFileStatus :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBatchFileStatus" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblNameText" runat="server">Name :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblFolderText" runat="server">Folder :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFolder" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblBatchFileText" runat="server">BatchFile :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBatchFile" runat="server"></asp:Label>
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
                 <asp:Label ID="lblCreatedDateText" runat="server">CreatedDate :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="ralabel">
                 <asp:Label ID="lblCreatedByPersonText" runat="server">CreatedByPerson :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCreatedByPerson" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                 <asp:Label ID="lblErrorsText" runat="server">Errors :</asp:Label>
            </td>
            <td>
                <asp:Label ID="lblErrors" runat="server"></asp:Label>
            </td>
            <td>
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

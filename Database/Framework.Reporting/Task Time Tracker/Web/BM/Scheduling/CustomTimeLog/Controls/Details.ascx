<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.CustomTimeLog.Controls.Details" %>

<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel"
    TagPrefix="db" %>
<%@ Register TagPrefix="ui" TagName="UpdateInfo" Src="~/Shared/Controls/UpdateInfo.ascx" %>
<div id="borderdiv" runat="server">
    <table class="table table-striped" >
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblCustomTimeLogIdText" runat="server">CustomTimeLogId: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCustomTimeLogId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynCustomTimeLogId" runat="server" />
            </td>
        </tr>
         <tr>
            <td >
                <asp:Label class="control-label" ID="lblCustomTimeLogKeyText" runat="server">CustomTimeLogKey: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCustomTimeLogKey" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynCustomTimeLogKey" runat="server" />
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblPersonText" runat="server">Person: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPersonId" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblCustomTimeCategoryText" runat="server">CustomTimeCategory: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCustomTimeCategory" runat="server"></asp:Label>
            </td>
        </tr>
        
       
        <tr>
            <td >
                <asp:Label class="control-label" ID="lblNoofFilesPromotedText" runat="server">No of Files Promoted: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNoofFilesPromoted" runat="server"></asp:Label>
            </td>
        </tr>
       
       
        <tr>
            <td colspan="3">
                <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
            </td>
        </tr>
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

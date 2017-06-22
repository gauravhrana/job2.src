<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.ScheduleDetail.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>

<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %>
<%@ Register TagPrefix="ui" TagName="UpdateInfo" Src="~/Shared/Controls/UpdateInfo.ascx" %>

<div class="panel panel-info table-bordered">

    <div class="form-horizontal">

        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblScheduleDetailIdText" runat="server" CssClass="control-label">ScheduleDetailId:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblScheduleDetailId" runat="server"></asp:Label></div>
        </div>
        
        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblScheduleIdText" runat="server" CssClass="control-label">ScheduleId:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblScheduleId" runat="server"></asp:Label></div>
        </div>

        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblScheduleDetailActivityCategoryText" runat="server" CssClass="control-label">ScheduleDetailActivityCategory:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblScheduleDetailActivityCategory" runat="server"></asp:Label></div>
        </div>   
        
        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblInTimeText" runat="server" CssClass="control-label">InTime:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblInTime" runat="server"></asp:Label></div>
        </div>      

       <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblOutTimeText" runat="server" CssClass="control-label">OutTime:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblOutTime" runat="server"></asp:Label></div>
        </div>      

        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblWorkTicketText" runat="server" CssClass="control-label">WorkTicket:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblWorkTicket" runat="server"></asp:Label></div>
        </div>

        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblMessageText" runat="server" CssClass="control-label">Message:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblMessage" runat="server"></asp:Label></div>
        </div>
        
         <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblCreatedByAuditIdText" runat="server">Created By AuditId:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblCreatedByAuditId" runat="server"></asp:Label></div>
        </div>
          <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblModifiedByAuditIdText" runat="server">Modified By AuditId:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblModifiedByAuditId" runat="server"></asp:Label></div>
        </div>
         </div>
          <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblCreatedDateText" runat="server">Created Date:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblCreatedDate" runat="server"></asp:Label></div>
        </div>
    </div>
          <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblModifiedDateText" runat="server">Modified Date:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblModifiedDate" runat="server"></asp:Label></div>
        </div>
        <div class="form-group">
            <div class="col-sm-12"><ui:UpdateInfo ID="oUpdateInfo" runat="server" /></div>
        </div>

        <div class="form-group">
            <div class="col-sm-2 control-label"></div>
            <div class="col-sm-10"><db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" /></div>
        </div>

        <div class="form-group">
            <div class="col-sm-2 control-label"></div>
            <div class="col-sm-10"><asp:PlaceHolder ID="dynScheduleDetailId" runat="server" /></div>
        </div>

    <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-sm-2 control-label">
                    <asp:Label ID="lblHistory" runat="server" Text=""><b>Record History</b></asp:Label>
                </div>
                <div class="col-sm-10"><dc:List ID="oHistoryList" runat="server" /></div>
            </div>
        </div>                    
    </asp:PlaceHolder>
        

    <div id="borderdiv" runat="server">        
        <table  >                                                    
        </table>       
    </div>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.ProjectManagement.ProjectTimeLine.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %> 
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>

<div id="borderdiv" runat="server">
    
        <div class="ralabel">
            <div class="col-sm-2 control-label"></div>
            <div class="col-sm-10"><db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" /></div>
        </div>

 <div class="ralabel">
            <div class="col-sm-2 control-label"><asp:Label ID="lblProjectTimeLineIdText" runat="server"
                 CssClass="control-label">ProjectTimeLineId:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblProjectTimeLineId" runat="server"></asp:Label></div>
        </div>
    <div class="form-group">
            <div class="col-sm-2 control-label"></div>
            <div class="col-sm-10"><asp:PlaceHolder ID="dynProjectTimeLineId" runat="server" /></div>
        </div>

       
     <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblProjectIdText" runat="server"
                 CssClass="control-label">ProjectId:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblProjectId" runat="server"></asp:Label></div>
        </div>
       
             <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblStartDateText" runat="server" 
                CssClass="control-label">lblStartDate:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblStartDate" runat="server"></asp:Label></div>
</div>
    <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblEndDateText" runat="server" 
                CssClass="control-label">EndDate :</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblEndDate" runat="server"></asp:Label></div>
        </div>    

         <div class="form-group">
            <div class="col-sm-12"><ui:UpdateInfo ID="oUpdateInfo" runat="server" /></div>
        </div>

</div>
<asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-sm-2 control-label">
                     <asp:Label ID="lblHistory" runat="server" Text=""><b>Record History</b></asp:Label>
                </div>
                <div class="col-sm-10">
                    <dc:List ID="oHistoryList" runat="server" />
                </div>
            </div>
        </div>
    </asp:PlaceHolder>

<div id="Div1" runat="server">
        <table  >
        </table>
    </div>

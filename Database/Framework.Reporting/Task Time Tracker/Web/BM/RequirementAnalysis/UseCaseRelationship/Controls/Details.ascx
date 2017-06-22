<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.UseCaseRelationship.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>

<div class="panel panel-info">

    <div class="form-horizontal">

        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblUseCaseRelationshipIdText" runat="server">Use Case RelationshipId:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblUseCaseRelationshipId" runat="server"></asp:Label></div>
        </div>
       <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblNameText" runat="server">Name:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblName" runat="server"></asp:Label></div>
        </div>
        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblDescriptionText" runat="server">Description:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblDescription" runat="server"></asp:Label></div>
        </div>
        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblSortOrderText" runat="server">Sort Order:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblSortOrder" runat="server"></asp:Label></div>
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
            <div class="col-sm-10"><asp:PlaceHolder ID="dynUseCaseRelationshipId" runat="server" /></div>
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

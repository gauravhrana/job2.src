<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.Productivity.ProductivityArea.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>

<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %>
<%@ Register TagPrefix="ui" TagName="UpdateInfo" Src="~/Shared/Controls/UpdateInfo.ascx" %>

<div class="panel panel-info table-bordered">

    <div class="form-horizontal">

        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblProductivityAreaIdText" runat="server" CssClass="control-label">ProductivityAreaId:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblProductivityAreaId" runat="server"></asp:Label></div>
        </div>

        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblNameText" runat="server" CssClass="control-label">Name:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblName" runat="server"></asp:Label></div>
        </div>

        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblDescriptionText" runat="server" CssClass="control-label">Description:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblDescription" runat="server"></asp:Label></div>
        </div>

        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblSortOrderText" runat="server" CssClass="control-label">Sort Order:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblSortOrder" runat="server"></asp:Label></div>
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
            <div class="col-sm-10"><asp:PlaceHolder ID="dynProductivityAreaId" runat="server" /></div>
        </div>

    </div>

    <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-sm-2 control-label">
                    <asp:Label ID="lblHistory" runat="server" Text="Record History"/>
                </div>
                <div class="col-sm-10">
                    <dc:List ID="oHistoryList" runat="server" />
                </div>
            </div>
        </div>
    </asp:PlaceHolder>

    <div id="borderdiv" runat="server">
        <table  >
        </table>
    </div>

</div>

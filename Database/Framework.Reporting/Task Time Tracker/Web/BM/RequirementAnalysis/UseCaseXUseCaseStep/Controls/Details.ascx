<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="ApplicationContainer.UI.Web.UseCaseXUseCaseStep.Controls.Details" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %> 
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>

<div class="panel panel-info">

    <div class="form-horizontal">

        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblUseCaseXUseCaseStepIdText" runat="server">Use Case X Use Case StepId:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblUseCaseXUseCaseStepId" runat="server"></asp:Label></div>
        </div>
       <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblUseCaseText" runat="server">Use Case:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblUseCase" runat="server"></asp:Label></div>
        </div>
        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblUseCaseWorkFlowCategoryText" runat="server">Use Case Work Flow Category:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblUseCaseWorkFlowCategory" runat="server"></asp:Label></div>
        </div>
        <div class="form-group">
            <div class="col-sm-2 control-label"><asp:Label ID="lblUseCaseStepText" runat="server">Use Case Step:</asp:Label></div>
            <div class="col-sm-10"><asp:Label ID="lblUseCaseStep" runat="server"></asp:Label></div>
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
            <div class="col-sm-10"><asp:PlaceHolder ID="dynUseCaseXUseCaseStepId" runat="server" /></div>
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

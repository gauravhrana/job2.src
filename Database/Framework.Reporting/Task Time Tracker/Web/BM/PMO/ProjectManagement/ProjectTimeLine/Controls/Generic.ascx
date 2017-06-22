<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.ProjectManagement.ProjectTimeLine.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
    

<div class="form-horizontal">

    
    <div class="ralabel">
        <asp:Label ID="lblProjectTimeLineId" Text="ProjectTimeLineId:" runat="server" 
            CssClass="col-sm-2 control-label" AssociatedControlID="txtProjectPortfolioGroupId"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtProjectTimeLineId" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynProjectTimeLineId" runat="server" />
        </div>
    </div>


    <div class="ralabel">
        <asp:Label ID="lblProjectId" Text="ProjectId:" runat="server"></asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpProjectList" runat="server" OnSelectedIndexChanged="drpProjectList_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TextBox ID="txtProjectId" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynProjectId" runat="server" />
        </div>
    </div>

    <div class="ralabel">
        <asp:Label ID="lblStartDate" Text="StartDate:" runat="server"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynStartDate" runat="server" />
        </div>
    </div>

    <div class="ralabel">
        <asp:Label ID="lblEndDate" Text="EndDate:" runat="server"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
        </div>
        <div class="col-sm-2">
        </div>
    </div>

    <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
</div>

<asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label ID="lblHistory" runat="server" Text="" CssClass="col-sm-2 control-label"/>
            <div class="col-sm-10">.</div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label">Record History:</label>
            <div class="col-sm-10 control-label">
                <dc:List ID="oHistoryList" runat="server" />
            </div>
        </div>
    </div>
</asp:PlaceHolder>

<div id="borderdiv" runat="server">
    <table  >
    </table>
</div>

<script type="text/javascript">
    $(function () {
        $("#<%= txtStartDate.ClientID  %>").datepicker();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("#<%= txtEndDate.ClientID  %>").datepicker();
        });
    </script>
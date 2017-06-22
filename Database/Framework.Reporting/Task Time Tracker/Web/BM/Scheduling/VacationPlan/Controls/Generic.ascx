<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.VacationPlan.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>

<script>
    $(document).ready(function () {
        $("#<%=txtStartDate.ClientID%>").datepicker({
            dateFormat: '<%= ConvertDateTimeFormat %>'
        });

        $("#<%=txtEndDate.ClientID%>").datepicker({
            dateFormat: '<%= ConvertDateTimeFormat %>'
        });
    });
</script>
<div class="form-horizontal">

    <div class="form-group">
        <asp:Label ID="lblVacationPlanId" Text="VacationPlan Id:" runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="txtVacationPlanId"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtVacationPlanId" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynVacationPlanId" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="<%=txtApplicationUserList.ClientID%>">Application User:</label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtApplicationUserList" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:DropDownList ID="drpApplicationUserList" runat="server" OnSelectedIndexChanged="drpApplicationUserList_SelectedIndexChanged" Visible="false">
            </asp:DropDownList>

            <asp:TextBox ID="txtApplicationUserId" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynApplicationUserId" runat="server"></asp:PlaceHolder>
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="<%=txtStartDate.ClientID%>">StartDate:</label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynStartDate" runat="server"></asp:PlaceHolder>
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="<%=txtEndDate.ClientID%>">EndDate:</label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynEndDate" runat="server"></asp:PlaceHolder>
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="<%=txtName.ClientID%>">Name:</label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
            <span class="help-block">.</span>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynName" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="txtDescription">Description:</label>
        <div class="col-sm-8">
            <textarea id="txtDescription" runat="server" rows="3" cols="50" cssclass="form-control"></textarea>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynDescription" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="<%=txtSortOrder.ClientID%>">Sort Order:</label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtSortOrder" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
        </div>
    </div>

    <ui:UpdateInfo ID="oUpdateInfo" runat="server" />

</div>

<asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label ID="lblHistory" runat="server" Text="" CssClass="col-sm-2 control-label" />
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
    <table>
    </table>
</div>

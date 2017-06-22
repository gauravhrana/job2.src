<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.ModuleOwner.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>

<div class="form-horizontal">
    <div class="form-group">
        <asp:Label ID="lblModuleOwnerId" Text="ModuleOwnerId:" CssClass="col-sm-2 control-label"
            runat="server" AssociatedControlID="txtModuleOwnerId"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtModuleOwnerId" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynModuleOwnerId" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <asp:Label Text="Module:" ID="lblModule" CssClass="col-sm-2 control-label" runat="server" AssociatedControlID="drpModuleList"></asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpModuleList" runat="server" OnSelectedIndexChanged="drpModuleList_SelectedIndexChanged"  CssClass="form-control">
            </asp:DropDownList>

            <asp:TextBox ID="txtModule" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynModuleId" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <asp:Label Text="Application:" ID="lblApplication" CssClass="col-sm-2 control-label" runat="server" AssociatedControlID="drpApplication"></asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpApplication" runat="server" OnSelectedIndexChanged="drpApplication_SelectedIndexChanged"
                Width="155" CssClass="form-control">
            </asp:DropDownList>
            <asp:TextBox ID="txtApplication" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynApplication" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <asp:Label Text="Developer Role:" ID="lblDeveloperRole" CssClass="col-sm-2 control-label" runat="server" AssociatedControlID="drpDeveloperRoleList"></asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpDeveloperRoleList" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpDeveloperRoleList_SelectedIndexChanged">
            </asp:DropDownList>

            <asp:TextBox ID="txtDeveloperRole" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynDeveloperRoleId" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <asp:Label Text="Developer:" ID="lblDeveloper" CssClass="col-sm-2 control-label" runat="server" AssociatedControlID="txtDeveloper"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtDeveloper" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynDeveloper" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <asp:Label Text="Feature Owner Status:" ID="lblFeatureOwnerStatus" CssClass="col-sm-2 control-label" runat="server" AssociatedControlID="drpFeatureOwnerStatusList"></asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpFeatureOwnerStatusList" CssClass="form-control" runat="server" OnSelectedIndexChanged="drpFeatureOwnerStatusList_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TextBox ID="txtFeatureOwnerStatus" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynFeatureOwnerStatusId" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <asp:Label Text="Total Hours Worked:" ID="lblTotalHoursWorked" CssClass="col-sm-2 control-label" runat="server" AssociatedControlID="txtTotalHoursWorked"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtTotalHoursWorked" runat="server" CssClass="form-control" ></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynTotalHoursWorked" runat="server" />
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


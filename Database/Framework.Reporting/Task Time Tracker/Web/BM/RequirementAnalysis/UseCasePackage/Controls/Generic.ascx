﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.UseCasePackage.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/KendoTextEditor/KendoEditor.ascx" TagPrefix="uc1" TagName="KendoEditor" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>

<div class="form-horizontal">

    <div class="form-group">
        <asp:Label ID="lblUseCasePackageId" Text="UseCasePackage Id:" runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="txtUseCasePackageId"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtUseCasePackageId" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynUseCasePackageId" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="<%=txtName.ClientID%>">Name:</label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>           
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynName" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="txtDescription" >Description:</label>
        <div class="col-sm-8">
            <textarea id="txtDescription" runat="server" rows="3" cols="50" CssClass="form-control"></textarea>
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
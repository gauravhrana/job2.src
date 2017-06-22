<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.Configuration.Menu.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%--<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>--%>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>

<div class="form-horizontal">
    <div class="form-group">
        <asp:Label ID="lblMenuId" runat="server" Text="MenuId:" CssClass="col-sm-2 control-label" AssociatedControlID="txtMenuId"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtMenuId" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynMenuId" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <asp:Label ID="lblName" runat="server" Text="Name:" CssClass="col-sm-2 control-label" AssociatedControlID="txtName"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynName" runat="server" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblDisplayName" runat="server" Text="DisplayName:" CssClass="col-sm-2 control-label" AssociatedControlID="txtDisplayName"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynDisplayName" runat="server" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblApplication" runat="server" Text="Application:" CssClass="col-sm-2 control-label" AssociatedControlID="drpApplicationName"></asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpApplicationName" runat="server" OnSelectedIndexChanged="drpApplicationName_SelectedIndexChanged"
                AppendDataBoundItems="true" CssClass="form-control">
                <asp:ListItem Selected="True" Value="-1">None</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtApplicationId" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynApplicationId" runat="server" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblParentMenu" runat="server" Text="Parent Menu:" CssClass="col-sm-2 control-label" AssociatedControlID="drpParentMenu"></asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpParentMenu" runat="server" OnSelectedIndexChanged="drpParentMenu_SelectedIndexChanged"
                AppendDataBoundItems="true" CssClass="form-control">
                <asp:ListItem Selected="True" Value="-1">None</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtParentMenuId" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynParentMenuId" runat="server" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblApplicationModule" runat="server" Text="Application Module:" CssClass="col-sm-2 control-label" AssociatedControlID="txtApplicationModule"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtApplicationModule" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynApplicationModule" runat="server" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblPrimaryDeveloper" runat="server" Text="Primary Developer:" CssClass="col-sm-2 control-label" AssociatedControlID="txtPrimaryDeveloper"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtPrimaryDeveloper" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynPrimaryDeveloper" runat="server" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblDescription" runat="server" Text="Description:" CssClass="col-sm-2 control-label" AssociatedControlID="txtDescription"></asp:Label>
        <div class="col-sm-8">
            <textarea id="txtDescription" runat="server" cols="50" rows="3" cssclass="form-control"></textarea>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynDescription" runat="server" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblSortOrder" runat="server" Text="Sort Order:" CssClass="col-sm-2 control-label" AssociatedControlID="txtSortOrder"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtSortOrder" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblIsChecked" runat="server" Text="Is Checked:" CssClass="col-sm-2 control-label" AssociatedControlID="chkIsVisible"></asp:Label>
        <div class="col-sm-8">
            <asp:CheckBox ID="chkIsVisible" runat="server"  />
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynIsVisible" runat="server" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblIsVisible" runat="server" Text="Is Visible:" CssClass="col-sm-2 control-label" AssociatedControlID="chkIsChecked"></asp:Label>
        <div class="col-sm-8">
            <asp:CheckBox ID="chkIsChecked" runat="server"  />
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynIsChecked" runat="server" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblNavigateURL" runat="server" Text="Navigate URL:" CssClass="col-sm-2 control-label" AssociatedControlID="txtNavigateURL"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtNavigateURL" runat="server" CssClass="form-control" />
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynNavigateURL" runat="server" />
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

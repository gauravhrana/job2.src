<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginSettings.ascx.cs"
    Inherits="Shared.UI.Web.Admin.Controls.LoginSettings" %>

<div class="form-horizontal">

    <div class="form-group">
        <asp:Label ID="lblUserApplicationModeId" Text="UserApplicationMode:" runat="server" CssClass="col-sm-2 control-label"> User Application Mode:</asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpIsTesting" runat="server">
            </asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblDateFormat" Text="Date Format:" runat="server" CssClass="col-sm-2 control-label"> Date Format:</asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpDateFormat" runat="server">
                <asp:ListItem Value="dd-MM-yy">dd-MM-yy</asp:ListItem>
                <asp:ListItem Value="yyyy-MM-dd">yyyy-MM-dd</asp:ListItem>
                <asp:ListItem Value="MM-dd-yyyy">MM-dd-yyyy</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="Label1" Text="Time Format:" runat="server" CssClass="col-sm-2 control-label">Time Format:</asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpTimeFormat" runat="server">
                <asp:ListItem Value="hh:mm:ss tt">hh:mm:ss tt</asp:ListItem>
                <asp:ListItem Value="HH:mm:ss">HH:mm:ss</asp:ListItem>
                <asp:ListItem Value="HH:m:s t">HH:m:s t</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblTheme" Text="Theme:" runat="server" CssClass="col-sm-2 control-label">Theme:</asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpTheme" runat="server">
            </asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblPleaseenteravalueforauditIdorleaveitasdefault" Text="Please enter a value for auditId or leave it as default:" runat="server" CssClass="col-sm-2 control-label"> Please enter a value for auditId or leave it as default:</asp:Label>

        <div class="col-sm-8">
            <asp:TextBox ID="txtAuditId" runat="server" />
            <asp:PlaceHolder ID="dynAuditId" runat="server" />
            <asp:CustomValidator ID="vldCode" runat="server" ErrorMessage="This value doesn't exist in the Database."
                ValidateEmptyText="False" OnServerValidate="vldCode_ServerValidate" ControlToValidate="txtAuditId" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblCountry" Text="Country:" runat="server" CssClass="col-sm-2 control-label">Country:</asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpCountry" runat="server">
            </asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblLanguage" Text="Language:" runat="server" CssClass="col-sm-2 control-label">Language:</asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpLanguage" runat="server">
            </asp:DropDownList>
        </div>
    </div>

    <div class="form-group">
        <asp:Label runat="server" CssClass="col-sm-2 control-label"> Default Click Action:</asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpDefaultClickAction" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Selected="True" Value="Detail">Detail</asp:ListItem>
                <asp:ListItem Selected="True" Value="Update">Update</asp:ListItem>
                <asp:ListItem Selected="True" Value="InlineUpdate">Inline Update</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblMenuCatgeory" Text="Menu Catgeory:" runat="server" CssClass="col-sm-2 control-label">MenuCatgeory:</asp:Label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpMenuCategory" runat="server">
            </asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <asp:Label CssClass="col-sm-2 control-label" runat="server">Active Client:</asp:Label>
        <div class="col-sm-2">
            <asp:TextBox ID="txtActiveClient" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynActiveClient" runat="server" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label CssClass="col-sm-2 control-label" runat="server">Active Project:</asp:Label>
        <div class="col-sm-2">
            <asp:TextBox ID="txtActiveProject" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynActiveProject" runat="server" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label CssClass="col-sm-2 control-label" runat="server">Active Need:</asp:Label>
        <div class="col-sm-2">
            <asp:TextBox ID="txtActiveNeed" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynActiveNeed" runat="server" />
        </div>
    </div>
    <div class="form-group">
        <asp:Label CssClass="col-sm-2 control-label" runat="server">Active Task:</asp:Label>
        <div class="col-sm-2">
            <asp:TextBox ID="txtActiveTask" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynActiveTask" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <asp:Label CssClass="col-sm-2 control-label" runat="server">Default Row Count:</asp:Label>
        <div class="col-sm-2">
            <asp:TextBox ID="txtDefaultRowCount" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynDefaultRowCount" runat="server" />
        </div>
    </div>
    <tr>
        <td></td>
        <td align="center">
            <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_OnClick"
                CausesValidation="true" />
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_OnClick" />
        </td>
    </tr>
    <div id="borderdiv" runat="server">
        <table>
        </table>
    </div>
</div>

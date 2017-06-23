<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FieldConfigurationSettings.ascx.cs"
    Inherits="Shared.UI.Web.Admin.Controls.FieldConfigurationSettings" %>
<%@ Register TagPrefix="eg" TagName="eSettingsGrid" Src="~/Shared/Controls/eSettingsGrid.ascx" %>
<%@ Register TagPrefix="er" TagName="eSettingsRepeater" Src="~/Shared/Controls/eSettingsRepeater.ascx" %>
<%@ Register TagPrefix="GenList" TagName="GenericList" Src="~/Configuration/FieldConfiguration/Controls/Generic.ascx" %>
<%@ Register TagPrefix="dc" TagName="DetailsView" Src="~/Configuration/FieldConfiguration/Controls/Details.ascx" %>

<table cellpadding="2" cellspacing="2" class="filterTable" >
    <tr>
        <td colspan="2">Search</td>
    </tr>
    <tr runat="server">
        <td class="ralabel" style="width: 250px;">Application:</td>
        <td>
            <asp:TextBox ID="txtApplicationList" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:DropDownList ID="ddlApplication" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlApplication_SelectedIndexChanged" Visible ="false">
            </asp:DropDownList>
        </td>
        <td>
             <asp:placeholder ID="dynApplicationId" runat="server"></asp:placeholder>
         </td>
    </tr>
    <tr>
        <td class="ralabel">RelativeUser:</td>
        <td>
            <asp:TextBox ID="txtRelativeUser" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:DropDownList ID="ddlRelativeApplicationUser" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlRelativeApplicationUser_SelectedIndexChanged" AppendDataBoundItems="true" Visible="false">
                <asp:ListItem Value="-1">All</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
             <asp:placeholder ID="dynRelativeUser" runat="server"></asp:placeholder>
         </td>
    </tr>
    <tr>
        <td class="ralabel">Entity:</td>
        <td>
            <asp:TextBox ID="txtEntity" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:DropDownList ID="ddlSystemEntityType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSystemEntityType_SelectedIndexChanged" Visible="false">
            </asp:DropDownList>
        </td>
        <td>
             <asp:placeholder ID="dynSystemEntity" runat="server"></asp:placeholder>
         </td>
    </tr>
    <tr>
        <td class="ralabel">Mode Category:</td>
        <td>
            <asp:TextBox ID="txtModeCategory" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:DropDownList ID="ddlFCModeCategory" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlFCModeCategory_SelectedIndexChanged" Visible="false">
            </asp:DropDownList>
        </td>
        <td>
             <asp:placeholder ID="dynModeCategory" runat="server"></asp:placeholder>
         </td>
    </tr>
    <tr>
        <td class="ralabel">Mode:</td>
        <td>
            <asp:TextBox ID="txtMode" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:DropDownList ID="ddlFCMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFCMode_SelectedIndexChanged" Visible="false">
            </asp:DropDownList>
        </td>
        <td>
             <asp:placeholder ID="dynMode" runat="server"></asp:placeholder>
         </td>
    </tr>
    <tr>
        <td class="ralabel">Application Role:</td>
        <td>
            <asp:TextBox ID="txtApplicationRole" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:DropDownList ID="ddlApplicationRole" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlApplicationRole_SelectedIndexChanged" AppendDataBoundItems="true" Visible ="false">
                <asp:ListItem Value="-1">All</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
             <asp:placeholder ID="dynApplicationRole" runat="server"></asp:placeholder>
         </td>
    </tr>
    <tr>
        <td class="ralabel">Application User:</td>
        <td>
            <asp:TextBox ID="txtApplicationUser" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:DropDownList ID="ddlApplicationUser" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlApplicationUser_SelectedIndexChanged" AppendDataBoundItems="true" Visible ="false">
                <asp:ListItem Value="-1">All</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
             <asp:placeholder ID="dynApplicationUser" runat="server"></asp:placeholder>
         </td>
    </tr>
    <tr>
        <td></td>
        <td horizontalalign="Right" columnspan="2">
            <asp:Button ID="btnGetColumns" runat="server" OnClick="btnGetColumns_Click" Text="Get Columns" />
        </td>
    </tr>
</table>
<table  class="maintable"
    >
    <tr>
        <td>
            <asp:RadioButtonList ID="rbtnList" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnList_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="Table View" Value="GridView"></asp:ListItem>
                <asp:ListItem Text="Panel View" Value="Repeater" Selected="True"></asp:ListItem>
            </asp:RadioButtonList>

        </td>
    </tr>
    <tr>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkbtnAddRow" Text="Add Field" OnClick="lnkbtnAddRow_Click" runat="server" />
            &nbsp;&nbsp;<asp:LinkButton ID="lnkCommonUpdate" runat="server" OnClick="lnkCommonUpdate_Click">Common Update</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <br />
            <eg:eSettingsGrid ID="eSettingsGrid" runat="server" />
            <er:eSettingsRepeater ID="eSettingsRepeater" runat="server" />
        </td>

    </tr>
    <tr>
        <td>
            <asp:Panel ID="AddRowPanel" runat="server" Visible="false">
                <genlist:genericlist id="genericList" runat="server" />
                <asp:LinkButton ID="lnkbtnAdd" Text="Save" OnClick="lnkbtnAdd_Click" runat="server" />
                <asp:LinkButton ID="lnkbtnCancel" Text="Cancel" OnClick="lnkbtnCancel_Click" runat="server" />
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr>
        <td align="right">
            <asp:LinkButton ID="btnReturn" Text="Return" OnClick="btnReturn_Click" Style="padding-right: 10px;"
                runat="server" />
        </td>
    </tr>
</table>

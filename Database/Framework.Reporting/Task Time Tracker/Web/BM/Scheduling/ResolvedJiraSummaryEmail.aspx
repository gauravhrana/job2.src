<%@ Page Title="Resolved Jira Summary Email" Language="C#" MasterPageFile="~/MasterPages/Schedule/Default.Master" AutoEventWireup="true"
    CodeBehind="ResolvedJiraSummaryEmail.aspx.cs" Inherits="ApplicationContainer.UI.Web.BM.Scheduling.ResolvedJiraSummaryEmail" %>

<%@ Register Src="~/Shared/Controls/DateRange.ascx" TagPrefix="uc1" TagName="DateRange" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HtmlEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ControlVisibilityManager" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SearchControlItem" runat="server">
    <div class="panel panel-info">
        <div class="panel-heading">
            Search
        </div>

        <div class="collapse in panel-body" id="pnlSearchParameters">
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-2 control-label">Person :</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="drpPersons" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Work Date :</label>
                    <div class="col-md-10">
                        <uc1:DateRange runat="server" ID="oDateRange" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Report Type :</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="drpReportType" runat="server">
                            <asp:ListItem Text="None">None</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Group By :</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="drpGroupBy" runat="server">
                            <asp:ListItem Text="None">None</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Sub Group By :</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="drpSubgroupBy" runat="server">
                            <asp:ListItem Text="None">None</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label"></label>
                <div class="col-md-10">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ListControlItem" runat="server">

    <div class="panel panel-info">
        <div class="panel-heading">
            Email
        </div>
        <div class="panel panel-body">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblEmailAddress" runat="server" Text="Enter Email Address : " />
                        <asp:TextBox ID="txtEmailAddress" runat="server" />
                        &nbsp;&nbsp;
                    <asp:Label ID="lblCCAddress" runat="server" Text="Enter CC Email Address : " />
                        <asp:TextBox ID="txtCCAddress" runat="server" />
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnPreview" Text="Preview Email" runat="server" OnClick="btnPreviewEmail_Click" />
                        &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnSendEmail" Text="SendEmail" OnClick="btnSendEmail_Click" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Issues
        </div>
        <div class="panel panel-body">
            <table>
                <tr>
                    <td>
                        <asp:PlaceHolder ID="contentHolder" runat="server"></asp:PlaceHolder>
                        <asp:Panel ID="panEdit" runat="server" Height="90%" Width="80%">
                            <cc1:Editor ID="Editor1" runat="server" Width="100%" />
                            <asp:Button ID="closeBtn" runat="server" Text="Close" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <ajaxToolkit:ModalPopupExtender ID="Modal1" runat="server" OkControlID="closeBtn"
        TargetControlID="btnPreview" PopupControlID="panEdit" OnLoad="btnPreviewEmail_Click" />

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ActionContent" runat="server">
</asp:Content>

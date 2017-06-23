<%@ Page Title="Database Change Log Report" Language="C#" MasterPageFile="~/MasterPages/Default.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs"
    Inherits="Shared.UI.Web.DatabaseChangeLog.Report" %>

<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>
<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="~/Shared/Admin/DatabaseChangeLog/Controls/SearchFilter.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HtmlEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>


<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
    <sr:SearchFilter ID="oSearchFilter" runat="server" />
</asp:Content>
<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">

    <div class="panel panel-info">
        <div class="panel-heading">
            Email
        </div>
        <div class="panel panel-body">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblEmailAddress" runat="server" Text="Enter Email Address : " />
                        <asp:TextBox ID="txtEmailAddress" runat="server" Text="development-common@indusvalleyresearch.com" />
                        &nbsp;&nbsp;
                    <asp:Label ID="lblCCAddress" runat="server" Text="Enter CC Email Address : " />
                        <asp:TextBox ID="txtCCAddress" runat="server" />
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnPreview" Text="Preview Email" runat="server" OnClick="btnPreviewEmail_Click" />
                        &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnSendEmail" Text="SendEmail" OnClick="btnSendEmail_Click" runat="server" />
                        <asp:Panel ID="panEdit" runat="server" Height="90%" Width="70%">
                            <cc1:Editor ID="Editor1" runat="server" Width="100%" />
                            <asp:Button ID="closeBtn" runat="server" Text="Close" />
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="Modal1" runat="server" OkControlID="closeBtn"
                            TargetControlID="btnPreview" PopupControlID="panEdit" OnLoad="btnPreviewEmail_Click" />
                    </td>
                    <td>
                        <asp:Label ID="lblStatus" runat="server" class="label label-success"></asp:Label>
                    </td>


                </tr>

            </table>

        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            Database Change Log            
        </div>
        <div class="panel panel-body">
            <table width="100%">
                <tr>
                    <td valign="top">
                        <dc:GroupList ID="oGroupList" runat="server" />
                        <asp:PlaceHolder ID="contentHolder" runat="server"></asp:PlaceHolder>
                        <asp:Panel ID="pnlGroupListContainer" runat="server" />
                    </td>

                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ControlVisibilityManager">
    <vc:VCManager ID="oVC" runat="server" />
</asp:Content>


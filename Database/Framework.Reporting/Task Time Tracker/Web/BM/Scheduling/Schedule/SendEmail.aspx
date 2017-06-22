<%@ Page Title="SendEmail" Language="C#" MasterPageFile="~/MasterPages/Schedule/Default.master"
    AutoEventWireup="true" CodeBehind="SendEmail.aspx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.Schedule.SendEmail" %>

<%@ Register TagPrefix="dc" TagName="GroupList" Src="~/Shared/Controls/GroupList.ascx" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>
<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>
<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="./Controls/SendEmailSearchFilter.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HtmlEditor" TagPrefix="cc1" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function dirtypop() {

            alert('hey');
            var divText = document.getElementById("dvPreview");
            var myWindow = window.open('', '', 'width=200,height=100');
            var doc = myWindow.document;
            doc.open();
            doc.write(divText);
            doc.close();

        }

    </script>
</asp:Content>

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
                        <asp:TextBox ID="txtEmailAddress" runat="server" />
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
                </tr>
            </table>
        </div>
    </div>
    <table>
        <tr>
            <td valign="top">
                <dc:GroupList ID="oGroupList" runat="server" />
            </td>

        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ControlVisibilityManager">
    <vc:VCManager ID="oVC" runat="server" />
</asp:Content>

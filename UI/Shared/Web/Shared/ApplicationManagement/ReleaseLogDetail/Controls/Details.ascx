<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="Shared.UI.Web.ApplicationManagement.ReleaseLogDetail.Controls.Details" EnableTheming="false" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/DetailButtonPanel.ascx" TagName="DetailsButtonPanel" TagPrefix="db" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">



    <div class="form-horizontal" role="form">
        <div class="form-group">
            <label for="lblReleaseLogDetailId" class="col-lg-2 control-label">
                <asp:Label ID="lblReleaseLogDetailIdText" runat="server">Release Log Detail Id:</asp:Label></label>
            <div class="col-lg-6">
                <asp:Label ID="lblReleaseLogDetailId" runat="server"></asp:Label></div>
            <div class="col-lg-4">
                <asp:PlaceHolder ID="dynReleaseLogDetailId" runat="server" />
            </div>
        </div>
    </div>


    <table class="table table-striped">
        <tr>
            <td colspan="3" align="right">
                <db:DetailsButtonPanel ID="oDetailButtonPanel" runat="server" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
                <label for="lblReleaseLogId" class="col-sm-4 control-label">
                    <asp:Label ID="lblReleaseLogIdText" runat="server"><span>Release Log: </span></asp:Label>
                </label>
            </td>
            <td>
                <asp:Label ID="lblReleaseLogId" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynReleaseLogId" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblApplicationIdText" runat="server">Application: </asp:Label>
            </td>
            <td>
                <asp:Label ID="lblApplicationId" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Item No:
            </td>
            <td>
                <asp:Label ID="lblItemNo" runat="server"><span>Item No: </span></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Description:
            </td>
            <td>
                <asp:Label ID="lblDescription" runat="server"><span>Description: </span></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Sort Order:
            </td>
            <td>
                <asp:Label ID="lblSortOrder" runat="server"><span>Sort Order: </span></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Requested By:
            </td>
            <td>
                <asp:Label ID="lblRequestedBy" runat="server"><span>Requested By: </span></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Primary Developer:
            </td>
            <td>
                <asp:Label ID="lblPrimaryDeveloper" runat="server"><span>Primary Developer: </span></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Requested Date:
            </td>
            <td>
                <asp:Label ID="lblRequestedDate" runat="server"><span>Requested Date: </span></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>

                <asp:Label ID="lblTimeSpentText" runat="server"><span>Time Spent: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTimeSpent" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblReleaseIssueTypeIdText" Width="150" runat="server"><span>Release Issue Type: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblReleaseIssueType" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynReleaseIssueType" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblReleasePublishCategoryIdText" runat="server"><span>Release Publish Category: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblReleasePublishCategory" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynReleasePublishCategory" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblJIRAText" runat="server"><span>JIRA: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblJIRA" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynJIRA" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFeatureText" runat="server"><span>Feature: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFeature" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynFeature" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblModuleText" runat="server"><span>Module: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModule" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynModule" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblReleaseFeatureText" runat="server"><span>ReleaseFeature: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblReleaseFeature" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynReleaseFeature" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPrimaryEntityText" runat="server"><span>Primary Entity: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPrimaryEntity" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynPrimaryEntity" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSystemEntityTypeText" runat="server"><span>System Entity Type: </span></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSystemEntityType" runat="server"></asp:Label>
            </td>
            <td>
                <asp:PlaceHolder ID="dynSystemEntityType" runat="server" />
            </td>
        </tr>
    </table>

    <ui:UpdateInfo ID="oUpdateInfo" runat="server" />

    <table>
        <tr>
            <td colspan="2">
                <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblHistory" runat="server" Text="" Visible="false"><b>Record History</b></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dc:List ID="oHistoryList" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
    </table>

</div>

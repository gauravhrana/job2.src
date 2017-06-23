<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.ApplicationManagement.ReleaseLogDetail.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register TagPrefix="ui" TagName="UpdateInfo" Src="~/Shared/Controls/UpdateInfo.ascx" %>

<div id="borderdiv" runat="server">
    <table>
        <tr>
            <td>
                <table class="table table-striped">
                    <tr>
                        <td>
                            <asp:Label Width="150" ID="lblReleaseLogDetailId" Text="Release Log Detail Id" CssClass="col-sm-2 control-label"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReleaseLogDetailId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynReleaseLogDetailId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Application:</label></td>
                        <td>
                            <asp:TextBox ID="txtApplicationList" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynApplicationId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Release Log:</label></td>
                        <td>
                            <asp:TextBox ID="txtReleaseLogList" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReleaseLogId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynReleaseLogId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Primary Developer:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrimaryDeveloper" Text="" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynPrimaryDeveloper" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">JIRA:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtJIRA" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynJIRA" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Description:</label>
                        </td>
                        <td>
                            <textarea id="txtDescription" runat="server" cols="100" rows="3" cssclass="form-control"></textarea>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDescription" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Time Spent:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTimeSpent" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynTimeSpent" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Item No:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtItemnNo" runat="server">1</asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynItemNo" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Sort Order:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortOrder" runat="server">1</asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Business Module:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtModuleList" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtModule" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynModule" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Application Feature:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReleaseFeatureList" runat="server" CssClass="form-control" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtReleaseFeatureId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynReleaseFeatureId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">System Entity Type:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntityList" runat="server" CssClass="form-control" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtSystemEntityTypeId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dyndrpSystemEntityTypeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Release Issue Type:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReleaseIssueTypeList" runat="server" CssClass="form-control" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtReleaseIssueTypeId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynReleaseIssueTypeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="col-sm-2 control-label" for="txtName">Release Publish Category:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReleasePublishCategoryList" runat="server" CssClass="form-control" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtReleasePublishCategoryId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynReleasePublishCategoryId" runat="server" />
                        </td>
                    </tr>
                </table>
                <ui:UpdateInfo ID="UpdateInfo" runat="server" />
                <table>
                    <tr>
                        <td colspan="4">
                            <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <a href="~/Shared/ApplicationManagement/ReleaseLogDetail/Controls/SearchFilter.ascx">~/ApplicationManagement/ReleaseLogDetail/Controls/SearchFilter.ascx</a>
                                        <td>
                                            <asp:Label ID="lblHistory" runat="server" Text=""><b>Record History</b></asp:Label>
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
                <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
            </td>
        </tr>
    </table>
</div>

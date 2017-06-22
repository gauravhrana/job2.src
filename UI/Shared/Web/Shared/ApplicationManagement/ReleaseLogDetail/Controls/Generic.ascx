<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.ApplicationManagement.ReleaseLogDetail.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register TagPrefix="ui" TagName="UpdateInfo" Src="~/Shared/Controls/UpdateInfo.ascx" %>
<script type="text/javascript">
    function onResize() {
        //var completionList = $find("AutoCompleteEx").get_completionList();
    }


    function pageLoad() {
        //$find("AutoCompleteEx").get_element().focus();
        //$addHandler(window, "resize", onResize);
    }


    function getLeft(e) {
        var offset = e.offsetLeft;
        if (e.offsetParent != null) offset += getLeft(e.offsetParent);
        return offset;
    }
    function getTop(e) {
        var offset = e.offsetTop;
        if (e.offsetParent != null) offset += getTop(e.offsetParent);
        return offset;
    }


</script>
<div id="borderdiv" runat="server">
    <table>
        <tr>
            <td>
                <table class="table table-striped">
                    <tr>
                        <td>
                            <asp:Label Width="150" ID="lblReleaseLogDetailId" Text="Release Log Detail Id"
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
                            <label class="control-label" for="txtName">Application:</label></td>
                        <td>
                            <asp:DropDownList ID="drpApplicationIdList" runat="server" OnSelectedIndexChanged="drpApplicationIdList_SelectedIndexChanged">
                            </asp:DropDownList>
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
                            <label class="control-label" for="txtName">Release Log:</label></td>
                        <td>
                            <asp:DropDownList ID="drpReleaseLogList" runat="server" OnSelectedIndexChanged="drpReleaseLogListList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtReleaseLogId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynReleaseLogId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="control-label" for="txtName">Primary Developer:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrimaryDeveloper" Text="" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynPrimaryDeveloper" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>JIRA:
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
                            <label class="control-label" for="txtName">Description:</label>
                        </td>
                        <td>
                            <textarea id="txtDescription" runat="server" cols="50" rows="3"></textarea>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynDescription" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="control-label" for="txtName">Time Spent:</label>
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
                            <label class="control-label" for="txtName">Item No:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtItemnNo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynItemNo" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="control-label" for="txtName">Sort Order:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="control-label" for="txtName">Module:</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpModuleList" runat="server" OnSelectedIndexChanged="drpModuleList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtModuleId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynModuleId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="control-label" for="txtName">Release Feature:</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpReleaseFeatureList" runat="server" OnSelectedIndexChanged="drpReleaseFeatureList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtReleaseFeatureId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynReleaseFeatureId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Feature:
                        </td>
                        <td>
                            <div>
                                <asp:TextBox runat="server" ID="txtFeature" />
                                <ajaxToolkit:AutoCompleteExtender ID="autoCompleteExtenderFeature" runat="server" TargetControlID="txtFeature"
                                    MinimumPrefixLength="1" BehaviorID="AutoCompleteFeature" CompletionSetCount="1" ServicePath="~/AutoComplete.asmx"
                                    UseContextKey="True" CompletionInterval="1" ServiceMethod="GetFeatureNames" OnClientShowing="onResize">
                                </ajaxToolkit:AutoCompleteExtender>
                            </div>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFeature" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Primary Entity:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrimaryEntity" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynPrimaryEntity" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>System Entity Type:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSystemEntityTypeList" runat="server" OnSelectedIndexChanged="drpSystemEntityTypeList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSystemEntityTypeId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dyndrpSystemEntityTypeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Release Issue Type:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpReleaseIssueTypeList" runat="server" OnSelectedIndexChanged="drpReleaseIssueTypeList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtReleaseIssueTypeId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynReleaseIssueTypeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Release Publish Category:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpReleasePublishCategoryList" runat="server" OnSelectedIndexChanged="drpReleasePublishCategoryList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtReleasePublishCategoryId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynReleasePublishCategoryId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="control-label" for="txtName">New Application:</label></td>
                        <td>
                            <asp:DropDownList ID="drpNewApplicationIdList" runat="server" OnSelectedIndexChanged="drpNewApplicationIdList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewApplicationId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynNewApplicationId" runat="server" />
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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.ReportCategory.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/KendoTextEditor/KendoEditor.ascx" TagPrefix="uc1" TagName="KendoEditor" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<script type="text/javascript">
    $(function () {
        $("#<%= txtCreatedDate.ClientID  %>").datepicker();
    });
</script>
<script type="text/javascript">
    $(function () {
        $("#<%= txtModifiedDate.ClientID  %>").datepicker();
    });
</script>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblReportCategoryId" Text="Report Category Id:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReportCategoryId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynReportCategoryId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel" valign="top">
                            Description:
                        </td>
                        <td>
                            <div id="example" class="k-content">                             
                                 <uc1:KendoEditor runat="server" id="txtDescription2" />
                             </div>
                          </td>                       
                    </tr>
                    <tr>
                        <td class="ralabel">
                            Sort Order:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSortOrder" runat="server" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="ralabel" style="display: none;">CreatedDate:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreatedDate" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynCreatedDate" runat="server" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="ralabel">ModifiedDate:
                        </td>
                        <td>
                            <asp:TextBox ID="txtModifiedDate" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynModifiedDate" runat="server" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="ralabel">CreatedByAuditId:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreatedByAuditId" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynCreatedByAuditId" runat="server" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="ralabel">ModifiedByAuditId:
                        </td>
                        <td>
                            <asp:TextBox ID="txtModifiedByAuditId" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynModifiedByAuditId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                    <td class="ralabel">
                Application
            </td>
            <td>
                <asp:DropDownList ID="drpApplication" runat="server" OnSelectedIndexChanged="drpApplication_SelectedIndexChanged"
                    Width="155">
                </asp:DropDownList>
                <asp:TextBox ID="txtApplication" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynApplication" runat="server" />
            </td>
                    </table>
                <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
                <table>
                    <tr>
                        <td colspan="4">
                            <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
                                <table>
                                    <tr>
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
            </td>
        </tr>
    </table>
</div>

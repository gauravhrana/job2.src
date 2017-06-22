<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityActiveStatus.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtKnowledgeDate.ClientID%>").datepicker({
            dateFormat: '<%= ConvertDateTimeFormat %>'
        });
    });
</script>
<div id="borderdiv" runat="server">
    <table width="400">
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="ralabel">
                            <b>
                                <asp:Label ID="lblFunctionalityXFunctionalityActiveStatusId" Text="FunctionalityXFunctionalityActiveStatusId:"
                                    runat="server"></asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunctionalityXFunctionalityActiveStatusId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFunctionalityXFunctionalityActiveStatusId" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td class="ralabel">Functionality:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFunctionalityList" runat="server" OnSelectedIndexChanged="drpFunctionalityList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunctionalityId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFunctionalityId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">FunctionalityActiveStatus:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFunctionalityActiveStatusList" runat="server" OnSelectedIndexChanged="drpFunctionalityActiveStatusList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunctionalityActiveStatusId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFunctionalityActiveStatusId" runat="server" />
                        </td>
                    </tr>


                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblMemo" Text="Memo:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMemo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynMemo" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">AcknowledgedBy:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAcknowledgedBy" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynAcknowledgedBy" runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="ralabel">KnowledgeDate:
                        </td>
                        <td>
                            <asp:TextBox ID="txtKnowledgeDate" runat="server"></asp:TextBox>

                        </td>
                        <td></td>
                        <td>
                            <asp:Label ID="lblUserDateTimeFormat" runat="server" Text=""></asp:Label>
                            <asp:PlaceHolder ID="dynKnowledgeDate" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>

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

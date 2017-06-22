<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="Shared.UI.Web.Admin.SystemEntityType.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>

<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtCreatedDate.ClientID%>").datepicker({
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
                        <td class="ralabel">SystemEntityTypeId:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSystemEntityTypeId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSystemEntityTypeId" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">EntityName:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntityName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynEntityName" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">EntityDescription:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntityDescription" runat="server">
                            </asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynEntityDescription" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Primary Database:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrimaryDatabase" runat="server">
                            </asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynPrimaryDatabase" runat="server"></asp:PlaceHolder>
                        </td>
                        <tr>
                            <td class="ralabel">Created Date:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCreatedDate" runat="server">
                                </asp:TextBox>

                            </td>
                            <td>
                                <asp:Label ID="lblUserDateTimeFormat" runat="server" Text=""></asp:Label>
                                <asp:PlaceHolder ID="dynCreatedDate" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                    <tr>
                        <td class="ralabel">NextValue:
                        </td>
                        <td>
                            <asp:TextBox ID="txtNextValue" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynNextValue" runat="server"></asp:PlaceHolder>
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


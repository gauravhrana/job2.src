<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.EntityDateRangeState.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>

<script>
    $(document).ready(function () {
        $("#<%=txtStartDate.ClientID%>").datepicker({           
            dateFormat: '<%= ConvertDateTimeFormat %>'
        });

        $("#<%=txtEndDate.ClientID%>").datepicker({
            dateFormat: '<%= ConvertDateTimeFormat %>'
         });
    });

</script>
<div id="borderdiv" runat="server">
    <table width="400">
        <tr>
            <td>
                <table width="95%">
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblEntityDateRangeStateId" Text="EntityDateRangeStateId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntityDateRangeStateId" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:PlaceHolder ID="dynEntityDateRangeStateId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">StartDate:
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>                            
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynStartDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">EndDate:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>                          
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynEndDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">SystemEntityId:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSystemEntityId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSystemEntityId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">
                            <asp:Label ID="lblKeyId" Text="KeyId:" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtKeyId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynKeyId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">EntityDateRangeStateType:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpEntityDateRangeStateTypeList" runat="server" OnSelectedIndexChanged="drpEntityDateRangeStateTypeList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntityDateRangeStateTypeId" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynEntityDateRangeStateTypeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">Notes:
                        </td>
                        <td>
                            <asp:TextBox ID="txtNotes" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynNotes" runat="server" />
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

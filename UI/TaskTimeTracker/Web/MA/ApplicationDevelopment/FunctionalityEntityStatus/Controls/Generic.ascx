<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<br />
<br />

<html xmlns="http://www.w3.org/1999/xhtml">
<%--<head id="Head1" runat="server">
    <title></title>
</head>--%>

<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtTargetDate.ClientID%>").datepicker({
                dateFormat: '<%= ConvertDateTimeFormat %>'
            });

            $("#<%=txtStartDate.ClientID%>").datepicker({
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
                                <asp:Label ID="lblFunctionalityEntityStatusId" Text="FunctionalityEntityStatusId:"
                                    runat="server"></asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunctionalityEntityStatusId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFunctionalityEntityStatusId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">SystemEntityType:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpSystemEntityTypeList" runat="server" OnSelectedIndexChanged="drpSystemEntityTypeList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSystemEntityTypeId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynSystemEntityTypeId" runat="server" />
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
                        <td class="ralabel">FunctionalityStatus:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFunctionalityStatusList" runat="server" OnSelectedIndexChanged="drpFunctionalityStatusList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunctionalityStatusId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFunctionalityStatusId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">FunctionalityPriority:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFunctionalityPriorityList" runat="server" OnSelectedIndexChanged="drpFunctionalityPriorityList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunctionalityPriorityId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynFunctionalityPriorityId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ralabel">AssignedTo:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAssignedTo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:PlaceHolder ID="dynAssignedTo" runat="server" />
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
                    <tr valign="top">
                        <td class="ralabel">TargetDate:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTargetDate" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:Label ID="lblUserDateTimeFormat" runat="server" Text=""></asp:Label>
                            <asp:PlaceHolder ID="dynTargetDate" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="ralabel">StartDate:
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>                            
                        </td>
                        <td></td>
                        <td>
                            <asp:Label ID="lblUserDateTimeFormat2" runat="server" Text=""></asp:Label>
                            <asp:PlaceHolder ID="dynStartDate" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
                <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
                <table>
                    <tr>
                        <td colspan="4">
                            <asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="true">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblHistory" runat="server" Text=""><b>Record History</b></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dc:List ID="oHistoryList" runat="server" />
                                            <br />
                                            <asp:PlaceHolder ID="plcControlHolder" runat="server"></asp:PlaceHolder>
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

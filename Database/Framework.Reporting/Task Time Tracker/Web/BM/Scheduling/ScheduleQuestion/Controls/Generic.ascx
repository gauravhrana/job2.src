<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.ScheduleQuestion.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>
<div id="borderdiv" runat="server">
    <table  width="400" >
        <tr>
            <td>
                <table width="95%"  >
                    <tr>
                       <td class="ralabel">
                <asp:Label ID="lblScheduleQuestionId" Text="ScheduleQuestionId:" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtScheduleQuestionId" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                <asp:PlaceHolder ID="dynScheduleQuestionId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                ScheduleId:
            </td>
            <td>
                <asp:DropDownList ID="drpScheduleList" runat="server" OnSelectedIndexChanged="drpScheduleList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtScheduleId" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynScheduleId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                QuestionId:
            </td>
            <td>
                <asp:DropDownList ID="drpQuestionList" runat="server" OnSelectedIndexChanged="drpQuestionList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtQuestionId" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynQuestionId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ralabel">
                Answer:
            </td>
            <td>
                <asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                <asp:PlaceHolder ID="dynAnswer" runat="server" />
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

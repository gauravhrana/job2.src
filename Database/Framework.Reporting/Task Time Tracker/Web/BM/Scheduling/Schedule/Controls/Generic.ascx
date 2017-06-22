<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.Schedule.Controls.Generic" %>
<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>

<link href="/Prototype/Kendo/content/shared/styles/examples-offline.css" rel="stylesheet">
<link href="/styles/kendo/kendo.common.min.css" rel="stylesheet">
<link href="/styles/kendo/kendo.rtl.min.css" rel="stylesheet">
<link href="/styles/kendo/kendo.default.min.css" rel="stylesheet">

<script src="/scripts/kendo/full/kendo.web.min.js"></script>
<script src="/Prototype/Kendo/content/shared/js/console.js"></script>
<script>
    $(document).ready(function () {
        // create TimePicker from input HTML element 
        $("#<%=txtStartTime.ClientID%>").kendoTimePicker({ interval: "15" });
        $("#<%=txtEndTime.ClientID%>").kendoTimePicker({ interval: "15" });
        $("#<%=txtNextWorkTime.ClientID%>").kendoTimePicker({ interval: "15" });

        $("#<%=txtWorkDate.ClientID%>").datepicker({
            numberOfMonths: 2, showCurrentAtPos: 1,
            dateFormat: '<%= ConvertDateTimeFormat %>'
        });

        $("#<%=txtNextWorkDate.ClientID%>").datepicker({
            numberOfMonths: 2, showCurrentAtPos: 1,
            dateFormat: '<%= ConvertDateTimeFormat %>'
        });
    });

</script>
<div id="borderdiv" runat="server">


    <table class="table table-striped">
        <tr>
            <td>
                <asp:Label ID="lblScheduleId" Text="ScheduleId:" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txtScheduleId" runat="server"></asp:TextBox>
                <%--</td>--%>
            <td></td>
            <td>
                <asp:PlaceHolder ID="dynScheduleId" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>Person:
            </td>
            <td>
                <asp:DropDownList ID="drpPersonList" runat="server" OnSelectedIndexChanged="drpPersonList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtPersonId" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynPersonId" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>Schedule State:
            </td>
            <td>
                <asp:DropDownList ID="drpScheduleStateList" runat="server" OnSelectedIndexChanged="drpScheduleStateList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtScheduleState" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynScheduleStateId" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr valign="top">
            <td>Work Date:
            </td>

            <td>
                <asp:TextBox ID="txtWorkDate" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblWorkDateFormat" runat="server" Text="Label"></asp:Label></td>
            <td>
                <asp:PlaceHolder ID="dynWorkDate" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>Start Time:
            </td>
            <td>
                <asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:PlaceHolder ID="dynStartTime" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>End Time:
            </td>
            <td>
                <asp:TextBox ID="txtEndTime" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:PlaceHolder ID="dynEndTime" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>Total Hours Worked:
            </td>
            <td>
                <asp:TextBox ID="txtTotalHoursWorked" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:PlaceHolder ID="dynTotalHoursWorked" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr valign="top">
            <td>Next Work Date:
            </td>

            <td>
                <asp:TextBox ID="txtNextWorkDate" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblNextWorkDateFormat" runat="server" Text="Label"></asp:Label></td>

            <td>
                <asp:PlaceHolder ID="dynNextWorkDate" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>Next Work Time:
            </td>
            <td>
                <asp:TextBox ID="txtNextWorkTime" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:PlaceHolder ID="dynNextWorkTime" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>Planned Hours:
            </td>
            <td>
                <asp:TextBox ID="txtPlannedHours" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:PlaceHolder ID="dynPlannedHours" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr style="display: none;">
            <td style="display: none;">CreatedDate:
            </td>
            <td>
                <asp:TextBox ID="txtCreatedDate" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynCreatedDate" runat="server" />
            </td>
        </tr>
        <tr style="display: none;">
            <td>ModifiedDate:
            </td>
            <td>
                <asp:TextBox ID="txtModifiedDate" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynModifiedDate" runat="server" />
            </td>
        </tr>
        <tr style="display: none;">
            <td>CreatedByAudit Id:
            </td>
            <td>
                <asp:TextBox ID="txtCreatedByAuditId" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynCreatedByAuditId" runat="server" />
            </td>
        </tr>
        <tr style="display: none;">
            <td>ModifiedByAuditId:
            </td>
            <td>
                <asp:TextBox ID="txtModifiedByAuditId" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynModifiedByAuditId" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <ui:UpdateInfo ID="oUpdateInfo" runat="server" />
            </td>
        </tr>
    </table>

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

</div>


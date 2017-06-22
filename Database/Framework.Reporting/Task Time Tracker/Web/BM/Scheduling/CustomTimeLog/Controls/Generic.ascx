<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.CustomTimeLog.Controls.Generic" %>

<%@ Register TagPrefix="dc" TagName="List" Src="~/Shared/Controls/HistoryList.ascx" %>
<%@ Register Src="~/Shared/Controls/UpdateInfo.ascx" TagName="UpdateInfo" TagPrefix="ui" %>

<link href="/Prototype/Kendo/content/shared/styles/examples-offline.css" rel="stylesheet">
<link href="/styles/kendo/kendo.common.min.css" rel="stylesheet">
<link href="/styles/kendo/kendo.rtl.min.css" rel="stylesheet">
<link href="/styles/kendo/kendo.default.min.css" rel="stylesheet">

<script src="/scripts/kendo/full/kendo.web.min.js"></script>
<script src="/Prototype/Kendo/content/shared/js/console.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtWorkDate.ClientID%>").datepicker({
            dateFormat: '<%= ConvertDateTimeFormat %>'
        });
    });
</script>
<div id="borderdiv" runat="server">


    <table class="table table-striped">
        <tr>
            <td>
                <asp:Label ID="lblCustomTimeLogId" Text="CustomTimeLogId:" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txtCustomTimeLogId" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:PlaceHolder ID="dynCustomTimeLogId" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>Application:
            </td>
            <td>
                <asp:DropDownList ID="drpApplicationList" runat="server" OnSelectedIndexChanged="drpApplicationList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtApplicationId" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td>
                <asp:PlaceHolder ID="dynApplicationId" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCustomTimeLogKey" Text="CustomTimeLog Key:" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txtCustomTimeLogKey" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:PlaceHolder ID="dynCustomTimeLogKey" runat="server"></asp:PlaceHolder>
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
            <td>
                <asp:Label ID="lblCustomTimeCategory" Text="Custom Time Category:" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="txtCustomTimeCategory" runat="server" Enabled="false" Text="Promoted Code"></asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:PlaceHolder ID="dynCustomTimeCategory" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr valign="top">
            <td>Promoted Date:
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
            <td>No of Files Promoted:
            </td>
            <td>
                <asp:TextBox ID="txtNoofFilesPromoted" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:PlaceHolder ID="dynNoofFilesPromoted" runat="server"></asp:PlaceHolder>
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


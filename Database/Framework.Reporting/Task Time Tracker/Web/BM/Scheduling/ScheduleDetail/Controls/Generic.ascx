<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Generic.ascx.cs" Inherits="ApplicationContainer.UI.Web.Scheduling.ScheduleDetail.Controls.Generic" %>
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
        $("#<%=txtInTime.ClientID%>").kendoTimePicker({ interval: "15" });
        $("#<%=txtOutTime.ClientID%>").kendoTimePicker({ interval: "15" });


        $("#chkSendEmail").click(function () {
            if (this.checked) {
                $("[id*=lblEmailAddress]").show();
                $("[id*=txtEmailAddress]").show();
                $("[id*=lblCCAddress]").show();
                $("[id*=txtCCAddress]").show();
            }
            else {
                $("[id*=lblEmailAddress]").hide();
                $("[id*=txtEmailAddress]").hide();
                $("[id*=lblCCAddress]").hide();
                $("[id*=txtCCAddress]").hide();
            }
        });
    });
</script>
<div class="form-horizontal">

    <div class="form-group">
        <asp:Label ID="lblScheduleDetailId" Text="ScheduleDetail Id:" runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="txtScheduleDetailId"></asp:Label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtScheduleDetailId" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynScheduleDetailId" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <%--<div class="col-sm-2 control-label">--%>
            <label id="lblScheduleId" class="col-sm-2 control-label" >Schedule :</label>
        <%--</div>--%>
        <div class="col-sm-8">

            <asp:DropDownList ID="drpScheduleList" runat="server" OnSelectedIndexChanged="drpScheduleList_SelectedIndexChanged">
            </asp:DropDownList>

            <asp:TextBox ID="txtScheduleId" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynScheduleId" runat="server"></asp:PlaceHolder>
        </div>
    </div>

    <div class="form-group">
         <%--<div class="col-sm-2 control-label">--%>
            <label id="Label1" class="col-sm-2 control-label" >Schedule Detail Activity Category :</label>
        <%--</div>--%>
        <div class="col-sm-8">

            <asp:DropDownList ID="drpScheduleDetailActivityCategoryList" runat="server" OnSelectedIndexChanged="drpScheduleDetailActivityCategoryList_SelectedIndexChanged">
            </asp:DropDownList>

            <asp:TextBox ID="txtScheduleDetailActivityCategoryId" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynScheduleDetailActivityCategoryId" runat="server"></asp:PlaceHolder>
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="<%=txtInTime.ClientID%>">In Time:</label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtInTime" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynInTime" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="<%=txtOutTime.ClientID%>">Out Time:</label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtOutTime" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynOutTime" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="<%=txtWorkTicket.ClientID%>">Work Ticket:</label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtWorkTicket" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynWorkTicket" runat="server" />
        </div>
    </div>

    <div class="form-group">
        <label class="col-sm-2 control-label" for="<%=txtMessage.ClientID%>">Message:</label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control"></asp:TextBox>
            <span class="help-block">.</span>
        </div>
        <div class="col-sm-2">
            <asp:PlaceHolder ID="dynMessage" runat="server" />
        </div>
    </div>
    <%--<tr>
        <td colspan="4" align="Left">
            <asp:CheckBox ID="chkSendEmail" runat="server" Text="&nbsp;Send Email" TextAlign="Right" ClientIDMode="Static" />
        </td>
    </tr>--%>
    <tr>
        <td colspan="4" align="Left">
            <asp:Label ID="lblEmailAddress" runat="server" Text="Enter Email Address : " Style="display: none;" />
            <asp:TextBox ID="txtEmailAddress" runat="server" Style="display: none;" /><br />
            <asp:Label ID="lblCCAddress" runat="server" Text="Enter CC Email Address : " Style="display: none;" />
            <asp:TextBox ID="txtCCAddress" runat="server" Style="display: none;" /></td>
    </tr>

    <ui:UpdateInfo ID="oUpdateInfo" runat="server" />

</div>

<asp:PlaceHolder ID="dynAuditHistory" runat="server" Visible="false">
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label ID="lblHistory" runat="server" Text="" CssClass="col-sm-2 control-label" />
            <div class="col-sm-10">.</div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label">Record History:</label>
            <div class="col-sm-10 control-label">
                <dc:List ID="oHistoryList" runat="server" />
            </div>
        </div>
    </div>
</asp:PlaceHolder>


<div id="borderdiv" runat="server">
    <table>
    </table>
</div>

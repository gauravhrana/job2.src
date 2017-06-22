<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateRange.ascx.cs" Inherits="Shared.UI.Web.Controls.DateRangeControl" %>
<div id="VerticalStyle" runat="server">
    <asp:Table ID="DateRangeContainer" runat="server" CssClass="datePanel">
        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top" CssClass="ralabel" ID="labelcell1" runat="server">Date:</asp:TableCell>
            <asp:TableCell>
                <span align="left">

                    <div style="padding: 0 1px">
                        <asp:CheckBox ID="chkDate" Checked="true" runat="server" onclick="chkdate_checkedchanged();"
                            AutoPostBack="false" />
                        <asp:DropDownList runat="server" onchange="FillUp(); return false;" ID="drpDateRange" Width="100px"
                            AppendDataBoundItems="true">
                            <asp:ListItem Value="-1">All</asp:ListItem>
                            <asp:ListItem Value="0">Today</asp:ListItem>
                            <asp:ListItem Value="1">This Week</asp:ListItem>
                            <asp:ListItem Value="2">This Week-to-date</asp:ListItem>
                            <asp:ListItem Value="3">This Month</asp:ListItem>
                            <asp:ListItem Value="4">This Month-to-date</asp:ListItem>
                            <asp:ListItem Value="5">This Quarter</asp:ListItem>
                            <asp:ListItem Value="6">This Quarter-to-date</asp:ListItem>
                            <asp:ListItem Value="7">This Year</asp:ListItem>
                            <asp:ListItem Value="8">This Year-to-date</asp:ListItem>
                            <asp:ListItem Value="9">Yesterday</asp:ListItem>
                            <asp:ListItem Value="10">Last Week</asp:ListItem>
                            <asp:ListItem Value="11">Last Week-to-date</asp:ListItem>
                            <asp:ListItem Value="12">Last Month</asp:ListItem>
                            <asp:ListItem Value="13">Last Month-to-date</asp:ListItem>
                            <asp:ListItem Value="14">Last Quarter</asp:ListItem>
                            <asp:ListItem Value="15">Last Quarter-to-date</asp:ListItem>
                            <asp:ListItem Value="16">Last Year</asp:ListItem>
                            <asp:ListItem Value="18">Last Year-to-date</asp:ListItem>
                            <asp:ListItem Value="19">Next Week</asp:ListItem>
                            <asp:ListItem Value="20">Next 4 Weeks</asp:ListItem>
                            <asp:ListItem Value="21">Next Month</asp:ListItem>
                            <asp:ListItem Value="22">Next Quarter</asp:ListItem>
                            <asp:ListItem Value="23">Next Year</asp:ListItem>
                            <asp:ListItem Value="24" Selected="True">Custom</asp:ListItem>
                        </asp:DropDownList>
                        <span stytle=" width:3px;"></span>
                        <asp:PlaceHolder ID="spacecreator" runat="server"></asp:PlaceHolder>
                        <asp:TextBox runat="server" ID="txtSearchVerticalFromDate" AutoPostBack="false" Columns="10" OnTextChanged="txtSearchVerticalFromDate_TextChanged" name="FromDate" Style="padding-left: 5px;"></asp:TextBox>
                        <%--  <asp:ImageButton ID="btnDate1" runat="server" CausesValidation="False" ImageUrl="~/Content/Images/calendar_icon1.png"
                            Width="24px" Height="26px" ImageAlign="AbsBottom" Style="padding-left: 5px; padding-top:4px;" />--%>
                        - 
                        <asp:TextBox runat="server" ID="txtSearchVerticalToDate" AutoPostBack="false" OnTextChanged="txtSearchVerticalToDate_TextChanged" Columns="10" name="ToDate" />
                        <%--<asp:ImageButton ID="btnDate2" runat="server" CausesValidation="False" ImageUrl="~/Content/Images/calendar_icon1.png"
                            Width="24px" Height="26px" ImageAlign="AbsBottom" Style="padding-left: 5px; padding-top:4px;" />--%>
                        <asp:Label runat="server" CssClass="ralabel" ID="lblUserDateFormat" Font-Size="Small" Columns="6" Style="padding-left: 5px; font-size: small;" />
                    </div>

                </span>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

</div>

<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtSearchVerticalToDate.ClientID%>").datepicker({
            dateFormat: '<%= ConvertDateTimeFormat %>'
        });

        $("#<%=txtSearchVerticalFromDate.ClientID%>").datepicker({
            dateFormat: '<%= ConvertDateTimeFormat %>'
        });
    });
    //function pageLoad(sender, args)
    //{
    //  $(document).ready(function(){   

    // put all your javascript functions here 
    function FillUp() {
        var funcname = "Fillup" + '<%=GetKey()%>';
        var param1 = '<%= GetDateRangeControlClientId() %>';
        var param2 = '<%= GetFromDateTextBoxClientId() %>';
        var param3 = '<%= GetToDateTextBoxClientId() %>';
        var funcCall = funcname + "();";
        var ret = eval(funcCall);
    }

    function chkdate_checkedchanged() {
        var funcname = "chkdate_checkedchanged" + '<%=GetKey()%>';
        var funcCall = funcname + "();";
        var ret = eval(funcCall);
    }

    function chkdate_checkedchanged<%=GetKey()%>() {
        var chkbox = document.getElementById('<%= GetCheckBoxClientId() %>');
       var txtbox1 = document.getElementById('<%= GetFromDateTextBoxClientId() %>');
       var txtbox2 = document.getElementById('<%= GetToDateTextBoxClientId() %>');

       if (chkbox.checked) {
           txtbox1.disabled = false;
           txtbox2.disabled = false;
       }
       else {
           txtbox1.disabled = true;
           txtbox2.disabled = true;
       }
   }

   function Fillup<%=GetKey()%>() {
        var daterange = document.getElementById("<%= GetDateRangeControlClientId() %>").options[document.getElementById("<%= GetDateRangeControlClientId() %>").selectedIndex].text
        ApplicationContainer.UI.Web.AutoComplete.FillUpDate(daterange, onSucess, onError);

        function onSucess(Dates, txtfromdate, txttodate) {
            document.getElementById('<%= GetFromDateTextBoxClientId() %>').value = Dates[0];
            document.getElementById('<%= GetToDateTextBoxClientId() %>').value = Dates[1];
        }

        function onError(Dates) {
            alert('error');
        }

    }

    function ifFnExistsCallIt(fnName) {
        fn = window[fnName];
        fnExists = typeof fn === 'function';
        if (fnExists)
            return true;
        else
            return false;
    }

</script>

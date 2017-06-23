<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateRange.ascx.cs" Inherits="Shared.UI.Web.Controls.DateRangeControl" %>

<div class="row no-gutter">
    <div class="col-sm-3">
        <div class="row no-gutter">
            <div class="col-sm-1">
                <asp:CheckBox ID="chkDate" CssClass="checkbox" Checked="true" runat="server" AutoPostBack="false" />
            </div>
            <div class="col-sm-11">
                <asp:DropDownList runat="server"   ID="drpDateRange" CssClass="form-control" AppendDataBoundItems="true">
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
            </div>
        </div>
    </div>

    <asp:PlaceHolder ID="spacecreator" runat="server"></asp:PlaceHolder>

    <div class="col-sm-2">        
        <asp:TextBox runat="server" ID="txtSearchVerticalFromDate" AutoPostBack="false" class="form-control" OnTextChanged="txtSearchVerticalFromDate_TextChanged" name="FromDate"></asp:TextBox>
    </div>
    <div class="col-sm-2">
        <asp:TextBox runat="server" ID="txtSearchVerticalToDate" AutoPostBack="false" class="form-control" OnTextChanged="txtSearchVerticalToDate_TextChanged" name="ToDate" />
    </div>
    <div class="col-sm-2">
        <asp:Label runat="server" ID="lblUserDateFormat" />
    </div>
    <div class="col-sm-3">
        <asp:Label ID="labelcell1" Text="Date:" CssClass="control-label" runat="server"></asp:Label>
    </div>
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
        //UI.Web.AutoComplete.FillUpDate(daterange, onSucess, onError);        
        $.ajax({
            type: "POST",
            url: "/API/AutoComplete.asmx/FillUpDate",
            data: "{'daterange': '" + daterange + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnFillupSuccess<%=GetKey()%>,
            error: OnFillupError
        });

    }

    function OnFillupSuccess<%=GetKey()%>(data, status) {
        
        document.getElementById('<%= GetFromDateTextBoxClientId() %>').value = data.d[0];
        document.getElementById('<%= GetToDateTextBoxClientId() %>').value = data.d[1];
    }

    function OnFillupError(request, status, error) {
        alert(request);
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

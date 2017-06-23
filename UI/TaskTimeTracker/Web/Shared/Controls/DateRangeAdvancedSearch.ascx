 <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateRangeAdvancedSearch.ascx.cs"  Inherits="Shared.UI.Web.Controls.DateRangeAdvancedSearch" %>
<div class="row">
    <div class="col-sm-3">
        <div class="row">
            <div class="col-sm-2">
                <asp:CheckBox ID="chkDate" CssClass="checkbox" Checked="true" runat="server" onclick="chkdate_checkedchanged();"
                    AutoPostBack="false" />
            </div>
            <div class="col-sm-5">
            <asp:DropDownList runat="server" onchange="FillUpGroup(); return false;" ID="drpDateRangeGroup" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="false"> </asp:DropDownList>
                 </div>
            <div class="col-sm-5">
                <asp:DropDownList runat="server" ViewStateMode="Enabled" onchange="FillUp(); return false;" ID="drpDateRange" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="false"> </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="col-sm-2">
        <asp:PlaceHolder ID="spacecreator" runat="server"></asp:PlaceHolder>
        <asp:TextBox runat="server" ID="txtSearchVerticalFromDate" AutoPostBack="false" class="form-control"  name="FromDate"></asp:TextBox>
    </div>
    <div class="col-sm-2">
        <asp:TextBox runat="server" ID="txtSearchVerticalToDate" AutoPostBack="false" class="form-control" name="ToDate" />
    </div>
    <div class="col-sm-2"> 
        <asp:Label runat="server" ID="lblUserDateFormat" />
    </div>
    <div class="col-sm-3">
        <asp:Label ID="labelcell1" Text="Date:" CssClass="control-label" runat="server"></asp:Label>
    </div>
</div>

<script type="text/javascript">

    var ddlDateRange;
    var group
    var subgroup
    var isPostBack = <%=Convert.ToString(Page.IsPostBack).ToLower()%>;

  function FillUpGroup<%=GetKey()%>() {
      group = document.getElementById("<%= GetDateRangeGroupControlClientId() %>").options[document.getElementById("<%= GetDateRangeGroupControlClientId() %>").selectedIndex].text
      if(group!="Custom") 
      {
          subgroup = document.getElementById("<%= GetDateRangeControlClientId() %>").options[document.getElementById("<%= GetDateRangeControlClientId() %>").selectedIndex].text
      }
      ddlDateRange = document.getElementById("<%= GetDateRangeControlClientId() %>");
      

        $.ajax({
            type: "POST",
            url: "/API/AutoComplete.asmx/FillUpDateGroup",
            data: "{'group': '" + group + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnFillUpGroupSuccess,
            error: OnFillUpGroupError
        });

    }

    function OnFillUpGroupSuccess(data, status) {
        if(!isPostBack)
        {              
            ddlDateRange.options.length = 0;        
            for (var i = 0; i < data.d.length; i++) {
                AddOption(data.d[i].Name.replace(group, ''), data.d[i].value);
            }               
        }
        Fillup<%=GetKey()%>();

    }
    function AddOption(text, value) {
        var option = document.createElement('option');
        option.value = value;
        option.innerHTML = text;
        ddlDateRange.options.add(option);
    }

    function OnFillUpGroupError(data, status, error) {
        alert(data);
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
        group = document.getElementById("<%= GetDateRangeGroupControlClientId() %>").options[document.getElementById("<%= GetDateRangeGroupControlClientId() %>").selectedIndex].text
       
        var daterange = document.getElementById("<%= GetDateRangeControlClientId() %>").options[document.getElementById("<%= GetDateRangeControlClientId() %>").selectedIndex].text
        
            if (group == null) group = '';
            $.ajax({
                type: "POST",
                url: "/API/AutoComplete.asmx/FillUpDate",
                data: "{'daterange': '" + group +' '+ daterange + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnFillupSuccess,
                error: OnFillupError
            });
            document.getElementById("<%= GetDateRangeControlClientId() %>").options[document.getElementById("<%= GetDateRangeControlClientId() %>").selectedIndex].text = daterange;
        }
    
    function OnFillupSuccess(data, status) {
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

    $(document).ready(function () {
        $("#<%=txtSearchVerticalToDate.ClientID%>").datepicker({
             dateFormat: '<%= ConvertDateTimeFormat %>'
        });

         $("#<%=txtSearchVerticalFromDate.ClientID%>").datepicker({
             dateFormat: '<%= ConvertDateTimeFormat %>'
        });
     });

</script>
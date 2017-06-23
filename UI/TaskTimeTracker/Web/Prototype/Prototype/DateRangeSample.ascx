<%@ Control Language="C#"  AutoEventWireup="true" CodeBehind="DateRangeSample.ascx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.DateRangeSample" %>


 
<div id="VerticalStyle" runat="server" visible="false">
    <asp:Table ID="Table1" runat="server" >
        <asp:TableRow>
            <asp:TableCell CssClass="ralabel" ID="labelcell1" runat="server">
            
            <div style="vertical-align: top;">    
         Date:
        </div>
            </asp:TableCell>
            <asp:TableCell>
            <div align="left">
               <span style="padding: 0 1px">
                <asp:DropDownList  runat="server" onchange="Fillup(); return false;" ID="drpDateRange" AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Value="-1">All</asp:ListItem>
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
                    <asp:ListItem Value="24">Custom</asp:ListItem>
                </asp:DropDownList><asp:CheckBox ID="chkDate" Checked="true" runat="server"   OnCheckedChanged="chkDate_CheckedChanged"
                    AutoPostBack="true" /></span></div>
  <asp:TableCell>
  <asp:TableCell>
               <div style="height:8px;font-size:1px;">&nbsp;</div>     
                    <asp:TextBox  runat="server" ID="txtSearchVerticalFromDate" Columns="8"></asp:TextBox>
               
               <span style="padding: 0 1px"></span>   <asp:TextBox runat="server" ID="txtSearchVerticalToDate" Columns="8" />
                &nbsp;
                <asp:Label runat="server" CssClass="ralabel" ID="lblUserDateFormat" Columns="6" />
               
                &nbsp;
            </asp:TableCell>
            </asp:TableCell>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow >
            <asp:TableCell>
            </asp:TableCell>
           <asp:TableCell>
              
            </asp:TableCell></asp:TableRow>
    </asp:Table>    
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtSearchVerticalFromDate.ClientID%>").datepicker({
             dateFormat: '<%= ConvertDateTimeFormat %>'
        });
        $("#<%=txtSearchVerticalToDate.ClientID%>").datepicker({
            dateFormat: '<%= ConvertDateTimeFormat %>'
        });
     });
    function Fillup() {
           
        var daterange = document.getElementById("<%= drpDateRange.ClientID %>").options[document.getElementById("<%= drpDateRange.ClientID %>").selectedIndex].text
        ApplicationContainer.UI.Web.AutoComplete.FillUpDate(daterange,onSucess, onError);
       
        function onSucess(Dates) {
           document.getElementById('<%=txtSearchVerticalFromDate.ClientID %>').value = Dates[0];
           document.getElementById('<%=txtSearchVerticalToDate.ClientID %>').value = Dates[1];
         
        }
        function onError(Dates) {
            alert('error');
        }
      
    }
</script>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KendoSample1.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.KendoSample1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        
        <!-- jQuery JavaScript -->
        <script src="/scripts/jquery-2.1.1.min.js"></script>

        <!-- Common Kendo UI Web CSS -->
        <link href="/styles/kendo/kendo.common.min.css" rel="stylesheet" />
        <!-- Default Kendo UI Web theme CSS -->
        <link href="/styles/kendo/kendo.default.min.css" rel="stylesheet" />
        
        <!-- Kendo UI Web combined JavaScript -->
        <script src="/scripts/kendo/full/kendo.web.min.js"></script>
        
        <!-- Kendo UI DataViz CSS -->
        <link href="/styles/kendo/kendo.dataviz.min.css" rel="stylesheet" />        
        <!-- Kendo UI DataViz combined JavaScript -->
        <script src="/scripts/kendo/full/kendo.dataviz.min.js"></script>

    <div>
    
        <!-- HTML element from which the Kendo DatePicker would be initialized -->
        <input id="datepicker" />
        <script>
            $(function () {
                // Initialize the Kendo DatePicker by calling the kendoDatePicker jQuery plugin
                $("#datepicker").kendoDatePicker();
            });
        </script>
        
        <div id="gauge"></div>
        <script>
            $(function () {
                $("#gauge").kendoRadialGauge();
            });
        </script>
    </div>

    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KendoUI.aspx.cs" Inherits="ApplicationContainer.UI.Web.KendoUI" %>

<!DOCTYPE html>

<html >
<head runat="server">
    <title></title>
    <link href="Content/kendo/2013.3.1119/kendo.common.min.css" rel="stylesheet" />
    <link href="Content/kendo/2013.3.1119/kendo.default.min.css" rel="stylesheet" />
</head>
<body>
         <div id="employeesDiv"></div>
    <%--<script src="Scripts/jquery-2.0.3.js"></script>
    <script src="Scripts/jquery-2.0.3.min.js"></script>--%>
    <script src="Scripts/Kendo/Full/jquery.min.js"></script>
    <%--<script src="Scripts/Kendo/2013.3.1119/kendo.web.min.js"></script>--%>
    <script src="Scripts/Kendo/Full/kendo.all.min.js"></script>
      <div id="example" class="k-content">
                <textarea id="editor" rows="10" cols="30" style="height:440px">
                     Kendo UI Editor allows your users to edit HTML in a familiar, user-friendly way.<br />
                        In this version, the Editor provides the core HTML editing engine, which includes basic text formatting, hyperlinks, lists,
                        and image handling. <br />The widget outputs identical HTML across all major browsers, follows
                        accessibility standards and provides API for content manipulation.
                    
                </textarea>
            <script>
                $(document).ready(function () {
                    // create Editor from textarea HTML element with default set of tools
                    $("#editor").kendoEditor();
                });
            </script>
          </div>
</body>

</html>

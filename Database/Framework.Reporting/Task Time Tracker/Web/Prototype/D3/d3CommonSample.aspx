<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="d3CommonSample.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.D3.d3CommonSample" %>

<%@ Register TagPrefix="gnrc" TagName="d3SampleControl" Src="~/Prototype/D3/Controls/d3SampleControl.ascx" %>
<%@ Register TagPrefix="gnrc" TagName="d3PieSampleControl" Src="~/Prototype/D3/Controls/d3PieSampleControl.ascx" %>
<script src="/scripts/d3.v3.min.js" type="text/javascript"></script>
<script src="/scripts/jquery-2.1.1.min.js" type="text/javascript"></script>
<script src="/scripts/jquery-ui-1.10.4.min.js" type="text/javascript"></script>
<script src="/scripts/d3.v3.min.js" type="text/javascript"></script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <table>
        <tr>

            <td>
                <gnrc:d3SampleControl ID="mySampleControl" runat="server" />
            </td>

        </tr>
        <tr>
            <td>
                <gnrc:d3PieSampleControl ID="myPieSampleControl" runat="server" />

            </td>
        </tr>
    </table>

</body>
</html>

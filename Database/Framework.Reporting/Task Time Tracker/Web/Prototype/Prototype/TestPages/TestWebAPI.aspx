<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="TestWebAPI.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.TestPages.TestWebAPI" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script>
        var apiUrl = 'http://localhost:50881/api/clients';

        $(document).ready(function () {

            // Send an AJAX request
            $.getJSON(apiUrl)
            .done(function (data) {
                // On success, 'data' contains a list of client.
                $.each(data, function (key, item) {
                    // Add a list item for the product.

                    var rowHtml = document.createElement("tr");  // Create with DOM
                    rowHtml.innerHTML = formatItemRow(item);
                    $('#clientContainer').append(rowHtml();

                });
            })
            .fail(function (jqXHR, textStatus, err) {
                $('#clientSearch').text('Error: ' + err);
            });
        });

        function formatItem(item) {
            return item.ClientId + ' : ' + item.Name + ' :  ' + item.Description + ' : ' + item.SortOrder;
        }

        function formatItemRow(item) {
            var rowHtml = '<td>' + item.ClientId + '</td><td>' + item.Name + '</td><td>';
            rowHtml += item.Description + '</td><td>' + item.SortOrder + '</td>';
            rowHtml += "<td><a href='client/details/" + item.ClientId + "'>Details</a>";
            rowHtml += "<td><a href='client/Update/" + item.ClientId + "'>Edit</a>";
            return rowHtml;
        }

        function find() {
            var id = $('#clientId').val();
            $.getJSON(apiUrl + '/' + id)
            .done(function (data) {


                $('#clientContainer tr:not(:first)').remove();

                //$('#clientSearch').text(formatItem(data));
                var rowHtml = document.createElement("tr");  // Create with DOM
                rowHtml.innerHTML = formatItemRow(data);
                $('#clientContainer').append(rowHtml();

            })
            .fail(function (jqXHR, textStatus, err) {
                $('#clientSearch').text('Error: ' + err);
            });
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <h2>
            All Clients</h2>
        <ul id="clients" />
        <table id="clientContainer">
            <tr>
                <th>
                    ClientId
                </th>
                <th>
                    Name
                </th>
                <th>
                    Description
                </th>
                <th>
                    SortOrder
                </th>
                <th>
                </th>
                <th>
                </th>
                <th>
                </th>
            </tr>
        </table>
    </div>
    <div>
        <a href="client/Insert">Insert</a>
        <h2>
            Search by ID</h2>
        <input type="text" id="clientId" size="5" />
        <input type="button" value="Search" onclick="find();" />
        <p id="clientSearch" />
    </div>
</asp:Content>

<%@ Page Title="JQuery Sortable Records" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeBehind="JquerySorting.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.TestPages.JquerySorting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        body
        {
            font-family: 'Droid Serif';
        }

        .connected, .sortable, .exclude, .handles
        {
            margin: auto;
            padding: 0;
            width: 310px;
            -webkit-touch-callout: none;
            -webkit-user-select: none;
            -khtml-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

            .sortable.grid
            {
                overflow: hidden;
            }

            .connected li, .sortable li, .exclude li, .handles li
            {
                list-style: none;
                border: 1px solid #CCC;
                background: #F6F6F6;
                font-family: "Tahoma";
                color: #1C94C4;
                margin: 5px;
                padding: 5px;
                height: 22px;
            }

            .handles div
            {
                list-style: none;
                border: 1px solid #CCC;
                background: #F6F6F6;
                font-family: "Tahoma";
                color: #1C94C4;
                margin: 5px;
                padding: 5px;
            }

            .handles span
            {
                cursor: move;
            }

        .itemId
        {
            display: block;
        }
    </style>
    <link href='http://fonts.googleapis.com/css?family=Droid+Serif' rel='stylesheet' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <br />

    <div class="">
        <input id="btnRefresh" type="button" onclick="LoadRecords();" value="Refresh" />
        <input id="btnSave" type="button" onclick="UpdateSortOrders();" value="Save" />
    </div>

    <div id="divRecordsContainer" class="handles list">
    </div>

    <br />
    <script src="../../../../Scripts/jquery.sortable.js"></script>
    <script>

        $(document).ready(function () {
            LoadRecords();
        });        

        function createHTMLRecord(item)
        {            
            var innerLine = "<div>";

            innerLine += "<span>::</span> " + item.Name + "";
            innerLine += "<div class=\"itemId\"> " + item.ClientId + "</div>";
            innerLine += "<div>Description: " + item.Description + "</div>";
            innerLine += "<div>SortOrder: " + item.SortOrder + "</div>";

            innerLine += "</div>";

            return innerLine;
        }

        function LoadRecords()
        {
            var apiUrl = 'http://localhost:50881/api/clients/';

            $("#divRecordsContainer").html("");

            var parentItem = $("#divRecordsContainer");

            // Send an AJAX request
            $.getJSON(apiUrl)
            .done(function (data) { 

                // On success, 'data' contains a list of client.
                $.each(data, function (key, item) {

                    var innerDiv = createHTMLRecord(item);
                    parentItem.append(innerDiv);

                });

                $('.handles').sortable({
                    handle: 'span'
                });

            })
            .fail(function (jqXHR, textStatus, err) {
                parentItem.text('Error: ' + err);
            });

        }

        function UpdateSortOrders() {

            var xAudit = <%: Shared.WebCommon.UI.Web.SessionVariables.RequestProfile.AuditId %>;
            var apiUrl = 'http://localhost:50881/api/clients/';

            var xRecordsContainerId = '#divRecordsContainer';

            var sortOrder = 1;

            $(xRecordsContainerId + " .itemId").each(function () {
                
                var id = this.innerText;

                var name = "";
                var desc = "";
                var clientObj = { ClientId: id, Name: name, Description: desc, SortOrder: sortOrder };
                var json = JSON.stringify(clientObj);                

                $.ajax({
                    url: apiUrl + id,
                    type: 'put',
                    cache: false,
                    data: json,
                    contentType: 'application/json; charset=utf-8',
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }

                });

                sortOrder = sortOrder + 1;
                //alert(id);
            });

            alert("Sort Orders Updated");

            return false;
        }

    </script>

</asp:Content>

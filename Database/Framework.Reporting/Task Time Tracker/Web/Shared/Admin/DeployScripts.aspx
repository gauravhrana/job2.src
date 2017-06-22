<%@ Page Title="Deploy Database Scripts" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="DeployScripts.aspx.cs" Inherits="Shared.UI.Web.Admin.DeployScripts" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css" media="screen">
        .ace_editor {
            position: relative !important;
            border: 1px solid lightgray;
            margin: auto;
            height: 200px;
            width: 100%;
            margin-bottom: 10px;
        }
    </style>
    <script src="/Scripts/ace-editor/src/ace.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    Deploy Scripts
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="height: 40px;"></div>
    <div class="row">
        <div class="col-sm-3">No Of Line Preference: </div>
        <div class="col-sm-6">
            <input type="text" id="txtLinePreference" />
        </div>
        <div class="col-sm-3">
            <input id="btnUpdate" type="button" class="btn btn-default" value="Update" onclick="UpdateAceEditorNoOfLines()" />
        </div>
    </div>
    <div class="row" style="height: 4px;"></div>
    <div class="row">
        <div class="col-sm-3">System Entity: </div>
        <div class="col-sm-9">
            <asp:DropDownList ID="drpSystemEntityType" runat="server"></asp:DropDownList>
         (Only Applicable to Standard Entities)</div>
    </div>
    <div style="margin: 10px 0px;">
        <input id="btnGet" type="button" class="btn btn-default" value="Generate Prcoedures" onclick="GenerateProcedureText()" />
    </div>
    <div id="tabs" style="display: none;">
        <ul>
            <li><a href="#tabs-insert">Insert Procedure</a></li>
            <li><a href="#tabs-update">Update Procedure</a></li>
            <li><a href="#tabs-delete">Delete Procedure</a></li>
            <li><a href="#tabs-search">Search Procedure</a></li>
        </ul>
        <div id="tabs-insert" title="Insert">
            <pre id="editorInsertProcedure"></pre>
            <input id="btnDeployInsert" type="button" onclick="DeployProcedureText('Insert')"
                class="btn btn-default" value="Deploy" />
        </div>
        <div id="tabs-update" title="Update">
            <pre id="editorUpdateProcedure"></pre>
            <input id="btnDeployUpdate" type="button" onclick="DeployProcedureText('Update')"
                class="btn btn-default" value="Deploy" />
        </div>
        <div id="tabs-delete" title="Delete">
            <pre id="editorDeleteProcedure"></pre>
            <input id="btnDeployDelete" type="button" onclick="DeployProcedureText('Delete')"
                class="btn btn-default" value="Deploy" />
        </div>
        <div id="tabs-search" title="Search">
            <pre id="editorSearchProcedure"></pre>
            <input id="btnDeploySearch" type="button" onclick="DeployProcedureText('Search')"
                class="btn btn-default" value="Deploy" />
        </div>
    </div>
    <div style="margin: 10px 0px;">
        <input id="btnDeployAll" type="button" class="btn btn-default"
            value="Deploy All" style="display: none;" onclick="DeployProcedureText('All')" />
    </div>

    <script>var aceEditorNoOfLines = 0;

        function UpdateAceEditorNoOfLines() {
            var noOfLines = document.getElementById("txtLinePreference").value;

            aceEditorNoOfLines = noOfLines;

            $.ajax({
                type: "POST",
                url: "/Shared/Admin/DeployScripts.aspx/UpdateAceEditorNoOfLines",
                data: "{'aceEditorNoOfLines': '" + noOfLines + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                error: OnError
            });
        }

        function GetAceEditorNoOfLines() {

            $.ajax({
                type: "POST",
                url: "/Shared/Admin/DeployScripts.aspx/GetAceEditorNoOfLines",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessPreference,
                error: OnError
            });
        }

        function OnSuccess(data, status) {
        }

        function OnSuccessPreference(data, status) {
            //alert('Preference Successfully updated');
            aceEditorNoOfLines = data.d;
            document.getElementById("txtLinePreference").value = aceEditorNoOfLines;
        }

        function prepareAceEditor(editorId, editorText) {
            var editor = ace.edit(editorId);
            editor.setTheme("ace/theme/twilight");
            editor.session.setMode("ace/mode/sql");
            editor.setAutoScrollEditorIntoView(false);
            editor.setOption("maxLines", aceEditorNoOfLines);
            editor.setShowPrintMargin(false);       // disable vertical line
            editor.setReadOnly(true);               // read only true

            editor.getSession().setValue(editorText);
        }

        function OnPostSuccess(data, status) {
            alert("Deployment Successfull");
        }

        function OnError(request, status, error) {
            alert("error updating Grid Lines UP Value: " + error);
        }

        function DeployProcedureText(procedureType) {

            var entityName = document.getElementById("<%= drpSystemEntityType.ClientID %>").value;

            $.ajax({
                type: "POST",
                url: "/Shared/Admin/DeployScripts.aspx/DeployProcedureText",
                data: "{'entityName': '" + entityName + "', 'procedureType': '" + procedureType + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnPostSuccess,
                error: OnError
            });
        }

        function OnGetMemberSuccess(data, status) {

            $("#tabs").show();
            $("#btnDeployAll").show();
            $("#tabs").tabs({
                activate: function (event, ui) {

                    //get activated tab
                    var procType = ui.newPanel.attr("title");

                    //refreshes ace editor w.o needing to put the cursor
                    ace.edit("editor" + procType + "Procedure").resize();
                }
            });

            prepareAceEditor("editorInsertProcedure", data.d.InsertProcedure);
            prepareAceEditor("editorUpdateProcedure", data.d.UpdateProcedure);
            prepareAceEditor("editorDeleteProcedure", data.d.DeleteProcedure);
            prepareAceEditor("editorSearchProcedure", data.d.SearchProcedure);

        }

        function GenerateProcedureText() {

            var entityName = document.getElementById("<%= drpSystemEntityType.ClientID %>").value;

            $.ajax({
                type: "POST",
                url: "/Shared/Admin/DeployScripts.aspx/GetProcedureText",
                data: "{'entityName': '" + entityName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnGetMemberSuccess,
                error: OnError
            });

        }

        GetAceEditorNoOfLines();

    </script>
</asp:Content>

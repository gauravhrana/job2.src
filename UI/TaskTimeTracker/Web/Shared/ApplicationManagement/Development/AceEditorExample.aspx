<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.Master"
    AutoEventWireup="true" CodeBehind="AceEditorExample.aspx.cs"
    Inherits="Shared.UI.Web.Development.AceEditorExample" Title="Ace Editor Example" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css" media="screen">
        .ace_editor
        {
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
<asp:Content ID="Content2" ContentPlaceHolderID="ControlVisibilityManager" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SectionName" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SearchControlItem" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ListControlItem" runat="server">
    <div class="row" style="height: 40px;"></div>
    <div class="row">
        <div class="col-sm-3">No Of Line Preference: </div>
        <div class="col-sm-6">
            <input type="text" id="txtLinePreference" />
        </div>
        <div class="col-sm-3">
            <input id="btnUpdate" type="button" class="btn btn-default" value="Update" onclick="UpdateAceEditorNoOfLines()" />
        </div>
    </div><div class="row" style="height: 10px;"></div>
    <div class="row">
        <div class="col-sm-3">System Entity: </div>
        <div class="col-sm-6">
            <asp:DropDownList ID="drpSystemEntityType" runat="server"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <input id="btnGet" type="button" class="btn btn-default" value="Generate Prcoedures" onclick="GenerateProcedureText()" />
        </div>
    </div>
    <div style="margin: 10px 0px;">
    </div>
    <div id="tabs" style="display: none;">
        <ul>
            <li><a href="#tabs-1">Insert Procedure</a></li>
            <li><a href="#tabs-2">Update Procedure</a></li>
            <li><a href="#tabs-3">Delete Procedure</a></li>
            <li><a href="#tabs-4">Search Procedure</a></li>
        </ul>
        <div id="tabs-1">
            <pre id="editorInsertProcedure"></pre>
            <input id="btnDeployInsert" type="button" onclick="DeployProcedureText('Insert')"
                class="btn btn-default" value="Deploy" />
        </div>
        <div id="tabs-2">
            <pre id="editorUpdateProcedure"></pre>
            <input id="btnDeployUpdate" type="button" onclick="DeployProcedureText('Update')"
                class="btn btn-default" value="Deploy" />
        </div>
        <div id="tabs-3">
            <pre id="editorDeleteProcedure"></pre>
            <input id="btnDeployDelete" type="button" onclick="DeployProcedureText('Delete')"
                class="btn btn-default" value="Deploy" />
        </div>
        <div id="tabs-4">
            <pre id="editorSearchProcedure"></pre>
            <input id="btnDeploySearch" type="button" onclick="DeployProcedureText('Search')"
                class="btn btn-default" value="Deploy" />
        </div>
    </div>
    <div style="margin: 10px 0px;">
        <input id="btnDeployAll" type="button" class="btn btn-default"
            value="Deploy All" style="display: none;" onclick="DeployProcedureText('All')" />
    </div>

    <script>

        var aceEditorNoOfLines = 0;

        function UpdateAceEditorNoOfLines() {
            var noOfLines = document.getElementById("txtLinePreference").value;

            aceEditorNoOfLines = noOfLines;

            $.ajax({
                type: "POST",
                url: "/Shared/ApplicationManagement/Development/AceEditorExample.aspx/UpdateAceEditorNoOfLines",
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
                url: "/Shared/ApplicationManagement/Development/AceEditorExample.aspx/GetAceEditorNoOfLines",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessPreference,
                error: OnError
            });
        }

        function OnSuccessPreference(data, status) {
            alert('Preference Successfully updated');
            aceEditorNoOfLines = data.d;
            document.getElementById("txtLinePreference").value = aceEditorNoOfLines;
        }

        function prepareAceEditor(editorId, editorText) {
            var editor = ace.edit(editorId);
            editor.setTheme("ace/theme/twilight");
            editor.session.setMode("ace/mode/sql");
            editor.setAutoScrollEditorIntoView(false);

            editor.setOption("maxLines", aceEditorNoOfLines);

            editor.getSession().setValue(editorText);
        }

        function DeployProcedureText(procedureType) {

            var entityName = document.getElementById("<%= drpSystemEntityType.ClientID %>").value;

            $.ajax({
                type: "POST",
                url: "/Shared/ApplicationManagement/Development/AceEditorExample.aspx/DeployProcedureText",
                data: "{'entityName': '" + entityName + "', 'procedureType': '" + procedureType + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnPostSuccess,
                error: OnError
            });
        }

        function OnSuccess(data, status) {
        }

        function OnPostSuccess(data, status) {
            alert("Deployment Successfull");
        }

        function GenerateProcedureText() {

            var entityName = document.getElementById("<%= drpSystemEntityType.ClientID %>").value;

            $.ajax({
                type: "POST",
                url: "/Shared/ApplicationManagement/Development/AceEditorExample.aspx/GetProcedureText",
                data: "{'entityName': '" + entityName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnGetMemberSuccess,
                error: OnGetMemberError
            });

        }

        function OnGetMemberSuccess(data, status) {

            $("#tabs").show();
            $("#btnDeployAll").show();
            $("#tabs").tabs();

            prepareAceEditor("editorInsertProcedure", data.d.InsertProcedure);
            prepareAceEditor("editorUpdateProcedure", data.d.UpdateProcedure);
            prepareAceEditor("editorDeleteProcedure", data.d.DeleteProcedure);
            prepareAceEditor("editorSearchProcedure", data.d.SearchProcedure);

        }

        function OnError(request, status, error) {
            alert("error updating Grid Lines UP Value: " + error);
        }

        GetAceEditorNoOfLines();

    </script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ActionContent" runat="server">
</asp:Content>

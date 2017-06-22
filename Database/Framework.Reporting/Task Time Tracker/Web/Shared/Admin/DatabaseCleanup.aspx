<%@ Page Title="Database Cleanup" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
    AutoEventWireup="true" CodeBehind="DatabaseCleanup.aspx.cs"
    Inherits="Shared.UI.Web.Admin.DatabaseCleanup" %>
<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageSiteMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css" media="screen">
        .ace_editor
        {
            position: relative !important;
            border: 1px solid lightgray;
            margin: auto;
            width: 95%;
        }
    </style>
    <script src="/Scripts/ace-editor/src/ace.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    Database Cleanup
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-2">Database</div>
        <div class="col-md-4">
            <asp:DropDownList ID="drpDBName" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Text="Configuration">Configuration</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-6"></div>
    </div>
    <div class="row">
        <div class="col-sm-2">Object Type</div>
        <div class="col-md-4">
            <asp:DropDownList ID="drpObjectType" runat="server">
                <asp:ListItem Text="Procedures">P</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-6"></div>
    </div>
    <div class="row">
        <div class="col-sm-2">Name</div>
        <div class="col-md-4">
            <asp:TextBox ID="txtName" runat="server">DoesExist</asp:TextBox>
        </div>
        <div class="col-md-6"></div>
    </div>
    <div class="row">
        <div class="col-sm-2"></div>
        <div class="col-md-10">
            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
        </div>
    </div>
    <div class="row" style="height: 40px;"></div>

    <div class="row">
        <div class="col-md-4">

            <div class="row">
                <asp:GridView ID="grdResult" runat="server" DataKeyNames="Name"
                    AutoGenerateColumns="False" OnRowDataBound="grdResult_RowDataBound"
                    EmptyDataText="No Records to Display">
                    <Columns>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkHeader" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="row">
                <asp:Button ID="btnDropProcedures" runat="server" Text="Drop Selected Prcoedures" OnClick="btnDropProcedures_Click" />
            </div>
        </div>
        <div class="col-md-8">
            <div class="row">
                <input id="chkPreview" type="checkbox" checked="checked" />Preview Script           
            </div>
            <div class="row">
                <pre id="editor"></pre>
            </div>
        </div>
    </div>

    <script>
        function showProcedurePreview(chkBoxId, procName) {

            var isChecked = document.getElementById(chkBoxId).checked;
            var isPreview = document.getElementById("chkPreview").checked;

            if (isPreview && isChecked) {

                var dbName = document.getElementById("<%= drpDBName.ClientID %>").value;

                function OnGetMemberSuccess(data, status) {

                    var editor = ace.edit("editor");
                    editor.setTheme("ace/theme/twilight");
                    editor.session.setMode("ace/mode/sql");
                    editor.setAutoScrollEditorIntoView(true);
                    editor.setOption("maxLines", 100);

                    editor.getSession().setValue(data.d);
                }

                function OnGetMemberError(request, status, error) {
                    alert("error updating Grid Lines UP Value: " + error);
                }

                $.ajax({
                    type: "POST",
                    url: "/Shared/Admin/DatabaseCleanup.aspx/GetProcedureText",
                    data: "{'dbName': '" + dbName + "', 'procName': '" + procName + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnGetMemberSuccess,
                    error: OnGetMemberError
                });
            }
        }
    </script>
</asp:Content>

<%@ Page Title="Cleanup FC Entries" Language="C#" MasterPageFile="~/MasterPages/Site.Master"
    AutoEventWireup="true" CodeBehind="CleanupFCEntries.aspx.cs" Inherits="Shared.UI.Web.Admin.CleanupFCEntries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    <div class="container text-center ">
        <h3>Cleanup Field Configuration Entries</h3>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="panel-title">
                    <h4>Cleanup Criteria</h4>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-2">
                        <label for="exampleInputEmail1">Select: </label>
                    </div>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="drpCriteria" runat="server" CssClass="form-control">
                            <asp:ListItem Selected="True" Value="None">None</asp:ListItem>
                            <asp:ListItem Value="PK and SortOrder">Remove PK Id and Sort Order in Standard Mode</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-4"></div>
                </div>
                <div class="row">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-10">
                        <asp:Button ID="btnSearch" runat="server" Text="Search Records" OnClick="btnSearch_Click" CssClass="btn btn-default" />
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-body">
                <div class="row">
                    <asp:GridView CellPadding="5" CellSpacing="2" ID="gvResult" DataKeyNames="FieldConfigurationId" runat="server" EmptyDataText="No Records Found" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="checkAll(this);"></asp:CheckBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelectItem" runat="server" onclick="Check_Click(this)"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FieldConfigurationId" HeaderText="Id">
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name">
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FieldConfigurationDisplayName" HeaderText="Display Name">
                                <ItemStyle HorizontalAlign="Center" Width="250px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SystemEntityType" HeaderText="Entity">
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FieldConfigurationMode" HeaderText="FC Mode">
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="row">
                    <asp:Button ID="btnRemove" runat="server" Text="Remove Rows" OnClick="btnRemove_Click" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
</asp:Content>

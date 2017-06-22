<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KendoAutoComplete.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.Sample.KendoAutoComplete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>Auto Complete Sample using Kendo UI</title>
    <!-- jQuery JavaScript -->
    <script src="/scripts/jquery-2.1.1.min.js" type="text/javascript"></script>

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
   
</head>
<body>
    <form id="form1" runat="server">
        
    <div>
    
        <!-- HTML element from which the Kendo DatePicker would be initialized -->
         <div id="divAC" style="float:left">
             <asp:TextBox ID="autoComplete" runat="server"></asp:TextBox>
             <asp:TextBox ID="autoComplete1" runat="server"></asp:TextBox>
    <asp:HiddenField ID="hfCustomerId" runat="server" />
       <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick = "Submit" />
             <div id="result"></div>

        
        <script type="text/javascript">
            // wire up autocomplete for #autocomplete input box
            $(document).ready(function ()
            {
                function onSelect(e) {
                    
                        var dataItem = this.dataItem(e.item.index());
                        alert(dataItem.ReleaseLogId);
                        $("#<%=hfCustomerId.ClientID %>").val(dataItem.ReleaseLogId);
                };

                $.ajax(
                {
                    type: "POST",
                    url: "http://localhost:53331/API/AutoComplete.asmx/GetReleaseLogList",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg)
                    {
                       
                        $("#autoComplete").kendoComboBox(
                                {
                                    dataSource: msg.d,
                                    dataTextField: "Name",
                                    dataValueField: "ReleaseLogId",
                                    filter: "startswith",
                                    select: onSelect,
                                    placeholder : "Select Release Log ...",                                   
                                   
                                });
                    },                  
                    
                });
            });



        </script>  
             
            <%-- <script type="text/javascript">
                 $(document).ready(function () {
                     $("#<%=autoComplete1.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "http://localhost:53331/API/AutoComplete.asmx/GetModuleList",
                    data: "{}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("#<%=hfCustomerId.ClientID %>").val(i.item.val);
            },
            minLength: 1
        });
    });
</script>     --%>
        
    </div>

        
                <div id="treeview" style="float:left"></div>
           

            <script>
                $(document).ready(function () {
                    var treeview = $("#treeview").kendoTreeView({
                        dataSource: {
                            data: [
                                {
                                    text: "Release Notes", expanded: true, items: [
                                      { text: "Release Log Status" },
                                      { text: "Release Issue Type" },
                                      { text: "Release Log Detail" }
                                    ]
                                },
                                {
                                    text: "TCM", items: [
                                         { text: "Test Suite" },
                                      {
                                          text: "Test Case", expanded: true, items: [{ text: "Test Case Priority" },
                                          { text: "Test Case Status" },
                                         ]
                                      },
                                     
                                      { text: "Test Run" }
                                    ]
                                },
                                { text: "Event Notification" }
                            ]
                        },
                        loadOnDemand: false
                    }).data("kendoTreeView"),

                        handleTextBox = function (callback) {
                            return function (e) {
                                if (e.type != "keypress" || kendo.keys.ENTER == e.keyCode) {
                                    callback(e);
                                }
                            };
                        };


                    $("#disableNode").click(function () {
                        var selectedNode = treeview.select();

                        treeview.enable(selectedNode, false);
                    });

                    $("#enableAllNodes").click(function () {
                        var selectedNode = treeview.select();

                        treeview.enable(".k-item");
                    });

                    $("#removeNode").click(function () {
                        var selectedNode = treeview.select();

                        treeview.remove(selectedNode);
                    });

                    $("#expandAllNodes").click(function () {
                        treeview.expand(".k-item");
                    });

                    $("#collapseAllNodes").click(function () {
                        treeview.collapse(".k-item");
                    });

                    var append = handleTextBox(function (e) {
                        var selectedNode = treeview.select();

                        // passing a falsy value as the second append() parameter
                        // will append the new node to the root group
                        if (selectedNode.length == 0) {
                            selectedNode = null;
                        }

                        treeview.append({
                            text: $("#appendNodeText").val()
                        }, selectedNode);
                    });

                    $("#appendNodeToSelected").click(append);
                    $("#appendNodeText").keypress(append);

                    // datasource actions

                    var ascending = false;

                    $("#sortDataSource")
                        .text(ascending ? "Sort ascending" : "Sort descending")
                        .click(function () {
                            treeview.dataSource.sort({
                                field: "text",
                                dir: ascending ? "asc" : "desc"
                            });

                            ascending = !ascending;

                            $(this).text(ascending ? "Sort ascending" : "Sort descending")
                        });

                    var filter = handleTextBox(function (e) {
                        var filterText = $("#filterText").val();

                        if (filterText !== "") {
                            treeview.dataSource.filter({
                                field: "text",
                                operator: "contains",
                                value: filterText
                            });
                        } else {
                            treeview.dataSource.filter({});
                        }
                    });

                    $("#filterDataSource").click(filter);
                    $("#filterText").keypress(filter);
                });
            </script> 

    </form>
</body>
</html>

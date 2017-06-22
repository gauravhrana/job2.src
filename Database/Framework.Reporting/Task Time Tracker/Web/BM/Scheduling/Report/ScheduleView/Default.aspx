<%@ Page Language="C#" MasterPageFile="~/MasterPages/Schedule/Default.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="ApplicationContainer.UI.Web.BM.Scheduling.Report.ScheduleView.Default" %>

<%@ Register TagPrefix="vc" TagName="VCManager" Src="~/Shared/Controls/ControlVisibilityManager.ascx" %>
<%@ Register TagPrefix="sr" TagName="SearchFilter" Src="./Controls/ScheduleNewSearchControl.ascx" %>
<%@ Register TagPrefix="sc" TagName="StatChart" Src="./Controls/ScheduleStatChart.ascx" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="ContentSectionName" runat="server" ContentPlaceHolderID="SectionName">
</asp:Content>

<asp:Content ID="ContentControlItem" runat="server" ContentPlaceHolderID="SearchControlItem">
    <sr:SearchFilter ID="oSearchFilter" runat="server" />
</asp:Content>
<%--  --%>
<asp:Content ID="ContentListControlItem" runat="server" ContentPlaceHolderID="ListControlItem">


    <asp:Panel ID="dynGridContainer" runat="server" />
    <%--<dc:GroupList ID="oGroupList" runat="server" />--%>
    <div id="scheduleAppContainer" ng-app="scheduleApp" ng-controller="ScheduleCtrl">
        <div id="Div1">
            <asp:PlaceHolder ID="plcGroupByHolder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="plcTabHolder" runat="server"></asp:PlaceHolder>

            <div style="background: lightblue; height: 50px;">
                <div class="exportmenuContainer" style="background: lightblue; float: left;">
                    <asp:DropDownList ID="ddlFieldConfigurationMode" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlFieldConfigurationMode_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>

            <table border="1">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblSearchStatus" Text="" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                        <div id="maindiv" runat="server">

                            <asp:Repeater ID="GrdParentGrid" runat="server" OnItemDataBound="GrdParentGrid_RowDataBound">

                                <ItemTemplate>

                                    <div id="itemdiv" runat="server">

                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <b>
                                                        <a href="Details/<%# DataBinder.Eval(Container.DataItem,"PersonId")%>"><%# DataBinder.Eval(Container.DataItem,"Person")%></a>
                                                        <asp:HiddenField ID="hdnScheduleId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"PersonId")%>' />
                                                    </b>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnInsert" runat="server" Text="Insert" PostBackUrl="~/Schedule/Insert/" />

                                                </td>
                                                <td align="right">
                                                    <span>
                                                        <asp:LinkButton ID='chkbox' runat="server" AutoPostBack="true" Text='[X]' CommandName='<%# Eval("PersonId") %>' OnClick="chkbox_Click" ForeColor="GrayText" />
                                                    </span>
                                                </td>
                                            </tr>
                                        </table>
                                        <dt>
                                            <div id="detailsGridContainer" runat="server"></div>
                                        </dt>
                                        <%--<dt>
                                        <dc:List ID="oList" runat="server" />
                                        <div id="divStat" class="divReleaseNotes" runat="server">
                                            <asp:Label ID="lblTotal" Text="Total:" runat="server"></asp:Label>
                                            <asp:Label ID="lblCount" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                                            <asp:Label ID="lblAverage" Text="Average:" runat="server"></asp:Label>
                                            <asp:Label ID="lblAverageValue" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                                            <asp:Label ID="lblMedian" Text="Median:" runat="server"></asp:Label>
                                            <asp:Label ID="lblMedianValue" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                                            <asp:Label ID="lblRecordCountText" Text="Count:" runat="server"></asp:Label>
                                            <asp:Label ID="lblRecordCount" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                                            <asp:Label ID="lblMaxText" Text="Max:" runat="server"></asp:Label>
                                            <asp:Label ID="lblMax" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                                            <asp:Label ID="lblMinText" Text="Min:" runat="server"></asp:Label>
                                            <asp:Label ID="lblMin" Style="font-weight: bold;" runat="server" CssClass="rslabel"></asp:Label>
                                        </div>
                                    </dt>--%>
                                    </div>                                    
                                    <br />
                                </ItemTemplate>

                            </asp:Repeater>
                        </div>
                    </td>
                </tr>
            </table>

            <asp:PlaceHolder ID="TableReportContent" runat="server"></asp:PlaceHolder>

            <asp:PlaceHolder ID="dynChart" runat="server">
                <table>
                    <tr>
                        <td>
                            <sc:StatChart ID="oSC" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:PlaceHolder>

        </div>
        <asp:PlaceHolder ID="Stats" runat="server">
           
            <table  onfocus="styleSummaryLine();" id="angularStats"  class="table table-striped table-bordered table-condensed">
                <tr>
                    <th class="text-center">{{GroupBy}}
                    </th>
                    <th class="text-center">Average
                    </th>
                    <th class="text-center">Median
                    </th>
                    <th class="text-center">Max
                    </th>
                    <th class="text-center">Min
                    </th>
                    <th class="text-center">Count
                    </th>
                    <th class="text-center">Count(%)
                    </th>
                    <th class="text-center">Total
                    </th>
                    <th class="text-center">Total(%)
                    </th>


                </tr>
                <tr ng-repeat="statistic in statistics">
                    <td class="text-right">{{statistic.Name}}
                   
                    </td>
                    <td class="text-right">{{statistic.Average| number:2}}
                    </td>
                    <td class="text-right">{{statistic.Median| number:2}}
                    </td>
                    <td class="text-right">{{statistic.Max| number:2}}
                    </td>
                    <td class="text-right">{{statistic.Min| number:2}}
                    </td>
                    <td class="text-right">{{statistic.Count| number:2}}
                    </td>
                    <td class="text-right">{{statistic.CountPercentage| number:2}}%
                    </td>
                    <td class="text-right">{{statistic.Total| number:2}}
                    </td>
                    <td class="text-right">{{statistic.TotalPercentage| number:2}}%
                    </td>

                </tr>

            </table>

        </asp:PlaceHolder>

        <asp:PlaceHolder ID="Chart" runat="server">
            
            <highchart id="chart1" config="chart"></highchart>
        </asp:PlaceHolder>

        <script>

            //debugger
           
            var app = angular.module('scheduleApp', ['ngResource', 'ngRoute', 'ui.select2', 'highcharts-ng']);

            app.controller('ScheduleCtrl', ['$scope', '$injector', 'fieldConfigurationService', 'scheduleService',
                   function ($scope, $injector, fieldConfigurationService, scheduleService) {

                       //debugger

                       var entityName = "Schedule";
                       var settingCategory = entityName + "NewDefaultViewSearchControl";

                       $scope.chart = {
                           options: {
                               chart: {
                                   type: 'column',
                                   width: 600
                               }
                           }
                       };


                       $scope.chart.title = {
                           text: 'Angular Chart'
                       };

                       fieldConfigurationService.getSearchFilterColumns({ value: entityName, value1: settingCategory },
                            onSuccessLoadSearchFC, onFailedLoad);

                       // error function
                       function onFailedLoad(serverResponse) {
                           debugger
                       }

                       function onSuccessLoadSearchFC(data) {

                           $scope.searchFieldConfigurations = [];
                           $scope.searchColumnSources = [];

                           $scope.sourceItem = {};
                           $scope.date = null;

                           // loop thru all search filter columns
                           for (var i = 0; i < data.length - 1; i++) {

                               var columnName = data[i].Name;

                               if (columnName == 'GroupBy') {

                                   var searchItem = {
                                       Name: data[i].Name
                                       , Value: data[i].Value
                                   };
                                   if (data[i].Value != -1)
                                       $scope.GroupBy = data[i].Value;
                                   else
                                       $scope.GroupBy = 'All';
                                   $scope.searchFieldConfigurations.push(searchItem);

                               }
                               else if (columnName != 'GroupByDirection' && columnName != 'SubGroupByDirection' && columnName != 'WorkDate') {

                                   if (!data[i].Value.length || data[i].Value == '-1') {
                                       var searchItem = {
                                           Name: data[i].Name
                                        , Value: null
                                       };
                                   }
                                   else {
                                       var searchItem = {
                                           Name: data[i].Name
                                           , Value: data[i].Value
                                       };

                                   };
                                   $scope.searchFieldConfigurations.push(searchItem);

                               }
                               else if (columnName == 'WorkDate') {
                                   $scope.date = data[i].Value;
                               };
                           }

                           //debugger

                           scheduleService.getStatisticDisplayData({ value: JSON.stringify($scope.searchFieldConfigurations), value1: $scope.date },
                                                          onSuccessLoadDatabaseDisplayObjects, onFailedDisplayLoad);

                       }



                       function onSuccessLoadDatabaseDisplayObjects(data) {

                                                  
                           scheduleService.getStatisticKeyData({ value: JSON.stringify($scope.searchFieldConfigurations), value1:$scope.date },
                              onSuccessLoadKeyObjects, onFailedKeyLoad);
                           
                           $scope.statistics = data;
                           var tempArray = [];
                           for (i = 0; i < data.length - 1; i++) {
                               tempString = data[i]["Total"];
                               tempArray.push(tempString);

                           }
                           
                           $scope.chart.series = [{
                               name: 'Total',
                               data: tempArray
                           }];
                           
                       }

                       function onFailedDisplayLoad(serverResponse) {
                           debugger
                       }

                       function onSuccessLoadKeyObjects(data) {

                           
                           $scope.statisticKeys = data;
                           
                           $scope.chart.xAxis = {
                               categories: data
                           };
                           
                           $scope.chart.yAxis = {
                               min: 0,


                               title: {
                                   text: 'Hours'
                               }
                           };
                           
                           //$scope.chart.series = [{

                           //    data: [3]
                           //}];
                           for (i = 0; i < data.length; i++) {
                               $scope.statistics[i].Name = data[i];
                           }
                           
                           var table = document.getElementById("angularStats");
                           
                           for (var i = 0, row; row = table.rows[i]; i++) {
                               if (i == table.rows.length - 1) {
                                   for (var j = 0, col; col = row.cells[j]; j++) {
                                       col.style.fontWeight = "bold";
                                       col.style.background = "#b0c4de";
                                   }
                               }

                           }
                           
                       }

                       function onFailedKeyLoad(serverResponse) { debugger }

                   }

            ]);

            app.factory('scheduleService', [
                  '$resource',
                  function ($resource) {
                      return $resource('./api/schedule/:detailId'
                          , null
                          , {
                              'getStatisticDisplayData': { method: 'GET', url: '/apiV2/schedule/GetStatisticDisplayData/:value/:value1', isArray: true }
                               , 'getStatisticKeyData': { method: 'GET', url: '/apiV2/schedule/GetStatisticKeyData/:value/:value1', isArray: true }
                          }
                          );
                  }
            ]);

            app.factory('fieldConfigurationService', [
                  '$resource',
                  function ($resource) {
                      return $resource('./api/fieldConfiguration/:detailId'
                          , null
                          , {
                              'getSearchFilterColumns': { method: 'GET', url: '/apiV2/FieldConfiguration/GetSearchFilterColumns/:value/:value1', isArray: true }

                            }
                          );
                  }
            ]);


            angular.bootstrap(document.getElementById("scheduleAppContainer"), ['scheduleApp']);

        </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ControlVisibilityManager">
    <vc:VCManager ID="oVC" runat="server" />
</asp:Content>




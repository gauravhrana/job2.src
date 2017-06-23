<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Prototype/Site.Master" AutoEventWireup="true" CodeBehind="AngularDashboardFrameworkSample.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.Dashboard.AngularDashboardFrameworkSample" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SectionName" runat="server">
    Angular Dashboard Framework Sample
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <!-- styles -->

    <link href="../../bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="../../bower_components/angular-dashboard-framework/dist/angular-dashboard-framework.min.css" rel="stylesheet">

    <!-- scripts -->
    <script type="text/javascript" src="../../bower_components/angular/angular.js"></script>
    <script type="text/javascript" src="../../bower_components/Sortable/Sortable.js"></script>
    <script type="text/javascript" src="../../bower_components/angular-bootstrap/ui-bootstrap.js"></script>
    <script type="text/javascript" src="../../bower_components/angular-bootstrap/ui-bootstrap-tpls.js"></script>
    <script type="text/javascript" src="../../bower_components/angular-dashboard-framework/dist/angular-dashboard-framework.js"></script>

    <link href="../../bower_components/adf-widget-clock/dist/adf-widget-clock.min.css" rel="stylesheet">
    <script type="text/javascript" src="../../bower_components/moment/moment.js"></script>
    <script type="text/javascript" src="../../bower_components/adf-widget-clock/dist/adf-widget-clock.min.js"></script>
    <div ng-app="adfWidgetSample">
        <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#">ADF Widget Sample</a>
                </div>
            </div>
        </div>

        <div class="container" ng-controller="dashboardController">
            <adf-dashboard name="widgetSampleDashboard" structure="4-8" adf-model="dashboard.model" />
        </div>
        <!-- bower:js -->
        <!-- endbower -->

        <!-- inject:js -->
        <!-- endinject -->

        <script type="text/javascript">
            angular
             .module('adfWidgetSample', ['adf', 'adf.widget.clock', 'LocalStorageModule'])
             .config(function (dashboardProvider, localStorageServiceProvider) {
                 localStorageServiceProvider.setPrefix('adf.clock');
                 dashboardProvider.structure('4-8', {
                     rows: [{
                         columns: [{
                             styleClass: 'col-md-4',
                             widgets: []
                         }, {
                             styleClass: 'col-md-8',
                             widgets: []
                         }]
                     }]
                 })
             }).controller('dashboardController', function ($scope, localStorageService) {
                 var model = localStorageService.get('widgetSampleDashboard');
                 if (!model) {
                     model = {
                         rows: [{
                             columns: [{
                                 styleClass: 'col-md-4',
                                 widgets: []
                             }, {
                                 styleClass: 'col-md-8',
                                 widgets: [{
                                     type: 'clock',
                                     title: 'Clock',
                                     config: {}
                                 }]
                             }]
                         }]
                     };
                 }
                 $scope.dashboard = {
                     model: model
                 };
                 $scope.$on('adfDashboardChanged', function (event, name, model) {
                     localStorageService.set(name, model);
                 });
             });
        </script>
    </div>

</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeBehind="UserLoginHistoryAngularSearch.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.Mongodb.UserLoginHistoryAngularSearch" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .myGridStyle {
            widows: 400;
        }

        .ui-grid-header-cell-wrapper{
            height: 30px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ListControlItem" runat="server">


    <link rel="stylesheet" type="text/css" href="style.css" />


    <div ng-app="AngularApp" ng-controller="UserLoginCtrl">
        <div class="panel panel-info">
            <div class="collapse in panel-body" id="pnlSearchParameters">

                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="col-sm-1 control-label">UserName:</label>
                        <div class="col-sm-9">
                            <input type="text" ng-model="searchText" class="form-control" placeholder="Search" style="width: 200px;" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-1 control-label">ServerName:</label>
                        <div class="col-sm-9">
                            <input type="text" ng-model="searchServerText" class="form-control" placeholder="Search" style="width: 200px;" />
                        </div>
                    </div>


                    <div class="form-group">
                        <label class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">

                            <a href="#" class="btn btn-default" ng-click="GetUserLoginHistoryList();">Search</a>
                            <a href="#" class="btn btn-default" ng-click="ResetValues();">Reset</a>

                        </div>
                    </div>

                </div>


            </div>
        </div>
        <div class="panel panel-info">
            <div id="grid1" ui-grid="uiGridOptions" class="gridStyle"></div>
        </div>
    </div>

    <script>

        var app = angular.module('AngularApp', ['ngResource', 'ui.grid']);

        app.controller('UserLoginCtrl', ['$scope', '$injector', 'userLoginHistoryService',
               function ($scope, $injector, userLoginHistoryService) {

                   $scope.searchText = '';
                   $scope.searchServerText = '';

                   $scope.GetUserLoginHistoryList = function () {
                       userLoginHistoryService.getUserHistoryData(
                           {
                                    value: JSON.stringify($scope.searchText)
                               ,    value1: JSON.stringify($scope.searchServerText)
                           }, onSuccessLoadUserLoginData, onFailedLoad);
                   };

                   $scope.GetUserLoginHistoryList();

                   function onSuccessLoadUserLoginData(data) {
                       $scope.myData = data;
                   }

                   function onFailedLoad(serverResponse) {
                       debugger;
                   }

                   $scope.ResetValues = function () {
                       $scope.searchText = '';
                       $scope.searchServerText = '';
                       $scope.GetUserLoginHistoryList();
                   };

                   $scope.uiGridOptions = {
                       data: 'myData',
                       columnDefs:  [
                                        { field: 'Application', displayName: 'Application', width: 200 },
                                        { field: 'UserName', displayName: 'User Name', width: 150 },
                                        { field: 'URL', displayName: 'URL', width: 200 },
                                        { field: 'ServerName', displayName: 'Server Name', width: 150 },
                                        { field: 'DateVisited', displayName: 'Record Date', width: 200, cellFilter: 'date:\'dd/MM/yyyy hh:mm:ss\'' }
                                    ]
                   };

                   $scope.uiGridOptions.onRegisterApi = function (gridApi) {
                       $scope.gridApi = gridApi;
                   };
                   $scope.$watch('test.isActive', function (active, oldActive) {
                       if (active && active !== oldActive && $scope.gridApi) {
                           $timeout(function () {
                               $scope.gridApi.grid.handleWindowResize();
                           });
                       }
                   });
               }
        ]);


        app.factory('userLoginHistoryService', [
              '$resource',
              function ($resource) {
                  return $resource('./api/userloginhistory/:detailId'
                      , null
                      , {
                          'getUserHistoryData': { method: 'GET', url: '/apiV2/userLoginHistory/GetListByUserName/:value/:value1', isArray: true }

                      }
                      );
              }
        ]);

    </script>



</asp:Content>




<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeBehind="UserLoginAngularSearch.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.Mongodb.UserLoginAngularSearch" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>



<asp:Content ID="Content3" ContentPlaceHolderID="ListControlItem" runat="server">

    <style type="text/css">
        .gridStyle {
            border: 1px solid rgb(212,212,212);
            width: 400px;
        }

        .ui-grid-header-cell-wrapper{
            height: 30px !important;
        }
    </style>

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
                        <label class="col-sm-3 control-label"></label>
                        <div class="col-sm-9">

                            <a href="#" class="btn btn-default" ng-click="GetUserLoginList();">Search</a>
                            <a href="#" class="btn btn-default" ng-click="ResetValues();">Reset</a>

                        </div>
                    </div>

                </div>
            </div>

        </div>


        <div class="gridStyle" style="width: 100%; height: 100%" ui-grid="gridOptions"></div>

    </div>

    <script>

        var app = angular.module('AngularApp', ['ngResource', 'ui.grid']);

        app.controller('UserLoginCtrl', ['$scope', '$injector', 'userLoginService',
               function ($scope, $injector, userLoginService) {
                   $scope.searchText = '';

                   $scope.GetUserLoginList = function () {
                       userLoginService.getUserData({ value: JSON.stringify($scope.searchText) },
                                onSuccessLoadUserLoginData, onFailedLoad);
                   };

                   function onSuccessLoadUserLoginData(data) {
                       $scope.myData = data;
                   }

                   $scope.GetUserLoginList();

                   function onFailedLoad(serverResponse) {
                       debugger;
                   }

                   $scope.ResetValues = function () {
                       $scope.searchText = '';
                       $scope.GetUserLoginList();
                   };

                   $scope.gridOptions = {
                       data: 'myData'
                       , columnDefs: [
                                        { field: 'Application', displayName: 'Application', width: 200 },
                                        { field: 'UserName', displayName: 'UserName' },
                                        { field: 'UserLoginStatus', displayName: 'UserLoginStatus', width: 200 },
                                        { field: 'RecordDate', displayName: 'RecordDate', width: 200 }
                                     ]

                   };
               }
        ]);


        app.factory('userLoginService', [
              '$resource',
              function ($resource) {
                  return $resource('./api/userlogin/:detailId'
                      , null
                      , {
                          'getUserData': { method: 'GET', url: '/apiV2/userLogin/GetListByUserName/:value', isArray: true }
                      }
                      );
              }
        ]);

    </script>



</asp:Content>




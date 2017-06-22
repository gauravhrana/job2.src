<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeBehind="UserLoginAngular.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.Mongodb.UserLoginAngular" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>



<asp:Content ID="Content3" ContentPlaceHolderID="ListControlItem" runat="server">

    <style type="text/css">
        .myGridStyle {
            widows: 400;
        }

        .ui-grid-header-cell-wrapper {
            height: 30px !important;
        }
    </style>


    <div ng-app="AngularApp" ng-controller="UserLoginCtrl">
        <div class="panel panel-info">
            <div class="collapse in panel-body" id="pnlSearchParameters">

                <div class="form-horizontal">
                    <div class="form-group">

                        <div>
                            <div>
                                <a href="#" class="button" ng-click="GetUserLoginList();">UserLoginDetails</a>

                            </div>
                            <div>
                                <a href="#" class="button" ng-click="GetUserLoginHistoryList();">UserLoginHistoryDetails</a>
                            </div>
                            <div>
                                <a href="#" class="button" ng-click="GetUserLoginStatusList();">UserLoginStatusDetails</a>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>

        <div class="panel panel-info">
            <div class="gridStyle" ui-grid="gridOptions"></div>
        </div>

    </div>

    <script>

        var app = angular.module('AngularApp', ['ngResource', 'ui.grid']);

        app.controller('UserLoginCtrl', ['$scope', 'userLoginService', 'userLoginHistoryService', 'userLoginStatusService',
               function ($scope, userLoginService, userLoginHistoryService, userLoginStatusService) {


                   $scope.GetUserLoginList = function () {
                       userLoginService.getUserLoginList({},
                                onSuccessLoadUserLoginData, onFailedLoad);

                   };

                   $scope.GetUserLoginHistoryList = function () {
                       userLoginHistoryService.getUserLoginHistoryList({},
                                onSuccessLoadUserLoginHistoryData, onFailedHistoryLoad);
                   };

                   $scope.GetUserLoginStatusList = function () {
                       userLoginStatusService.getUserLoginStatusList({},
                                onSuccessLoadUserLoginStatusData, onFailedStatusLoad);

                   };

                   function onSuccessLoadUserLoginData(data) {
                       $scope.myData = data;
                   }

                   function onSuccessLoadUserLoginHistoryData(data) {
                       $scope.myData = data;
                   }

                   function onSuccessLoadUserLoginStatusData(data) {
                       $scope.myData = data;
                   }

                   function onFailedLoad(serverResponse) { debugger }

                   function onFailedHistoryLoad(serverResponse) { debugger }

                   function onFailedStatusLoad(serverResponse) { debugger }

                   $scope.colDefs = [];

                   $scope.$watch('myData', function () {
                       $scope.colDefs = [];

                       angular.forEach(Object.keys($scope.myData[0]), function (key) {
                           $scope.colDefs.push({ field: key });
                       });
                   }
                   );


                   $scope.gridOptions = {
                       data: 'myData'
                   };
               }
        ]);


        app.factory('userLoginService', [
              '$resource',
              function ($resource) {
                  return $resource('./api/userlogin/:detailId'
                      , null
                      , {
                          'getUserLoginList': { method: 'GET', url: '/apiV2/userLogin/GetList', isArray: true }
                      }
                      );
              }
        ]);

        app.factory('userLoginHistoryService', [
          '$resource',
          function ($resource) {
              return $resource('./api/userloginhistory/:detailId'
                  , null
                  , {
                      'getUserLoginHistoryList': { method: 'GET', url: '/apiV2/userLoginHistory/GetList', isArray: true }

                  }
                  );
          }
        ]);

        app.factory('userLoginStatusService', [
          '$resource',
          function ($resource) {
              return $resource('./api/userloginstatus/:detailId'
                  , null
                  , {
                      'getUserLoginStatusList': { method: 'GET', url: '/apiV2/userLoginStatus/GetList', isArray: true }
                  }
                  );
          }
        ]);

    </script>



</asp:Content>




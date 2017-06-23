<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeBehind="UserLoginAngularSearch.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.Mongodb.UserLoginAngularSearch" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>



<asp:Content ID="Content3" ContentPlaceHolderID="ListControlItem" runat="server">

    <style type="text/css">
        .grid {
            width: 100%;
        }
    </style>

    <div ng-app="AngularApp" ng-controller="userLoginCtrl">
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
        <p>Number of characters left: <span ng-bind="left()"></span></p>

        <div ui-grid="gridOptions" ui-grid-selection class="grid"></div>


    </div>

    <script>
        alert('hi');
        var app = angular.module('AngularApp', ['ngResource', 'ui.grid']);

        alert('hi2');
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

        app.controller('userLoginCtrl', ['$scope', 'userLoginService',
		        function ($scope, userLoginService) {

                   alert('hi3');
                   $scope.message = "hi";
                   $scope.left = function () { return $scope.message.length; };
                   $scope.searchText = '';


                   userLoginService.getUserData({ value: JSON.stringify($scope.searchText) },
                            onSuccessLoadUserLoginData, onFailedLoad);

                   alert('hi4');
                   function onSuccessLoadUserLoginData(data) {
                       debugger;

                       $scope.myData = data;
                       $scope.gridOptions = data;
                   }

                   $scope.GetUserLoginList();

                   function onFailedLoad(serverResponse) {
                       debugger;
                   }

                   $scope.ResetValues = function () {
                       $scope.searchText = '';
                       $scope.GetUserLoginList();
                   };
               }
        ]);

    </script>



</asp:Content>




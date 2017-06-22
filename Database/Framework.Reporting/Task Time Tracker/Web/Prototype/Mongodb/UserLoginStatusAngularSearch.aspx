<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" 
    CodeBehind="UserLoginStatusAngularSearch.aspx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.Mongodb.UserLoginStatusAngularSearch" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>



<asp:Content ID="Content3" ContentPlaceHolderID="ListControlItem" runat="server">

    <style type="text/css">
        .gridStyle
        {
            border: 1px solid rgb(212,212,212);
            width: 400px;
           
        } .ui-grid-header-cell-wrapper {
            height: 30px !important;
        }
    </style>

    
    <div ng-app="AngularApp" ng-controller="UserLoginCtrl">
        <div class="panel panel-info">
        <div class="collapse in panel-body" id="pnlSearchParameters">

				<div class="form-horizontal">
                    <div class="form-group">
						<label class="col-sm-1 control-label">Name:</label>
						<div class="col-sm-9">							
                            <input type="text" ng-model="searchText" class="form-control" placeholder="Search" style="width:200px;" />
						</div>
					</div>

					<div class="form-group">
						<label class="col-sm-3 control-label"></label>
						<div class="col-sm-9">              
							
                            <a href="#" class="btn btn-default" ng-click="GetUserLoginStatusList();">Search</a>
                             <a href="#" class="btn btn-default" ng-click="ResetValues();">Reset</a>
							
						</div>
					</div>

				</div>
            </div>

			</div>
       
        
       <div class="gridStyle" style="width: 100%;height:100%" ui-grid="gridOptions"></div>

         
          </div>

    <script>

        var app = angular.module('AngularApp', ['ngResource', 'ui.grid']);

        app.controller('UserLoginCtrl', ['$scope', 'userLoginStatusService',
               function ($scope, userLoginStatusService) {

                   $scope.searchText = '';

                   $scope.GetUserLoginStatusList = function () {
                       userLoginStatusService.getUserStatusData({ value: JSON.stringify($scope.searchText) },
                                onSuccessLoadUserLoginData, onFailedLoad);
                   };

                   function onSuccessLoadUserLoginData(data) {
                       $scope.myData = data;
                   }

                   $scope.GetUserLoginStatusList();

                   function onFailedLoad(serverResponse) { debugger }

                   $scope.ResetValues = function () {
                       $scope.searchText = '';
                       $scope.GetUserLoginStatusList();
                   };

                   $scope.gridOptions = {
                       data: 'myData',
                       columnDefs:  [
                                        { field: 'Application', displayName: 'Application', width: 200 },
                                        { field: 'Name', displayName: 'Name', width: 200 },
                                        { field: 'Description', displayName: 'Desc', width: 250 },
                                        { field: 'SortOrder', displayName: 'Order', width: 150 }
                                    ]
                   };
               }
        ]);


        app.factory('userLoginStatusService', [
              '$resource',
              function ($resource) {
                  return $resource('./api/userloginstatus/:detailId'
                      , null
                      , {
                          'getUserStatusData': { method: 'GET', url: '/apiV2/userLoginStatus/GetListByUserName/:value/:value1', isArray: true }
                      }
                      );
              }
        ]);

    </script>



</asp:Content>




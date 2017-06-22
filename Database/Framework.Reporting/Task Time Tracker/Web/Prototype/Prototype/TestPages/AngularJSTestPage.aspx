<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeBehind="AngularJSTestPage.aspx.cs" Inherits="Shared.UI.Web.ApplicationManagement.Development.TestPages.AngularJSTestPage" %>

<%@ MasterType TypeName="Framework.UI.Web.BaseClasses.PageDefaultMaster" %>



<asp:Content ID="Content3" ContentPlaceHolderID="ListControlItem" runat="server">

    <style type="text/css">
        .gridStyle
        {
            border: 1px solid rgb(212,212,212);
            width: 400px;
           
        }
    </style>

    <link rel="stylesheet" type="text/css" href="http://angular-ui.github.com/ng-grid/css/ng-grid.css" />
    <link rel="stylesheet" type="text/css" href="style.css" />    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>

    
    <div ng-app="testApp" ng-controller="EntityCtrl">
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
							<%--<button type="submit" value="Reset" class="btn btn-default" id="btnReset" ng-click="GetDatabaseObjectList();" >Reset</button>--%>
                            <a href="#" class="button" ng-click="GetDatabaseObjectList();">Search</a>
                             <a href="#" class="button" ng-click="ResetValues();">Reset</a>
							<%--<button type="submit" value="Search" class="btn btn-default" id="search" ng-click="GetDatabaseObjectList();" >Search</button>--%>
						</div>
					</div>

				</div>
            </div>

			</div>
       
        <%--<button type="submit" value="Search" class="btn btn-default" id="search" ng-click="GetDatabaseObjectList();" >Search</button>--%>
       <div class="gridStyle" style="width: 100%;height:200px" ui-grid="gridOptions"></div>

         <div class="collapse in panel-body" id="Div1">
    	<div class="form-group">
						<label class="col-sm-1 control-label"></label>
						<div class="col-sm-9">
							<button type="submit" value="Insert" class="btn btn-default" id="Button1" ng-click="" >Insert</button>
							<button type="submit" value="Delete" class="btn btn-default" id="Button2" ng-click="" >Delete</button>
                            <button type="submit" value="Details" class="btn btn-default" id="Button3" ng-click="" >Details</button>
                            <button type="submit" value="Update" class="btn btn-default" id="Button4" ng-click=";" >Update</button>
						</div>
            </div>
            </div>
          </div>

    <script>
        
        var app = angular.module('testApp', ['ngResource', 'ngGrid']);
            
        app.controller('EntityCtrl', ['$scope', '$injector','scheduleStateService',
               function ($scope, $injector, scheduleStateService) {                   
                   
                   $scope.searchText = '';
                   $scope.GetDatabaseObjectList = function () {
                       scheduleStateService.getList({ value: $scope.searchText},
                                onSuccessLoadDatabaseObjects, onFailedLoad);
                   };
                  
                   function onSuccessLoadDatabaseObjects(data) {
                       
                       $scope.myData = data;
                       //debugger
                       //for (var i = 0; i < data.length; i++) {
                       //    var tempString = {};
                       //    tempString.Name = data[i];
                       //    $scope.myData.push(tempString);                           
                       //}                       
                   }

                   function onFailedLoad(serverResponse) {debugger}

                   $scope.ResetValues = function () {
                       $scope.searchText = '';
                       $scope.GetDatabaseObjectList();
                   };

                   $scope.gridOptions = {
                       data: 'myData'
                       , checkboxHeaderTemplate: '<input class="ngSelectionHeader" type="checkbox" ng-model="allSelected" ng-change="toggleSelectAll(allSelected)"/>'
                       , showSelectionCheckbox: true
                       , selectWithCheckboxOnly: false                      
                       , multiSelect: true
                       , columnDefs: [{ field: 'Name', displayName: 'Name', width: 200 },
                                                { field: 'Description', displayName: 'Description', width: 200 },
                                                { field: 'SortOrder', displayName: 'SortOrder' }]

                   };
               }
           ]);

       
        app.factory('scheduleStateService', [
              '$resource',
              function ($resource) {
                  return $resource('./api/scheduleState/:detailId'
                      , null
                      , {
                          'getList': { method: 'GET', url: '/apiV2/scheduleState/GetListByApplication/:value' , isArray: true }
                          
                      }
                      );
              }
        ]);

    </script>



</asp:Content>




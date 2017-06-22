'use strict';

angular.module('rootAppShell')            
.controller('modalRowEditorCtrl', ['$scope', '$modal', '$injector','scheduleStateService',
           function ($scope, $modal, $injector, scheduleStateService) { 

               $scope.searchText = '';

               $scope.someProp = 'abc';
               $scope.showMe = function () {
                   alert($scope.someProp);
               };


               scheduleStateService.getList({ value: $scope.searchText },
                        onSuccessLoadDatabaseObjects, onFailedLoad);

               $scope.myData = function () {
                   scheduleStateService.getList({ value: $scope.searchText },
                            onSuccessLoadDatabaseObjects, onFailedLoad);
               };

               $scope.gridOptions = {
                   data: 'myData'
                   //, checkboxHeaderTemplate: '<input class="ngSelectionHeader" type="checkbox" ng-model="allSelected" ng-change="toggleSelectAll(allSelected)"/>'
                   //, showSelectionCheckbox: true
                   //, selectWithCheckboxOnly: false                      
                   //, multiSelect: true
              , columnDefs: [{ field: 'Name', displayName: 'Name', cellTemplate: 'edit-button.html', width: 200 },
                                       { field: 'Description', displayName: 'Description', width: 200 },
                                       { field: 'SortOrder', displayName: 'SortOrder' },
               {
                   name: 'ShowScope',
                   cellTemplate: '<button class="btn primary" ng-click="grid.appScope.showMe()">Click Me</button>'
               }]

               };

               MainCtrl.$inject = ['$modal'];

              function MainCtrl($modal) {

                   var vm = this;

                   vm.editRow = editRow;
                   vm.gridOptions = {
                       columnDefs: [
                         { field: 'Name', displayName: '', width: 34 },
                          { field: 'Description', displayName: 'Description', width: 200 },
                                       { field: 'SortOrder', displayName: 'SortOrder' }]
                   };


                   init();


                   function editRow(grid, row) {
                       $modal.open({
                           templateUrl: 'edit-modal.html',
                           controller: ['$modalInstance', 'grid', 'row', RowEditCtrl],
                           controllerAs: 'vm',
                           resolve: {
                               grid: function () { return grid; },
                               row: function () { return row; }
                           }
                       });
                   }

                   function init() {
                       scheduleStateService.getList({ value: $scope.searchText },
                             onSuccessLoadDatabaseObjects, onFailedLoad);
                   }
                   function onSuccessLoadDatabaseObjects(data) {

                       vm.gridOptions.data = data;

                       $scope.myData = data;

                       $scope.gridOptions = {
                           data: 'myData'
                           //, checkboxHeaderTemplate: '<input class="ngSelectionHeader" type="checkbox" ng-model="allSelected" ng-change="toggleSelectAll(allSelected)"/>'
                           //, showSelectionCheckbox: true
                           //, selectWithCheckboxOnly: false                      
                           //, multiSelect: true
                      , columnDefs: [{ field: 'Name', displayName: 'Name', width: 200 },
                                               { field: 'Description', displayName: 'Description', width: 200 },
                                               { field: 'SortOrder', displayName: 'SortOrder' },
                       {
                           name: 'ShowScope',
                           cellTemplate: '<button class="btn primary" ng-click="grid.appScope.showMe()">Click Me</button>'
                       }]
                       

                       };
                   }
               }

                  
           function onSuccessLoadDatabaseObjects(data) {
               
               

               $scope.myData = data;

                $scope.gridOptions = {
               data: 'myData'
               //, checkboxHeaderTemplate: '<input class="ngSelectionHeader" type="checkbox" ng-model="allSelected" ng-change="toggleSelectAll(allSelected)"/>'
               //, showSelectionCheckbox: true
               //, selectWithCheckboxOnly: false                      
               //, multiSelect: true
               , columnDefs: [{ field: 'Name', displayName: 'Name', width: 200 },
                                        { field: 'Description', displayName: 'Description', width: 200 },
                                        { field: 'SortOrder', displayName: 'SortOrder' },
                {
                    name: 'ShowScope',
                    cellTemplate: '<button class="btn primary" ng-click="grid.appScope.showMe()">Click Me</button>'
                }]
                

           };
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

          
           }
]);


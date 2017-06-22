'use strict';

angular.module('rootAppShell')
    .controller('dbCleanupCtrl', [
        '$location', '$scope', '$injector', '$routeParams', '$modal', 'userService', 'dbCleanupService', '$timeout', '$log', 'uiGridConstants',
        function ($location, $scope, $injector, $routeParams, $modal, userService, dbCleanupService, $timeout, $log, uiGridConstants) {

            $scope.selectedObjects = null;
            $scope.objectName = '';

            $scope.gridOptions = {
                  enableRowSelection: true
                , multiSelect: true
            };

            $scope.gridOptions.onRegisterApi = function (gridApi) {
                //set gridApi on scope
                $scope.gridApi = gridApi;
                gridApi.selection.on.rowSelectionChanged($scope, function (row) {

                    if (row.isSelected) {
                        var procName = row.entity.Name;
                        $scope.selectedObjectName = procName;
                        dbCleanupService.getDatabaseObjectText(
                                {
                                    value1: procName,
                                    value2: $scope.objectType,
                                    value3: $scope.objectDatabase.ConnectionString
                                }, onSuccessLoadObjectText, onFailedLoad);
                    }
                });
            };

            function onSuccessLoadObjectText(data) {
                $scope.objectPreviewText = data[0];
            }

            // load database drop down
            dbCleanupService.listDatabase(null, onSuccessLoadDBNames, onFailedLoad);

            // on successfull retrieval of data from API controller method via service
            function onSuccessLoadDBNames(data) {

                var tempString = {};
                tempString.ConnectionString = "Configuration";

                data.splice(0, 0, tempString);
                $scope.databaseNames = data;

                $scope.objectDatabase = data[0];
                $scope.objectType = "ip";
            }

            function onFailedLoad(serverResponse) {
                userService.AlertManager.logFailureAlert('', serverResponse.data, []);
            }

            $scope.getDatabaseObjects = function () {
                dbCleanupService.getDatabaseObjects(
                        {
                            value1: JSON.stringify($scope.objectName),
                            value2: $scope.objectType,
                            value3: $scope.objectDatabase.ConnectionString
                        }, onSuccessLoadDatabaseObjects, onFailedLoad);
            };

            $scope.dropDatabaseObjects = function () {

                var selectedObjs = "";
                var selectedRows = $scope.gridApi.selection.getSelectedRows();

                for (var i = 0; i < selectedRows.length; i++) {
                    selectedObjs = selectedObjs + selectedRows[0].Name + "-";
                }

                dbCleanupService.dropDatabaseObject
                (
                    {
                        value1: selectedObjs,
                        value2: $scope.objectType,
                        value3: $scope.objectDatabase.ConnectionString
                    },
                    onSuccessDropObjects,
                    onFailedLoad
                );
            }

            function onSuccessLoadDatabaseObjects(data) {

                $scope.objectNames = [];
                for (var i = 0; i < data.length; i++) {
                    var tempString = {};
                    tempString.Name = data[i];
                    $scope.objectNames.push(tempString);
                }

                $scope.gridOptions.data = $scope.objectNames;
            }

            function onSuccessDropObjects(data) {
                $scope.getDatabaseObjects();
            }

        }
    ]);
'use strict';

angular.module('rootAppShell')
    .controller('reportViewerCtrl', [
        '$scope', 'reportService', 'summaryService', 'userService',
        function ($scope, reportService, summaryService, userService) {
            $scope.reportData = {
                Columns: []
            };

            $scope.supportsDate = Modernizr.inputtypes.date;

            $scope.fullScreen = false;
            $scope.summaries = summaryService.getList(null,
                function() {
                },
                function(errorResponse) {
                    userService.AlertManager.addFailureResponse(errorResponse);
                });

            $scope.summaryDataRequest = {
                SummaryCode: '',
                EndDate: new Date()
            };

            $scope.dataSets = [];
            $scope.fullScreenDataSets = [];

            $scope.Download = function() {
                if ($scope.mainForm.$invalid) {
                    userService.AlertManager.addFailureAlert("Invalid parameters.");
                } else {
                    angular.element($scope.mainForm.submitForm).submit();
                }
            };


            $scope.ToggleFullScreen = function(fullScreen) {
                $scope.fullScreen = fullScreen;
                $scope.dataSets = [];
                $scope.fullScreenDataSets = [];
                if (fullScreen) {
                    $scope.fullScreenDataSets.push(createDataSet());
                } else {
                    $scope.dataSets.push(createDataSet());
                }
            }

            var createDataSet = function() {
                var columnDefs = [];
                $scope.reportData.Columns.forEach(function(column) {
                    var columnDef = {
                        field: column,
                        displayName: column,
                        sortable: true,
                        resizable: true
                    };
                    columnDefs.push(columnDef);
                });

                var dataSet = {
                    data: 'reportData.Rows',
                    enableColumnResize: true,
                    enableColumnReordering: true,
                    multiSelect: false,
                    columnDefs: columnDefs
                };
                return dataSet;
            };

            $scope.Run = function() {
                if ($scope.mainForm.$invalid) {
                    userService.AlertManager.addFailureAlert("Invalid parameters.");
                } else {
                    $scope.dataSets = [];
                    $scope.reportData = reportService.requestSummaryData($scope.summaryDataRequest,
                        function (response) {
                            $scope.dataSets.push(createDataSet());
                        },
                        function(errorResponse) {
                            userService.AlertManager.addFailureResponse(errorResponse);
                        });
                }
            };
        }
    ]);

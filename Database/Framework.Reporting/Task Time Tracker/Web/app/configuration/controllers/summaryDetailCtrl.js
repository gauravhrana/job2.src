'use strict';

angular.module('rootAppShell')
    .controller('summaryDetailCtrl', [
        '$location', '$scope', '$routeParams', '$modal', 'summaryService', 'calculationService',  'userService', '$timeout', '$log',
        function ($location, $scope, $routeParams, $modal, summaryService, calculationService, userService, $timeout, $log) {

            //load summary data
            $scope.summary = {
                SummaryCode: $routeParams.summaryCode + '',
                Name: '',
                Description: '',
                SummaryCalculations: []
            };
            
            if (!$scope.WorkflowStateIsNew()) {
                $scope.summary = summaryService.getByCode({ summaryCode: $routeParams.summaryCode },
                    function(response) {
                        //$scope.sortSummaryCalculations();
                    }, function(errorResponse) {
                        userService.AlertManager.addFailureResponse(errorResponse);
                    });
            }

            //load calculation data
            $scope.Calculations = [];
            $scope.CalculationsByCode = {};
            $scope.Calculations = calculationService.getList(null,
                function() {
                    $scope.CalculationsByCode = {};
                    $scope.Calculations.forEach(function(calc) {
                        $scope.CalculationsByCode[calc.CalculationCode] = calc;
                    });
                },
                function(errorResponse) {
                    userService.AlertManager.addFailureResponse(errorResponse);
                });

            
            $scope.showAddSummaryCalculation = function() {
                //$scope.tempSummaryCalculation = {
                //    CalculationCode: ''
                //};
                                
                $scope.modalInstance = $modal.open(
                {
                      scope: $scope
                    , templateUrl: 'app/configuration/views/calculation-selection.html'
                    , controller: 'calculationListModalCtrl'
                    , size: 'lg'
                    , resolve: {
                        selectedElements: function() {
                            return _.toArray(_.chain($scope.summary.SummaryCalculations)
                                    .pluck('CalculationCode')
                                    .flatten()
                                    .unique()
                                    .value()
                                    .sort());                                
                        }                            
                        , hideOrSelect: function () { return 'Hide'; }
                        , title: function () { return 'Select Additional Calculations'; }
                    }
                });                
                
                $scope.modalInstance.result.then(function (selectedItem) {                    
                    $log.debug('Modal selected at: ' + new Date());
                    $log.debug(selectedItem);
                    
                    $scope.summary.SummaryCalculations = $scope.summary.SummaryCalculations.concat(selectedItem);
                                        
                }, function () {                    
                    $log.debug('Modal dismissed at: ' + new Date());                                        
                });
            };

            $scope.cancel = function () {
                $scope.modalInstance.dismiss();
            };

            $scope.deleteSummaryCalculation = function (calculation) {
                $log.info(calculation);                
                $log.info($scope.summary.SummaryCalculations.indexOf(calculation));
                $scope.summary.SummaryCalculations.splice($scope.summary.SummaryCalculations.indexOf(calculation), 1);
            };                        
            
            $scope.showEditSummaryCalculation = function (row) {
                return ($routeParams.calculationCode == '{New}');
                $scope.summary.SummaryCalculations.splice($scope.summary.SummaryCalculations.indexOf(row.entity), 1);
            };

            $scope.save = function() {

                var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';
                                
                $scope.summary = summaryService[saveMethod]($scope.summary,
                    function() {
                        userService.AlertManager.addSuccessAlert('Summary \'' + $scope.summary.Name + '\' saved.');
                        //$location.url('/summaries/');
                    },
                    function(errorResponse) {
                        userService.AlertManager.addFailureAlert('Summary failed to save. ' + [JSON.stringify(errorResponse)]);
                    });
            };

            $scope.delete = function() {
                summaryService.delete({ summaryCode: $scope.summary.SummaryCode },
                    function() {
                        userService.AlertManager.addSuccessAlert('Summary \'' + $scope.summary.Name + '\' deleted.');
                        $location.url('/summaries/');
                    },
                    function onFailedDelete(errorResponse) {
                        userService.AlertManager.addFailureAlert('Summary \'' + $scope.summary.Name + '\' failed to delete.' + [JSON.stringify(errorResponse)]);
                    });
            };            
        }
    ]);



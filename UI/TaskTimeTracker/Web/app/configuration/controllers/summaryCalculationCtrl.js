'use strict';

angular.module('rootAppShell')
    .controller('summaryCalculationCtrl', [
        '$timeout', '$location', '$scope', '$route', '$routeParams', '$modal', '$filter', 'summaryService', 'calculationService', 'userService', '$log',

        function ($timeout, $location, $scope, $route, $routeParams, $modal, $filter, summaryService, calculationService, userService, $log) {
                            
            $scope.summary = {
                SummaryCode: '',
                ActiveStatusCode: 'Active',
                Name: '',
                Description: '',
                FailedCalculationDependency: [],
            };

            $scope.DependencyStatusDisplay = [];
            $scope.DependencyStatusDisplay.isCollapsed = true;
            
            $scope.errors = [];            

            $scope.showFormula = true;
            //$scope.showDependencies = true;
            $scope.showActiveStatusCode = true;
            //$scope.showNotes = false;
            $scope.showCalculationOrder = true;
            $scope.showCalculationType = true;            
            $scope.showCalculations = true;
            $scope.showKeys = true;
            
            // *** 
            // fitlers
            // *** 
            $scope.filterCalculationIsKeyColumn = function (item) {                
                return (item.CalculationTypeCode.toString() == 'KeyColumn'); 
            };

            $scope.filterCalculationIsCalculation = function (item) {                
                return (item.CalculationTypeCode.toString() != 'KeyColumn'); 
            };
            
            // load data
            function refreshData() {
                
                function onFailedLoad(data) {
                    userService.AlertManager.addFailureResponse(data, JSON.stringify($scope.summary));
                }

                function onSuccessLoad(data) {
                                        
                    $scope.summary = data;                    
                    
                    $scope.allCalculationsVisible = true;

                    $scope.togggleCalculations($scope.allCalculationsVisible);

                    $scope.markExecutionDetails();                    
                }

                
                if (!$scope.WorkflowStateIsNew()) {
                        summaryService.getByCodeWithDependencyDetail({
                        summaryCode: $routeParams.summaryCode
                    }
                        , onSuccessLoad
                        , onFailedLoad
                    );
                }
            }            

            // *****
            // Visual Cues -- maybe absctracted to common encapulsated object
            // *****
           
            $scope.allCalculationsVisible = false;
            
            // *** 
            // Toggle
            // *** 
            $scope.togggleCalculations = function (state) {

                // switch scope setting
                $scope.allCalculationsVisible = state;
                
                for (var i = 0; i < $scope.summary.SummaryCalculationsDetails.length; i++) {
                    $scope.summary.SummaryCalculationsDetails[i].isCollapsed = $scope.allCalculationsVisible;
                }                
            };
            
            // *** 
            // Execution Details
            // *** 
            $scope.markExecutionDetails = function () {

                var summary = $scope.summary;

                for (var i = 0; i < summary.SummaryCalculationsDetails.length; i++) {
                    
                    var calc = summary.SummaryCalculationsDetails[i];
                                        
                    for (var q = 0; q < summary.CalculationExecutionOrderDetails.length; q++) {

                        var executionInfo = summary.CalculationExecutionOrderDetails[q];
                        
                        if (executionInfo.CalculationCode == calc.CalculationCode) {
                            calc.ExecutionSortOrder = executionInfo.ExceutionOrder;
                            calc.ExecutionCalculationTypeCode = executionInfo.CalculationTypeCode;
                            break;
                        }
                    }                    
                }                
            };
            
            // ****
            // Ace Editior
            // ****
            
            // ace editor event handler
            $scope.aceLoaded = function (editor) {
                
                //ace.require("ace/ext/language_tools");
                
                editor.setTheme("ace/theme/monokai");
                editor.getSession().setMode("ace/mode/csharp");
                editor.setPrintMarginColumn(false);
                editor.setAutoScrollEditorIntoView(true);
                editor.setOption("maxLines", 30);
                editor.setOption("minLines", 5);
                
            };
            
            $scope.aceChanged = function (e) {
                //
            };
            
            // ****
            // Entity Save and Delete
            // ****
                        
            $scope.WorkflowStateIsNew = function () {
                return ($routeParams.summaryCode == '{New}');
            };
            
            $scope.save = function () {

                var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

                $scope.summary = summaryService[saveMethod]($scope.summary,
                    function () {
                        userService.AlertManager.addSuccessAlert('Summary \'' + $scope.summary.Name + '\' saved.');
                        if ($scope.WorkflowStateIsNew()) {
                            $location.path('/summaryCalculation/' + $scope.summary.SummaryCode).replace();
                            //$location.url('/summaryCalculation/' + $scope.summary.SummaryCode);
                        } else {
                            $route.reload();
                        }
                    },
                    function (errorResponse) {                                                
                        userService.AlertManager.addFailureResponse(errorResponse, JSON.stringify($scope.summary));
                    });
            };
            
            $scope.delete = function () {
                
                if ($scope.WorkflowStateIsNew()) return;
                    
                summaryService.delete({ summaryCode: $scope.summary.SummaryCode },
                    function () {
                        userService.AlertManager.addSuccessAlert('Summary \'' + $scope.summary.Name + '\' deleted.');
                        $location.url('/summaries/');
                    },
                    function onFailedDelete(errorResponse) {
                        //userService.AlertManager.addFailureAlert('Summary \'' + $scope.summary.Name + '\' failed to delete.' + [JSON.stringify(errorResponse)]);
                        userService.AlertManager.addFailureResponse(errorResponse, JSON.stringify($scope.summary));                        
                    });
            };            

            // ****
            // Add  / Delete Additional Calculations
            // ****

            // deletes single calcuation
            $scope.deleteSummaryCalculation = function (calculation) {
                $log.info('deleteSummaryCalculation');
                
                //var itemOfInterest = _.pick(calculation, 'CalculationCode', 'CalculationTypeCode');                
                //var matchedItem = _.where($scope.summary.SummaryCalculations, itemOfInterest);
                
                var filtered = $scope.summary.SummaryCalculations
               .filter(function (el) {
                   return el.CalculationCode !== calculation.CalculationCode;
               });
                
                $log.info(filtered.length);                
                $log.info($scope.summary.SummaryCalculations.length);                
                
                // Actual Remove by reseeting item
                $scope.summary.SummaryCalculations = filtered;
                
                // remove from details 
                $scope.summary.SummaryCalculationsDetails = $scope.summary.SummaryCalculationsDetails
                    .filter(function (el) {
                        return el.CalculationCode !== calculation.CalculationCode;
                    });                                
            };
            
            $scope.showAddSummaryCalculation = function () {
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
                        selectedElements: function () {
                            return _.toArray(_.chain($scope.summary.SummaryCalculations)
                                    .pluck('CalculationCode')
                                    .flatten()
                                    .unique()
                                    .value()
                                    .sort());
                        }
                        , hideOrSelect: function () {
                            return 'Hide';
                        }
                        , title: function () {
                            return 'Select Additional Calculations';
                        }
                    }
                });

                $scope.modalInstance.result.then(function (selectedItem) {
                    
                    $log.debug('Modal selected at: ' + new Date());
                    $log.debug(selectedItem);

                    // add to details info
                    $scope.summary.SummaryCalculationsDetails = $scope.summary.SummaryCalculationsDetails.concat(selectedItem);
                    
                    // sync to details info
                    $log.debug('sync to details info');

                    var additionalItems = _.map(selectedItem, function (calc) {
                        return {
                            CalculationCode: calc.CalculationCode,
                            CalculationTypeCode: calc.CalculationTypeCode                            
                        };
                    });

                    additionalItems.forEach(function(calc) {

                        // find and mark
                        var iCalc = $scope.summary.SummaryCalculationsDetails.some(function(el) {
                            if (el.CalculationCode == calc.CalculationCode) {
                                el.isCollapsed = true;
                                return true;
                            }
                            return false;
                        });                                                
                    });
                                                           
                    $log.debug(additionalItems);
                    
                    $scope.summary.SummaryCalculations = $scope.summary.SummaryCalculations.concat(additionalItems);
                    
                    $log.debug($scope.summary.SummaryCalculations);

                }, function () {
                    $log.debug('Modal dismissed at: ' + new Date());
                });
            };

            $scope.cancel = function () {
                $scope.modalInstance.dismiss();
            };
            
            // Init
            function onInit() {
                refreshData();
            }
            
            // Start it off
            onInit();
        }
    ]);



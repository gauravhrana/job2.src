'use strict';

angular.module('rootAppShell')
    .controller('processRunDetailsCtrl', [
        '$scope', '$routeParams', 'processRunInfoService', 'userService',
        function ($scope, $routeParams, processRunInfoService, userService) {

            //$scope.supportingDetails = [];

            $scope.processRunDetails = processRunInfoService.getDetails({ processRunId: $routeParams.processRunId },
                // on success
                function(response) {                    
                    console.log($scope.processRunDetails);
                    $scope.getSupportingDetails();
                },
                //on failure
                function(errorResponse) {
                    userService.AlertManager.addFailureResponse(errorResponse);
                });

            $scope.actions = processRunInfoService.getActions({ processRunId: $routeParams.processRunId },
                // on success
                function (response) {
                    //do nothing
                },
                //on failure
                function(errorResponse) {                    
                    userService.AlertManager.addFailureResponse(errorResponse);
                });

            $scope.getSupportingDetails = function () {                                
                if ($scope.processRunDetails.ProcessInfo.Code == 'CalculateSummaries') {
                    $scope.processRunDetails.SupportingDetails = processRunInfoService.getSummaryCalculateInfo({ processRunId: $routeParams.processRunId },
                        // on success
                        function(response) {
                            //do nothing                    
                        },
                        //on failure
                        function(errorResponse) {
                            userService.AlertManager.addFailureResponse(errorResponse);
                        });
                } else {
                    console.log('asdf');
                    $scope.processRunDetails.SupportingDetails = null;
                }
            };

            $scope.gridOptions = {
                data: 'actions',
                enableColumnResize: true,
                multiSelect: false,
                sortInfo: { fields: ['StartTime'], directions: ['desc'] },
                columnDefs: [
                    {
                        field: 'ActionInfo.Description',
                        displayName: 'Action',
                        width: '*',
                        sortable: true,
                        resizable: true
                    },
                    { field: 'ActionRunStatusCode', displayName: 'Status', width: '*', sortable: true, resizable: true },
                    {
                        field: 'Message',
                        displayName: 'Message',
                        width: '*',
                        sortable: true,
                        resizable: true,
                        cellTemplate: '<div class="ngCellText" ng-class="col.colIndex()"><div data-title="Message" data-placement="top" data-content="{{row.getProperty(col.field)}}" data-container="body"  bs-modal>{{row.getProperty(col.field)}}</div></div>'
                    },
                    {
                        field: 'Error',
                        displayName: 'Error',
                        width: '*',
                        sortable: true,
                        resizable: true,
                        cellTemplate: '<div class="ngCellText" ng-class="col.colIndex()"><div data-title="Message" data-placement="top" data-content="{{row.getProperty(col.field)}}" data-container="body"  bs-modal>{{row.getProperty(col.field)}}</div></div>'
                    },
                    {
                        field: 'StartTime',
                        displayName: 'Start Time',
                        width: '*',
                        sortable: true,
                        resizable: true,
                        cellTemplate: '<div class="ngCellText" ng-class="col.colIndex()"><div data-title="Message" data-placement="top" data-content="{{row.getProperty(col.field)}}" data-container="body"  bs-modal>{{row.getProperty(col.field) | date : \'medium\'}}</div></div>',
                        cellFilter: 'date:\'MM/dd/yyyy hh:mm\'',
                    },
                    {
                        field: 'EndTime',
                        displayName: 'End Time',
                        width: '*',
                        sortable: true,
                        resizable: true,
                        cellTemplate: '<div class="ngCellText" ng-class="col.colIndex()"><div data-title="Message" data-placement="top" data-content="{{row.getProperty(col.field)}}" data-container="body"  bs-modal>{{row.getProperty(col.field) | date : \'medium\'}}</div></div>',
                        cellFilter: 'date:\'MM/dd/yyyy hh:mm\'',
                    }                
                ]
            };
            
            $scope.gridSupportingDetailsOptions = {
                data: 'processRunDetails.SupportingDetails',
                enableColumnResize: true,
                multiSelect: false,
                sortInfo: { fields: ['StartTime'], directions: ['desc'] },
                columnDefs: [
                    {
                        field: 'SummaryCode',
                        displayName: 'SummaryCode',
                        width: '*',
                        sortable: true,
                        resizable: true
                    },
                    { field: 'SummaryCalculateStatusCode', displayName: 'Status', width: '*', sortable: true, resizable: true },
                    
                    {
                        field: 'StartTime',
                        displayName: 'Start Time',
                        width: '*',
                        sortable: true,
                        resizable: true,
                        cellTemplate: '<div class="ngCellText" ng-class="col.colIndex()"><div data-title="Message" data-placement="top" data-content="{{row.getProperty(col.field)}}" data-container="body"  bs-modal>{{row.getProperty(col.field) | date : \'medium\'}}</div></div>',
                        cellFilter: 'date:\'MM/dd/yyyy hh:mm\'',
                    },
                    {
                        field: 'EndTime',
                        displayName: 'End Time',
                        width: '*',
                        sortable: true,
                        resizable: true,
                        cellTemplate: '<div class="ngCellText" ng-class="col.colIndex()"><div data-title="Message" data-placement="top" data-content="{{row.getProperty(col.field)}}" data-container="body"  bs-modal>{{row.getProperty(col.field) | date : \'medium\'}}</div></div>',
                        cellFilter: 'date:\'MM/dd/yyyy hh:mm\'',
                    },
                    { field: 'Delta', displayName: 'Delta (min)', width: '*', sortable: true, resizable: true },
                    { field: 'NoOfFormulas', displayName: 'Formula Count', width: '*', sortable: true, resizable: true }
                ]
            };

        }
    ]);
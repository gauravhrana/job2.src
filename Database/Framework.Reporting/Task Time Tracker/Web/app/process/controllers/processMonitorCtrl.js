'use strict';

angular.module('rootAppShell')
    .controller('processMonitorCtrl', [
        '$scope', '$routeParams', 'processRunInfoService',
        function($scope, $routeParams, processRunInfoService) {
            if ($routeParams.selectedDateRange == '')
                $scope.selectedDateRange = 'Today';
            else {
                $scope.selectedDateRange = $routeParams.selectedDateRange;
            }
            $scope.processRuns = processRunInfoService.getList();

            $scope.SelectDateRange = function(dateRange) {
                $scope.selectedDateRange = dateRange;
            }

            $scope.gridOptions = {
                data: 'processRuns',
                enableColumnResize: true,                
                multiSelect: false,
                sortInfo: { fields: ['StartTime'], directions: ['desc'] },
                columnDefs: [
                    {
                        field: 'ProcessInfo.Description', displayName: 'Process', width: '*', sortable: true, resizable: true,
                        cellTemplate: '<div class="ngCellText" ng-class="col.colIndex()"><a href="#/processRunDetails/{{row.getProperty(\'ProcessRunId\')}}" title="{{row.getProperty(col.field)}}"><span ng-cell-text>{{row.getProperty(col.field)}}</span></a></div>'
                    },
                    { field: 'ProcessRunStatusCode',    displayName: 'Status',  width: '*', sortable: true, resizable: true },
                    {
                        field: 'Message', displayName: 'Message', width: '*', sortable: true, resizable: true,
                        cellTemplate: '<div class="ngCellText" ng-class="col.colIndex()"><div data-title="Message" data-placement="top" data-content="{{row.getProperty(col.field)}}" data-container="body"  bs-modal>{{row.getProperty(col.field)}}</div></div>'
                    },
                    {
                        field: 'Error', displayName: 'Error', width: '*', sortable: true, resizable: true,
                        cellTemplate: '<div class="ngCellText" ng-class="col.colIndex()"><div data-title="Message" data-placement="top" data-content="{{row.getProperty(col.field)}}" data-container="body"  bs-modal>{{row.getProperty(col.field)}}</div></div>'
                    },
                    { field: 'StartTime', displayName: 'Start Time', width: '*', sortable: true, resizable: true, cellFilter: 'date:\'MM/dd/yyyy hh:mm\'', },
                    { field: 'EndTime', displayName: 'End Time', width: '*', sortable: true, resizable: true, cellFilter: 'date:\'MM/dd/yyyy hh:mm\'', }
                ]                    
            }
        }
    ]);
'use strict';

angular.module('rootAppShell')
    .controller('scheduleStateDetailCtrl', [
        '$location', '$scope', '$routeParams', '$modal', 'scheduleStateService', 'userService', '$timeout', '$log',
        function ($location, $scope, $routeParams, $modal, scheduleStateService, userService, $timeout, $log) {

            // Schedule State Data Model
            $scope.ScheduleState= {
                SummaryCode: $routeParams.detailId + '',
                Name: '',
                Description: '',
                SummaryCalculations: []
            };


            $scope.WorkflowStateIsNew = function () {
                return ($routeParams.detailId == '{New}');
            };

            // init
            if (!$scope.WorkflowStateIsNew()) {
            	$scope.ScheduleState = scheduleStateService.getById({ detailId: $routeParams.detailId },
                    function(response) {
                        //$scope.sortSummaryCalculations();
                    }, function(errorResponse) {
                        userService.AlertManager.addFailureResponse(errorResponse);
                    });
            }

            $scope.save = function() {

                var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

                $scope.ScheduleState = scheduleStateService[saveMethod]($scope.ScheduleState,
                    function() {
                        userService.AlertManager.addSuccessAlert('Schedule State\'' + $scope.ScheduleState.Name + '\' saved.');
                        //$location.url('/summaries/');
                    },
                    function(errorResponse) {
                        userService.AlertManager.addFailureAlert('Schedule State failed to save. ' + [JSON.stringify(errorResponse)]);
                    });
            };

            $scope.delete = function() {
                scheduleStateService.delete({ summaryCode: $scope.summary.SummaryCode },
                    function() {
                        userService.AlertManager.addSuccessAlert('Schedule State\'' + $scope.ScheduleState.Name + '\' deleted.');
                        $location.url('/summaries/');
                    },
                    function onFailedDelete(errorResponse) {
                        userService.AlertManager.addFailureAlert('Schedule State\'' + $scope.ScheduleState.Name + '\' failed to delete.' + [JSON.stringify(errorResponse)]);
                    });
            };
        }
    ]);



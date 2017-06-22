'use strict';
angular.module('rootAppShell')
    .controller('processRequestCtrl', [
        '$scope','$location', 'processInfoService', 'userService','processRequestService',
        function ($scope, $location, processInfoService, userService, processRequestService) {

            $scope.RunMessage = 'Run';
            $scope.supportsDate = Modernizr.inputtypes.date;

            $scope.Run = function() {
                if ($scope.mainForm.$invalid) {
                    userService.AlertManager.addFailureAlert("Invalid parameters.");
                } else {
                    var request = { ProcessCode: $scope.Process.Code, ParameterValues: [] };

                    if ($scope.Process.Parameters != undefined) {
                        $scope.Process.Parameters.forEach(function(p) {
                            request.ParameterValues.push({ Code: p.Name, Values: [p.Value] });
                        });
                    }

                    processRequestService.submit(request,
                        // on success
                        function() {
                            $location.url('processRequestSuccess');
                        },
                        //on failure
                        function(errorResponse) {
                            userService.AlertManager.addFailureResponse(errorResponse);
                        });
                }
            };
            $scope.ProcessDictionary = {};
            $scope.ProcessCode = '';
            $scope.Process = {};
            $scope.$watch('ProcessCode', function() {
                $scope.Process = angular.copy($scope.ProcessDictionary[$scope.ProcessCode]);
                return;
            });

            $scope.processes = processInfoService.getList(null, function() {
                $scope.ProcessDictionary = {};
                $scope.processes.forEach(function(process) {
                    $scope.ProcessDictionary[process.Code] = process;
                });
            });           
        }
    ]);
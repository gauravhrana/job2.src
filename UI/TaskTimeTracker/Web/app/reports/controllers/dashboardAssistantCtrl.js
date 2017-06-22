'use strict';

angular.module('rootAppShell')
    .controller('dashboardAssistantCtrl', [
        '$scope', '$modalInstance', 'userService', function($scope, $modalInstance) {

            $scope.steps = [];
            $scope.step1 = { number: 1, header: 'Choose Data', template: './app/reports/views/dashboard-assistant-choose-data.html', active: false };
            $scope.steps.push($scope.step1);
            $scope.step2 = { number: 2, header: 'Design', template: './app/reports/views/dashboard-assistant-design.html', active: false };
            $scope.steps.push($scope.step2);
            $scope.step3 = { number: 3, header: 'Choose Location', template: './app/reports/views/dashboard-assistant-choose-location.html', active: false };
            $scope.steps.push($scope.step3);
            $scope.step4 = { number: 4, header: 'Finish', template: './app/reports/views/dashboard-assistant-finish.html', active: false };
            $scope.steps.push($scope.step4);

            $scope.selectedStep = undefined;
            $scope.nextStep = undefined;
            $scope.previousStep = undefined;


            $scope.ok = function() {
                $modalInstance.close($scope);
            };

            $scope.cancel = function() {

                $modalInstance.dismiss('cancel');
            };

            $scope.delete = function() {
                $modalInstance.dismiss('delete');
            };

            $scope.back = function() {
                selectStep($scope.previousStep);
            };

            $scope.next = function() {
                selectStep($scope.nextStep);
            };

            function selectStep(step) {
                if (step == undefined) {
                    return;
                }
                $scope.selectedStep = step;
                $scope.steps.forEach(function(s) { s.active = false; });
                $scope.selectedStep.active = true;


                var index = $scope.steps.indexOf(step);

                if (index == 0) {
                    $scope.previousStep = undefined;
                } else {
                    $scope.previousStep = $scope.steps[index - 1];
                }

                if (index == $scope.steps.length - 1) {
                    $scope.nextStep = undefined;
                } else {
                    $scope.nextStep = $scope.steps[index + 1];
                }
            }

            selectStep($scope.step1);
        }
    ]);

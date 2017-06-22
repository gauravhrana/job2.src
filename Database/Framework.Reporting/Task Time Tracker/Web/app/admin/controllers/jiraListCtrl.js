'use strict';

angular.module('rootAppShell')
    .controller('jiraListCtrl', [
        '$location', '$scope', '$injector', '$routeParams', '$modal', 'userService', 'jiraListService', '$timeout', '$log',
        function ($location, $scope, $injector, $routeParams, $modal, userService, jiraListService, $timeout, $log) {

            $scope.showJIRAList = false;

            function onFailedLoad(serverResponse) {
                debugger
                userService.AlertManager.logFailureAlert('', serverResponse.data, []);
            }

            $scope.getJIRAIssues = function () {
                jiraListService.getJIRAIssues(null, 
                    function (data) {
                        debugger
                        $scope.showJIRAList = true;
                        $scope.jiraIssues = data;
                        
                    }, onFailedLoad);
            }

            function onSuccessLoadJiraIssues(data) {
                debugger
                $scope.showJIRAList = true;
                $scope.jiraIssues = data;
            }

        }
    ]);
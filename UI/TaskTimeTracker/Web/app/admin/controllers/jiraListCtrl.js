'use strict';

angular.module('rootAppShell')
    .controller('jiraListCtrl', [
        '$location', '$scope', '$injector', '$routeParams', '$modal', 'userService', 'jiraListService', '$timeout', '$log',
        function ($location, $scope, $injector, $routeParams, $modal, userService, jiraListService, $timeout, $log) {

            $scope.showJIRAList = false;

            function onFailedLoad(serverResponse) {
                userService.AlertManager.logFailureAlert('', serverResponse.data, []);
            }

            $scope.getJIRAIssues = function () {
                jiraListService.getJIRAIssues(null, 
                    function (data) {
                        $scope.showJIRAList = true;
                        $scope.jiraIssues = data;
                        
                    }, onFailedLoad);
            }

            function onSuccessLoadJiraIssues(data) {
                $scope.showJIRAList = true;
                $scope.jiraIssues = data;
            }

        }
    ]);
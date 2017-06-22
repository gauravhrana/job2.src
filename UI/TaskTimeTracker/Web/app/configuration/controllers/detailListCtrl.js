'use strict';

angular.module('rootAppShell')
    .controller('detailListCtrl', [
        '$scope', '$location', 'userService', 'detailService',
        function($scope, $location, userService, detailService) {
            $scope.details = detailService.getList(null, function() {}, function(errorResponse) {
                userService.AlertManager.addFailureResponse(errorResponse);
            });

            $scope.SubmitMessage = 'Add New';
            $scope.AddNew = function() {
                $scope.SubmitMessage = 'Adding New ...';
                $location.url('/details/{New}');
            };
        }
    ]);


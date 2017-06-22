'use strict';

angular.module('rootAppShell')
    .controller('genevaQueryListCtrl', [
        '$scope', '$location', 'userService', 'genevaQueryService',
        function($scope, $location, userService, genevaQueryService) {
            $scope.queries = genevaQueryService.getList(null, function() {}, function(errorResponse) {
                userService.AlertManager.addFailureResponse(errorResponse);
            });
            $scope.SubmitMessage = 'Add New';
            $scope.AddNew = function() {
                $scope.SubmitMessage = 'Adding New ...';
                $location.url('/genevaQueries/{New}');
            };
        }
    ]);
'use strict';

angular.module('rootAppShell')
    .controller('summaryListCtrl', [
        '$scope', '$location', 'summaryService', 'cachingService',
            function ($scope, $location, summaryService, cachingService) {
            $scope.summaries = summaryService.query();
            
            $scope.errors = [];
            $scope.CanSave = true;
            $scope.SubmitMessage = 'Add New';
            $scope.searchText = cachingService.get('SummarySearch');
            
            $scope.$watch('searchText', function (newVal, oldVal, scope) {
                cachingService.set('SummarySearch', newVal);
            });

            $scope.AddNew = function () {

                $scope.SubmitMessage = 'Adding New ...';

                $location.url('/summaryCalculation/{New}');

            };
        }
    ]);
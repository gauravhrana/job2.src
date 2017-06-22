'use strict';

angular.module('rootAppShell')
    .controller('scheduleStateListCtrl', [
        '$scope', '$location', 'scheduleStateService', 'userService', 'cachingService',

        function ($scope, $location, scheduleStateService, userService, cachingService) {

            $scope.UniqueCategories = [];
            $scope.UniqueCategoriesChunked = [];
            $scope.errors = [];
            $scope.CanSave = true;
            $scope.SubmitMessage = 'Add New';

            $scope.searchText = cachingService.get('ScheduleStateSearch');

            $scope.$watch('searchText', function (newVal, oldVal, scope) {
            	cachingService.set('ScheduleStateSearch', newVal);
            });

            function onFailedLoad(serverResponse) {
                userService.AlertManager.logFailureAlert('', serverResponse.data, []);
            }

            function onSuccessLoad(data) {
                $scope.ScheduleStates = data;
            }

            $scope.AddNew = function () {
                $scope.SubmitMessage = 'Adding New ...';
                $location.url('/scheduleStates/{New}');
            };

            scheduleStateService.getList(null, onSuccessLoad, onFailedLoad);
        }

    ]);
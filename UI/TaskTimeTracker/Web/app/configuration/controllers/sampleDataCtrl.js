'use strict';

angular.module('rootAppShell')
    .controller('sampleDataCtrl', [
        '$scope', '$location', 'calculationService', 'calculationSelectionService', 'userService', 'cachingService',

        function ($scope, $location, calculationService, calculationSelectionService, userService, cachingService) {

            $scope.errors = [];

            function onFailedLoad(serverResponse) {
                userService.AlertManager.logFailureAlert('', serverResponse.data, []);
            }

            function onSuccessLoad(data) {
                $scope.SampleData = data;
            }

            calculationService.getList(null, onSuccessLoad, onFailedLoad);
        }
    ]);
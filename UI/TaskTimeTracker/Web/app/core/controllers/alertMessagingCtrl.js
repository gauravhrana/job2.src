angular.module('rootAppShell')
    .controller('alertMessagingCtrl', ['$scope', 'userService', function ($scope, userService) {

        $scope.successList = userService.AlertManager.successList;

        $scope.failureList = userService.AlertManager.failureList;

        $scope.removeFailureItem = function (index) {
            userService.AlertManager.failureList.splice(index, 1);
        };

        $scope.removeSuccessItem = function (index) {
            userService.AlertManager.successList.splice(index, 1);
        };
    }]);
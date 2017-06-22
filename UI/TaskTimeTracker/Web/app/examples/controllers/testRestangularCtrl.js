'use strict';

angular.module('rootAppShell')
    .controller('testRestangularCtrl', function ($scope, Restangular, $location) {

        var bathroomAPI = Restangular.all('BathRooms');

        bathroomAPI.getList().then(function (data) {

            $scope.recordList = data;
        });

        // add new link function
        $scope.addNew = function () {
            $scope.SubmitMessage = 'Adding New ...';
            $location.url('/test-restangular/save/{New}');
        };


        //$scope.applicationModes = Restangular.all('applicationMode').getList();

    });
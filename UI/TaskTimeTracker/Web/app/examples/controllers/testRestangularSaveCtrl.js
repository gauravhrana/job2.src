//'use strict';

angular.module('rootAppShell')
    .controller('testRestangularSaveCtrl', [
        '$location', '$scope', '$routeParams', 'Restangular',
        function ($location, $scope, $routeParams, Restangular) {

            var baseRepository = Restangular.all('BathRooms');

            $scope.item = {
                BathRoomId: $routeParams.detailId,
                Name: '',
                Description: '',
                SortOrder: ''
            };

            $scope.WorkflowStateIsNew = function () {
                return ($routeParams.detailId == '{New}');
            };

            function onFailedLoad(serverResponse) {
                //userService.AlertManager.logFailureAlert('', serverResponse.data, []);
                alert(serverResponse.data);
                debugger
            }

            function onSuccessLoad(data) {
                $scope.item = data;
                $scope.item.BathRoomId = $routeParams.detailId;
            }

            // init
            if (!$scope.WorkflowStateIsNew()) {
                
                //$scope.item = Restangular.one('BathRooms', $routeParams.detailId).get().$object
                Restangular.one('BathRooms', $routeParams.detailId).get().then(function (serverData) {

                    $scope.item = serverData;
                });
            }

            $scope.save = function () {

                var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';
                if (saveMethod == 'create') {
                    baseRepository.post($scope.item);
                }
                else {
                    $scope.item.put();

                    //$scope.item.put("BathRooms", $routeParams.detailId);
                }

                $location.url('/test-restangular/search');

                //$scope.item
            };

            $scope.delete = function () {
                debugger

                $scope.item.remove().then(function (serverData) {
                    $location.url('/test-restangular/search');
                }, function errorCallback(errorResponse) {
                    debugger
                    alert("Oops error from server :(");
                });

            };

        }
    ]);

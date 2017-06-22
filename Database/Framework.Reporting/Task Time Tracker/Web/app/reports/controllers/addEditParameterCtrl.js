'use strict';

angular.module('rootAppShell')
    .controller('addEditParameterCtrl', [
        '$scope', '$modalInstance', 'modalInfo', 'parameter', 'datasets', function($scope, $modalInstance, modalInfo, parameter, datasets) {
            $scope.modalInfo = modalInfo;
            $scope.parameter = parameter;
            $scope.datasets = datasets;

            $scope.ok = function() {
                $modalInstance.close(parameter);
            };

            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            };

            $scope.delete = function () {
                $modalInstance.dismiss('delete');
            };

        }
    ]);
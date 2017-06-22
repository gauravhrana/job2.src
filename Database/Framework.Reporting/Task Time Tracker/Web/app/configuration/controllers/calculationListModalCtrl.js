'use strict';

angular.module('rootAppShell')
    .controller('calculationListModalCtrl', [
        '$scope', '$modalInstance', 'selectedElements', 'hideOrSelect', 'title', 'calculationService', 'calculationSelectionService', '$log', 'userService',
        
        function ($scope, $modalInstance, selectedElements, hideOrSelect, title, calculationService, calculationSelectionService, $log, userService) {

            $scope.title = "Calculation Selection";

            $scope.SelectedItems = [];

            if (title != undefined && title.length > 0) {
                $scope.title = title;
            }
            
            function onFailedLoad(serverResponse) {
                userService.AlertManager.logFailureAlert('', serverResponse.data, []);
            }
            
            function onSuccessLoad(data) {
                
                $scope.calculations = data;

                $scope.UniqueCategories = calculationSelectionService.getUniqueTags($scope.calculations);                
                $scope.Grouped = calculationSelectionService.groupByCategory($scope.UniqueCategories, $scope.calculations, selectedElements, hideOrSelect, 3);
                
                $scope.SelectedItems = $scope.Grouped;
            }
            
            $scope.AddCalcOk = function () {
                
                var items = calculationSelectionService.getSelectedItems($scope.Grouped);
                
                $modalInstance.close(items);
            };

            $scope.filterSelectedItem = function (item) {                
                return item.Selected;
            };

            $scope.AddCalcCancel = function () {
                $modalInstance.dismiss('cancel');
            };
            
            calculationService.getList(null, onSuccessLoad, onFailedLoad);
        }
    ]);
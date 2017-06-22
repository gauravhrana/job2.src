'use strict';

angular.module('rootAppShell')
    .controller('calculationListCtrl', [
        '$scope', '$location', 'calculationService', 'calculationSelectionService', 'userService', 'cachingService',
        
        function ($scope, $location, calculationService, calculationSelectionService, userService, cachingService) {
                                    
            $scope.UniqueCategories = [];
            $scope.UniqueCategoriesChunked = [];
            $scope.errors = [];
            $scope.CanSave = true;
            $scope.SubmitMessage = 'Add New';
            $scope.Grouped = [];
            $scope.searchText = cachingService.get('CalcualtionSearch');
            
            $scope.$watch('searchText', function (newVal, oldVal, scope) {
                cachingService.set('CalcualtionSearch', newVal);                
            });
                        
            function onFailedLoad(serverResponse) {
                userService.AlertManager.logFailureAlert('', serverResponse.data, []);
            }
            
            function onSuccessLoad(data) {
                
                $scope.calculations = data;
           
                $scope.UniqueCategories = calculationSelectionService.getUniqueTags($scope.calculations);                                
                $scope.Grouped = calculationSelectionService.groupByCategory($scope.UniqueCategories, $scope.calculations, [], null, 4);
                
            }                        
            
            $scope.AddNew = function () {

                $scope.SubmitMessage = 'Adding New ...';

                $location.url('/calculations/{New}');
                                
            };
            
            calculationService.getList(null, onSuccessLoad, onFailedLoad);
        }

    ]);
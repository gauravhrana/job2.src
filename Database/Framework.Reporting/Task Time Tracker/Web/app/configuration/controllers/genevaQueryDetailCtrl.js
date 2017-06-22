'use strict';

angular.module('rootAppShell')
    .controller('genevaQueryDetailCtrl', [
        '$scope', '$routeParams', 'userService', 'genevaQueryService',
        function ($scope, $routeParams, userService, genevaQueryService) {

            $scope.isSelectCollapsed = true;
            $scope.isFromCollapsed = true;
            $scope.isGivenCollapsed = true;
            $scope.isDisaggregateCollapsed = true;
            $scope.isWhereCollapsed = true;

            $scope.query = {
                GenevaQueryCode: '',
                FromEntity: '',
                DisaggregateClause: '',
                WhereClause: '',
                GenevaQueryColumns: [],
                GenevaQueryGivens: [],
                DetailCode: ''
            };


            var code = $routeParams.genevaQueryCode;
            if (code != '{New}') {
                $scope.query = genevaQueryService.getByCode({ genevaQueryCode: code }, function() {

                }, function(errorResponse) {
                    userService.AlertManager.addFailureResponse(errorResponse);
                });
            }

            $scope.removeColumn = function(column) {
                var index = $scope.query.GenevaQueryColumns.indexOf(column);
                if (index >= 0) {
                    $scope.query.GenevaQueryColumns.splice(index, 1);
                }
            };

            $scope.addColumn = function() {
                $scope.query.GenevaQueryColumns.push({
                    GenevaQueryColumnCode: '',
                    Formula: '',
                    GroupBy: false,
                    ColumnOrder: 100
                });
            };

            $scope.removeGiven = function (given) {
                var index = $scope.query.GenevaQueryGivens.indexOf(given);
                if (index >= 0) {
                    $scope.query.GenevaQueryGivens.splice(index, 1);
                }
            };

            $scope.addGiven = function () {
                $scope.query.GenevaQueryGivens.push({
                    GenevaQueryGivenCode: '',
                    GivenValue: ''
                });
            };
        }
    ]);

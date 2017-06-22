'use strict';

angular.module('rootAppShell')
    .controller('deployScriptsCtrl', [
        '$location', '$scope', '$injector', '$routeParams', '$modal', 'userService', 'deployScriptsService', '$timeout', '$log',
        function ($location, $scope, $injector, $routeParams, $modal, userService, deployScriptsService, $timeout, $log) {

            $scope.showTabs = false;

            // load entity drop down
            deployScriptsService.listEntities(null, onSuccessLoadEntities, onFailedLoad);

            function onFailedLoad(serverResponse) {
                userService.AlertManager.logFailureAlert('', serverResponse.data, []);
            }

            function onSuccessLoadEntities(data) {
                $scope.systemEntityTypes = data;
                $scope.selectedEntity = data[0];
            }

            // Ace Editor Configuration
            $scope.loadAceEditor = function (_editor) {
                // Editor part
                var _session = _editor.getSession();
                var _renderer = _editor.renderer;

                // Options
                _editor.setReadOnly(true);
                _editor.setTheme("ace/theme/twilight");
                _editor.session.setMode("ace/mode/sql");
                _editor.setAutoScrollEditorIntoView(false);
                _editor.setShowPrintMargin(false);       // disable vertical line   
                _renderer.setShowGutter(false);
            };

            $scope.generateProcedures = function () {
                deployScriptsService.getProcedureText(
                    {
                        value: $scope.selectedEntity.EntityName
                    }, onSuccessLoadProcedureText, onFailedLoad);
            }

            function onSuccessLoadProcedureText(data) {

                if (data.length > 0) {

                    $scope.showTabs = true;
                    $scope.insertProcedureText = data[0];
                    $scope.updateProcedureText = data[1];
                    $scope.deleteProcedureText = data[2];
                    $scope.searchProcedureText = data[3];
                }                
            }

            $scope.deployProcedures = function (procedureType) {
                deployScriptsService.deployProcedureText(
                    {
                        value: procedureType,
                        entityName: $scope.selectedEntity.EntityName
                }, onSuccessDeployProcedureText, onFailedLoad);
            }

            function onSuccessDeployProcedureText(data) {
                alert("Deployment Status: true");
            }

        }
    ]);
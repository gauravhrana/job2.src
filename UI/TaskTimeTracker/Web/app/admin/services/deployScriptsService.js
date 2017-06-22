'use strict';

angular.module('rootAppShell')
    .factory('deployScriptsService', [
        '$resource',
        function ($resource) {
            return $resource('./apiV2/DeployScripts/:detailId'
                ,   null
                ,   {
                          'listEntities': { method: 'GET', url: '../../apiV2/DeployScripts/ListEntities', isArray: true }
                        , 'getProcedureText': { method: 'GET', url: '../../apiV2/DeployScripts/GetProcedureText', isArray: true }
                        , 'deployProcedureText': { method: 'GET', url: '../../apiV2/DeployScripts/DeployProcedureText/:value/:entityName' }
                    }
                );
        }
    ]);


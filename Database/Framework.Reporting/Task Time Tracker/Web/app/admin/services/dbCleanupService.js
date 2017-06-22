'use strict';

angular.module('rootAppShell')
    .factory('dbCleanupService', [
        '$resource',
        function ($resource) {
            return $resource('./api/databaseCleanup/:detailId'
                ,   null
                ,   {
                          'listDatabase': { method: 'GET', url: '../../apiV2/databaseCleanup/ListDatabaseNames', isArray: true }
                        , 'getDatabaseObjects': { method: 'GET', url: '../../apiV2/databaseCleanup/GetDatabaseObjects/:value1/:value2/:value3', isArray: true }
                        , 'getDatabaseObjectText': { method: 'GET', url: '../../apiV2/databaseCleanup/GetDatabaseObjectText/:value1/:value2/:value3', isArray: true }
                        , 'dropDatabaseObject': { method: 'GET', url: '../../apiV2/databaseCleanup/DropDatabaseObject/:value1/:value2/:value3' }
                    }
                );
        }
    ]);


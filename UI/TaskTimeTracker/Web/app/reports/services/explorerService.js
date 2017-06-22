'use strict';

angular.module('rootAppShell')
    .factory('explorerService', [
        '$resource',
        function($resource) {
            return $resource('./api/explorer', null, {
                'getEntityList': { method: 'GET', url: './api/explorer/GetEntityList', isArray: true },
                'getQuery': { method: 'GET', url: './api/explorer/GetQuery/:explorerQueryId' },
                    'saveQuery': { method: 'POST', url: './api/explorer/SaveQuery' }
                }
            );
        }
    ]);
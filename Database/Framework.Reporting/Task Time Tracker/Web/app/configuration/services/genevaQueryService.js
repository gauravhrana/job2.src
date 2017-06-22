'use strict';

angular.module('rootAppShell')
    .factory('genevaQueryService', [
        '$resource',
        function($resource) {
            return $resource('./api/genevaQuery/:genevaQueryCode', null, {
                    'getByCode': { method: 'GET', url: './api/genevaQuery/GetByCode/:genevaQueryCode' },
                    'getList': { method: 'GET', url: './api/genevaQuery/GetList', isArray: true },
                    'create': { method: 'POST', url: './api/genevaQuery/Create' },
                    'update': { method: 'POST', url: './api/genevaQuery/Update' },
                    'delete': { method: 'DELETE', url: './api/genevaQuery/Delete/:genevaQueryCode' }
                }
            );
        }
    ]);
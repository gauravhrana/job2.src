'use strict';

angular.module('rootAppShell')
    .factory('detailService', [
        '$resource',
        function($resource) {
            return $resource('./api/detail/:detailCode', null, {
                    'getByCode': { method: 'GET', url: './api/detail/GetByCode/:detailCode' },
                    'getList': { method: 'GET', url: './api/detail/GetList', isArray: true },
                    'create': { method: 'POST', url: './api/detail/Create' },
                    'update': { method: 'POST', url: './api/detail/Update' },
                    'delete': { method: 'DELETE', url: './api/detail/Delete/:detailCode' }
                }
            );
        }
    ]);


'use strict';

angular.module('rootAppShell')
    .factory('processInfoService', [
        '$resource',
        function($resource) {
            return $resource('./api/processInfo/', null, {
                    'getList': { method: 'GET', url: './api/processInfo/GetList', isArray: true }
                }
            );
        }
    ]);
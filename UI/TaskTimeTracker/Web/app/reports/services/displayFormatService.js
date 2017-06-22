'use strict';

angular.module('rootAppShell')
    .factory('displayFormatService', [
        '$resource',
        function($resource) {
            return $resource('./api/displayFormat/', null, {
                    'getList': { method: 'GET', url: './api/displayFormat/GetList', isArray: true },
                }
            );
        }
    ]);
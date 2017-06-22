'use strict';

angular.module('rootAppShell')
    .factory('processRequestService', [
        '$resource',
        function($resource) {
            return $resource('./api/processRequest/', null, {
                    'submit': { method: 'POST', url: './api/processRequest/Submit' }
                }
            );
        }
    ]);
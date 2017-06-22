'use strict';

angular.module('rootAppShell')
    .factory('loggingService', [
        '$resource',
        function($resource) {
            return $resource('./api/LoggingService/:calculationCode'
                ,   null                
                ,   {                      
                        'create': { method: 'POST', url: './api/LoggingService/Create' }
                    }
                );
        }
    ]);
'use strict';

angular.module('rootAppShell')
    .factory('referenceDataService', [
        '$resource',
        function ($resource) {
            return $resource('./api/referenceData/:referenceKey'
                , null                
                ,   {
                    'getReferenceList': { method: 'GET', url: './api/referenceData/getReferenceList/:referenceKey', isArray: true }
                    }
                );
        }
    ]);

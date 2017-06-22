'use strict';

angular.module('rootAppShell')
    .factory('menuService', [
        '$resource',
        function ($resource) {
            return $resource('./apiV2/Menu/:detailId'
                , null
                , {
                        'getUPMenu': { method: 'GET', url: '../../apiV2/Menu/GetUserPreferenceMenu', isArray: true }
                    ,   'getApplicationCode': { method: 'GET', url: '../../apiV2/Menu/GetApplicationCode' }
                    ,   'setApplicationModule': { method: 'GET', url: '../../apiV2/Menu/SetApplicationModule/:value' }
                }
                );
        }
    ]);


'use strict';

angular.module('rootAppShell')
    .factory('applicationModeService', [
        '$resource',
        function ($resource) {
            return $resource('./api/applicationMode/:detailId'
                , null
                , {
                            'getById': { method: 'GET', url: '../../apiV2/applicationMode/GetById/:detailId' }                       
                        ,   'getList': { method: 'GET', url: '../../apiV2/applicationMode/GetList', isArray: true }
                        ,   'create': { method: 'POST', url: '../../apiV2/applicationMode/Create' }
                        ,   'update': { method: 'POST', url: '../../apiV2/applicationMode/Update' }
                        ,   'delete': { method: 'DELETE', url: '../../apiV2/applicationMode/Delete/:detailId' }
                }
                );
        }
    ]);


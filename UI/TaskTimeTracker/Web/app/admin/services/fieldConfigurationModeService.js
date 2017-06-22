'use strict';

angular.module('rootAppShell')
    .factory('fieldConfigurationModeService', [
        '$resource',
        function ($resource) {
            return $resource('./api/fieldConfigurationMode/:detailId'
                , null
                , {
                            'getById': { method: 'GET', url: '../../apiV2/fieldConfigurationMode/GetById/:detailId' }
                        ,   'getList': { method: 'GET', url: '../../apiV2/fieldConfigurationMode/GetList', isArray: true }
                        ,   'create': { method: 'POST', url: '../../apiV2/fieldConfigurationMode/Create' }
                        ,   'update': { method: 'POST', url: '../../apiV2/fieldConfigurationMode/Update' }
                        ,   'delete': { method: 'DELETE', url: '../../apiV2/fieldConfigurationMode/Delete/:detailId' }
                }
                );
        }
    ]);


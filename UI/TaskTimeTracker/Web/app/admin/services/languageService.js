        'use strict';

angular.module('rootAppShell')
    .factory('languageService', [
        '$resource',
        function ($resource) {
            return $resource('./api/language/:detailId'
                , null
                , {
                            'getById': { method: 'GET', url: '../../apiV2/language/GetById/:detailId' }
                        ,   'getList': { method: 'GET', url: '../../apiV2/language/GetList', isArray: true }
                        ,   'create': { method: 'POST', url: '../../apiV2/language/Create' }
                        ,   'update': { method: 'POST', url: '../../apiV2/language/Update' }
                        ,   'delete': { method: 'DELETE', url: '../../apiV2/language/Delete/:detailId' }
                }
                );
        }
    ]);


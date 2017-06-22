'use strict';

angular.module('rootAppShell')
    .factory('menuCategoryService', [
        '$resource',
        function ($resource) {
            return $resource('./api/menuCategory/:detailId'
                , null
                , {
                            'getById': { method: 'GET', url: '../../apiV2/menuCategory/GetById/:detailId' }
                        ,   'getList': { method: 'GET', url: '../../apiV2/menuCategory/GetList/:value/:value1', isArray: true }
                        ,   'create': { method: 'POST', url: '../../apiV2/menuCategory/Create' }
                        ,   'update': { method: 'POST', url: '../../apiV2/menuCategory/Update' }
                        ,   'delete': { method: 'DELETE', url: '../../apiV2/menuCategory/Delete/:detailId' }
                }
                );
        }
    ]);


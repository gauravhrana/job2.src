'use strict';

angular.module('rootAppShell')
    .factory('applicationRoleService', [
        '$resource',
        function ($resource) {
            return $resource('./api/applicationRole/:detailId'
                , null
                , {
						'getById': { method: 'GET', url: '../../apiV2/applicationRole/GetById/:detailId' }
                        , 'getList': { method: 'GET', url: '../../apiV2/applicationRole/GetList', isArray: true }
                        , 'create': { method: 'POST', url: '../../apiV2/applicationRole/Create' }
                        , 'update': { method: 'POST', url: '../../apiV2/applicationRole/Update' }
                        , 'delete': { method: 'DELETE', url: '../../apiV2/applicationRole/Delete/:detailId' }
                }
                );
        }
    ]);


'use strict';

angular.module('rootAppShell')
    .factory('scheduleStateService', [
        '$resource',
        function($resource) {
        	return $resource('./api/scheduleState/:detailId'
                ,   null
                ,   {
                            'getById': { method: 'GET', url: '../../apiV2/scheduleState/GetById/:detailId' }
                        , 'getByCode': { method: 'GET', url: '../../apiV2/scheduleState/GetByCode/:detailId' }
                        , 'getByCodeWithDependencyDetail': { method: 'GET', url: '../../apiV2/scheduleState/GetByCodeWithDependencyDetail/:detailId' }
                        ,   'getList': { method: 'GET', url: '../../apiV2/scheduleState/GetList', isArray: true }
                        ,   'create': { method: 'POST', url: '../../apiV2/scheduleState/Create' }
                        ,   'update': { method: 'POST', url: '../../apiV2/scheduleState/Update' }
                        , 'delete': { method: 'GET', url: '../../apiV2/scheduleState/Delete/:detailId' }
                    }
                );
        }
    ]);


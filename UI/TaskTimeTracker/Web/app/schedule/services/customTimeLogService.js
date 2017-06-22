'use strict';

angular.module('rootAppShell')
    .factory('customTimeLogService', [
        '$resource',
        function ($resource) {
            return $resource('./api/customTimeLog/:detailId'
                , null
                , {
                        'getById': { method: 'GET', url: '../../apiV2/customTimeLog/GetById/:detailId' }
                    ,   'getList': { method: 'GET', url: '../../apiV2/customTimeLog/GetList', isArray: true }
                    ,   'create': { method: 'POST', url: '../../apiV2/customTimeLog/Create' }
                    ,   'update': { method: 'POST', url: '../../apiV2/customTimeLog/Update' }
                    ,   'delete': { method: 'DELETE', url: '../../apiV2/customTimeLog/Delete/:detailId' }
                }
                );
        }
    ]);

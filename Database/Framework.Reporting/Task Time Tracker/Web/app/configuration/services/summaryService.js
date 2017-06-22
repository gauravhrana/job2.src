'use strict';

angular.module('rootAppShell')
    .factory('summaryService', [
        '$resource',
        function($resource) {
            return $resource('./api/summary/:summaryCode'
                ,   null
                ,   {
                          'getByCode': { method: 'GET', url: './api/summary/GetByCode/:summaryCode' }
                        , 'getByCodeWithDependencyDetail': { method: 'GET', url: './api/summary/GetByCodeWithDependencyDetail/:summaryCode' }
                        , 'getList': { method: 'GET', url: './api/summary/GetList', isArray: true }
                        , 'create': { method: 'POST', url: './api/summary/Create' }
                        , 'update': { method: 'POST', url: './api/summary/Update' }
                        , 'delete': { method: 'GET', url: './api/summary/Delete/:summaryCode' }
                    }
                );
        }
    ]);


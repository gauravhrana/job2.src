'use strict';

angular.module('rootAppShell')
    .factory('calculationService', [
        '$resource',
        function($resource) {
            return $resource('./api/calculation/:calculationCode'
                ,   null                
                , {
                      'getByCode': { method: 'GET', url: './api/calculation/GetByCode/:calculationCode' }
                    , 'getByCodeWithDependencyDetail': { method: 'GET', url: './api/calculation/GetByCodeWithDependencyDetail/:calculationCode' }
                    , 'getCategoryList': { method: 'GET', url: './api/calculation/GetCategoryList', isArray: true }
                    , 'getList': { method: 'GET', url: './api/calculation/GetList', isArray: true }
                    , 'create': { method: 'POST', url: './api/calculation/Create' }
                    , 'update': { method: 'POST', url: './api/calculation/Update' }                    
                    , 'delete': { method: 'GET', url: './api/calculation/Delete/:calculationCode'}
                    }
                );
        }
    ]);
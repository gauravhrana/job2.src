'use strict';

angular.module('rootAppShell')
    .factory('questionService', [
        '$resource',
        function ($resource) {
            return $resource('./api/question/:detailId'
                , null
                , {
                            'getById': { method: 'GET', url: '../../apiV2/question/GetById/:detailId' }
                        ,   'getList': { method: 'GET', url: '../../apiV2/question/GetList/:value/:value1', isArray: true }
                }
            );
        }
    ]);


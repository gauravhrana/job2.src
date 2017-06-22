'use strict';

angular.module('rootAppShell')
    .factory('processRunInfoService', [
        '$resource',
        function($resource) {
            return $resource('./api/processRunInfo/', { processRunId: '@processRunId' }, {
                    'getList': { method: 'GET', url: './api/processRunInfo/GetList', isArray: true },
                    'getDetails': { method: 'GET', url: './api/processRunInfo/GetDetails/:processRunId' },
                    'getActions': { method: 'GET', url: './api/processRunInfo/GetActions/:processRunId', isArray: true },
                    'getSummaryCalculateInfo': { method: 'GET', url: './api/summary/GetSummaryCalculateInfo/:processRunId', isArray: true },
                }
            );
        }
    ]);
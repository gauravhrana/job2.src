'use strict';

angular.module('rootAppShell')
    .factory('reportService', [
        '$resource',
        function($resource) {
            return $resource('./api/reports/', null, {
                    'getList': { method: 'GET', url: './api/reports/GetList', isArray: true },
                    'requestData': { method: 'POST', url: './api/reports/RequestData', isArray: true },
                    'requestSummaryData': { method: 'POST', url: './api/reports/RequestSummaryData' }
                }
            );
        }
    ]);
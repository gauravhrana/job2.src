'use strict';

angular.module('rootAppShell')
    .factory('elapsedTimeService', [
        '$resource',
        function ($resource) {
            return $resource('./apiV2/ElapsedTimeData/:detailId'
                , null
                , {
                      'listUsers': { method: 'GET', url: '../../apiV2/ElapsedTimeData/ListUsers', isArray: true }
                    , 'getElapsedTimeData': { method: 'GET', url: '../../apiV2/ElapsedTimeData/GetElapsedTimeData/:value1/:value2/:value3', isArray: true }
                    , 'getStoredSearchData': { method: 'GET', url: '../../apiV2/ElapsedTimeData/GetStoredSearchData/:value1/:value2/:value3' }
                    , 'setStoredSearchData': { method: 'GET', url: '../../apiV2/ElapsedTimeData/SetStoredSearchData/:value/:value1' }
                }
                );
        }
    ]);

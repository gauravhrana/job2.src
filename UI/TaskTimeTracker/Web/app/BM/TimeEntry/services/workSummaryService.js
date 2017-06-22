'use strict';

angular.module('rootAppShell')
    .factory('workSummaryService', [
        '$resource',
        function ($resource) {
            return $resource('./apiV2/WorkSummary/:detailId'
                , null
                , {
                    'listUsers': { method: 'GET', url: '../../apiV2/WorkSummary/ListUsers', isArray: true }
                        , 'getWorkCategoryReportData': { method: 'GET', url: '../../apiV2/WorkSummary/GetWorkCategoryReportData/:value1/:value2/:value3', isArray: true }
                        , 'getBranchSummaryReportData': { method: 'GET', url: '../../apiV2/WorkSummary/GetBranchSummaryReportData/:value1/:value2/:value3', isArray: true }
                    , 'getStoredSearchData': { method: 'GET', url: '../../apiV2/WorkSummary/GetStoredSearchData/:value1/:value2/:value3' }
                    , 'setStoredSearchData': { method: 'GET', url: '../../apiV2/WorkSummary/SetStoredSearchData/:value/:value1' }
                }
                );
        }
    ]);


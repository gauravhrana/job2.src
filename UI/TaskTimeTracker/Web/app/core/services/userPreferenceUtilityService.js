'use strict';

angular.module('rootAppShell')
    .factory('userPreferenceUtilityService', [
        '$resource',
        function ($resource) {
            return $resource('./apiV2/UserPreferenceUtility/:detailId'
                , null
                , {
                        'getUPData': { method: 'GET', url: '../../apiV2/UserPreferenceUtility/GetUPData/:value/:value1' }
                    ,   'setUPData': { method: 'GET', url: '../../apiV2/UserPreferenceUtility/SetUPData/:value1/:value2/:value3' }
                    ,   'getUserDateFormat': { method: 'GET', url: '../../apiV2/UserPreferenceUtility/GetUserDateFormat' }
                }
                );
        }
    ]);


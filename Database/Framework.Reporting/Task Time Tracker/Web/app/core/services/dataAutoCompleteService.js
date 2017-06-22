'use strict';

angular.module('rootAppShell')
    .factory('dataAutoCompleteService', [
        '$resource',
        function ($resource) {
            return $resource('./api/AutoComplete/:detailId'
                , null
                , {
                          'getById': { method: 'GET', url: '../../apiV2/AutoComplete/GetAutoCompleteList/:detailId' }
                        , 'getKendoComboSource': { method: 'GET', url: '../../apiV2/AutoComplete/:methodName', isArray: true }
                        , 'getAutoCompleteList': { method: 'GET', url: '../../apiV2/AutoComplete/GetAutoCompleteList/:value/:value1', isArray: true }
                        , 'getGroupByList': { method: 'GET', url: '../../apiV2/AutoComplete/GetGroupByList/:value', isArray: true }
                        , 'getComboSource': { method: 'GET', url: '../../apiV2/AutoComplete/:methodName', isArray: true }
                }
                );
        }
    ]);


'use strict';

angular.module('rootAppShell')
    .factory('fieldConfigurationService', [
        '$resource',
        function ($resource) {
            return $resource('./apiV2/FieldConfiguration/:detailId'
                ,   null
                ,   {
                          'getFCModes': { method: 'GET', url: '../../apiV2/FieldConfiguration/GetFCModes/:value', isArray: true }
                        , 'getUserFieldConfigurationMode': { method: 'GET', url: '../../apiV2/FieldConfiguration/GetUserFieldConfigurationMode/:value' }
                        , 'getSearchFilterColumns': { method: 'GET', url: '../../apiV2/FieldConfiguration/GetSearchFilterColumns/:value/:value1', isArray: true }
                        , 'resetSearchFilterColumns': { method: 'GET', url: '../../apiV2/FieldConfiguration/ResetSearchFilterColumns/:value/:value1', isArray: true }
                        , 'getFieldConfigurations': { method: 'GET', url: '../../apiV2/FieldConfiguration/GetFieldConfigurations/:value/:value1', isArray: true }
                        , 'updateUserFieldConfigurationMode': { method: 'GET', url: '../../apiV2/FieldConfiguration/UpdateUserFieldConfigurationMode/:value/:value1' }
                    }
                );
        }
    ]);


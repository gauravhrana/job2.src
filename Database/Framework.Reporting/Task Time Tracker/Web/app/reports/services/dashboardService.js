'use strict';

angular.module('rootAppShell')
    .factory('dashboardService', [
        '$resource',
        function($resource) {
            return $resource('./api/dashboard/', null, {
                    'getList': { method: 'GET', url: './api/dashboard/GetList', isArray: true },
                    'getByCode': { method: 'GET', url: './api/dashboard/GetByCode/:dashboardCode' },
                    'getDatasetResults': { method: 'POST', url: './api/dashboard/GetDatasetResults', isArray: true },
                    'saveDashboard': { method: 'POST', url: './api/dashboard/SaveDashboard' },
                    'deleteDashboard': { method: 'DELETE', url: './api/dashboard/DeleteDashboard/:dashboardCode', isArray: true },                    
                }
            );
        }
    ]);

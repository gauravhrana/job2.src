'use strict';
angular.module('rootAppShell')
    .factory('dashboardState', function() {
        var dashboard = {};
        var dashboardStateService = {};

        dashboardStateService.setState = function(state) {
            dashboard = state;
        };
        dashboardStateService.getState = function() {
            return dashboard;
        };

        return dashboardStateService;
    });

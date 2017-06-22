'use strict';

angular.module('rootAppShell')
    .controller('dashboardListCtrl', [
        '$scope', '$routeParams', '$modal', 'dashboard', 'dashboardService', 'dashboardState', 'userService', function($scope, $routeParams, $modal, dashboard, dashboardService, dashboardState, userService) {            
            $scope.dashboards = [];
            $scope.dashboards = dashboardService.getList();
        }
    ]);

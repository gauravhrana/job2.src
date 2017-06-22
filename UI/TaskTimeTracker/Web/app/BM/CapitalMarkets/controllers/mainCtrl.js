'use strict';

angular.module('rootAppShell')
    .controller('mainCMCtrl', [
		'$scope', 'localStorageService',
        function ($scope, localStorageService) {

            var model = localStorageService.get('widgetSampleDashboard');
            if (!model) {
                model = {
                    rows: [{
                        columns: [{
                            styleClass: 'col-md-4',
                            widgets: []
                        }, {
                            styleClass: 'col-md-8',
                            widgets: [{
                                type: 'clock',
                                title: 'Clock',
                                config: {}
                            }]
                        }]
                    }]
                };
            }
            $scope.dashboard = {
                model: model
            };
            $scope.$on('adfDashboardChanged', function (event, name, model) {
                localStorageService.set(name, model);
            });
        }
    ]
);


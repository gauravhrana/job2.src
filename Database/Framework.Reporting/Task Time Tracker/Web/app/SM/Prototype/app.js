'use strict';

var app = angular.module('rootAppShell', [
        'ngResource',
        'ngSanitize',
        'ngRoute',
		'ngCookies',
        'ui.select2',
        //'ngGrid',
        'ui.bootstrap',
        //'adf',
        //'adf-structures',
        'btford.markdown',
        //'highcharts-ng',
        //'mgcrea.ngStrap.datepicker',
        'ngNumeraljs',
        'restangular',
        //'sample.widgets.markdown',
        //'reports.widgets.grid',
        //'reports.widgets.table',
        //'reports.widgets.barchart',
        'ui.ace'
		, 'ui.grid', 'ui.grid.cellNav', 'ui.grid.edit', 'ui.grid.resizeColumns', 'ui.grid.pinning', 'ui.grid.selection', 'ui.grid.moveColumns', 'ui.grid.pagination'
		, 'ngTable'
		, 'kendo.directives'
    ])
    .config(function ($httpProvider, $routeProvider, $parseProvider, $logProvider, RestangularProvider) {

        RestangularProvider.setBaseUrl('../../apiV2/rest/');
        RestangularProvider.setDefaultHeaders({ 'Content-Type': 'application/json' });

        $logProvider.debugEnabled(true);

        //$parseProvider.unwrapPromises(true);

        //Route mapping
        $routeProvider

            // core
            .when('/main', { templateUrl: '../../app/BM/Prototype/views/main.html', controller: 'mainCtrl' })
            .when('/login', { templateUrl: '../../app/core/views/login.html', controller: 'loginCtrl' })
            .when('/logout', { templateUrl: '../../app/core/views/logout.html', controller: 'logoutCtrl' })

            //Event Sub Type
            .when('/EventSubType/search', { templateUrl: '../../app/BM/Prototype/views/listTableEventSubType.html', controller: 'listTableEventSubTypeCtrl', caseInsensitiveMatch: true })
            .when('/EventSubType/detail/:detailId', { templateUrl: '../../app/BM/Prototype/views/detailEventSubType.html', controller: 'detailEventSubTypeCtrl', caseInsensitiveMatch: true })
            .when('/EventSubType/save/:detailId', { templateUrl: '../../app/BM/Prototype/views/saveEventSubType.html', controller: 'saveEventSubTypeCtrl', caseInsensitiveMatch: true })

            // help
            .when('/help', { templateUrl: '../../app/help/views/help/help.html' })
            .otherwise({ redirectTo: '/main' });

            //REQUIRED - capture all 401 http response errors
            $httpProvider.interceptors.push(function($rootScope, $q) {
                return {
                    'responseError': function(rejection) {
                        if (rejection.status == 401) {
                            var deferred = $q.defer();
                            $rootScope.$broadcast('auth-loginRequired');
                            return deferred.promise;
                        } else {
                            return $q.reject(rejection);
                        }
                    }
                };
            }
        );

        //REQUIRED - disable ajax request caching. We do not want the browser to cache any results, we want new data every time we request it
        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
    })
    .run(function($rootScope, $location, userService) {

        $rootScope.ShowDetailedSupportInfo = true;

        $rootScope.$on('$routeChangeStart', function () {
            if ($location.path() != '/login') {
                userService.setPreviousRoute(userService.getNextRoute().path);
                userService.setNextRoute($location.path(), 'set next route');
            }
        });

        //REQUIRED - redirect 401 response errors to login
        $rootScope.$on('auth-loginRequired', function() {
            userService.setNextError('You must login to access this page');
            $location.url('login');
        });
    })
    .directive('hboTabs', function () {
        return {
            restrict: 'A',
            link: function (scope, elm, attrs) {
                var jqueryElm = $(elm[0]);
                $(jqueryElm).tabs({
                	activate: function (event, ui) {

                		//get activated tab
                		var procType = ui.newPanel.attr("title");

                		//refreshes ace editor w.o needing to put the cursor
                		ace.edit("editor" + procType + "Procedure").resize();
                	}
                });
            }
        };
    })
    .directive('jqdatepicker', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ctrl) {
                $(element).datepicker({
                    dateFormat: 'dd.mm.yy',
                    onSelect: function (date) {
                        ctrl.$setViewValue(date);
                        ctrl.$render();
                        scope.$apply();
                    }
                });
            }
        };
    })
    .factory('autoCompleteDataService', function () {
        return {
            methodName: 'GetAutoCompleteList',
            getAutoCompleteMethod: function (columnName) {
                var objInfo = {};
                if (columnName == 'EventTypeId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'EventTypeId';
                    objInfo["MethodName"] = 'GetEventTypeList';
                    objInfo["Found"] = true;
                }
                else if (columnName == 'PersonId') {
                    objInfo["DataTextField"] = 'ApplicationUserName';
                    objInfo["DataValueField"] = 'ApplicationUserId';
                    objInfo["MethodName"] = 'GetPersonList';
                    objInfo["Found"] = true;
                }
                else{
                    objInfo["Found"] = false;
                }
                return objInfo;
            }
        };
    });

Date.prototype.chromeDate = function () {
        var yyyy = this.getFullYear().toString();
        var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
        var dd = this.getDate().toString();
        return yyyy + "-" + (mm[1] ? mm : "0" + mm[0]) + "-" + (dd[1] ? dd : "0" + dd[0]); // padding
    };



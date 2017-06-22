'use strict';

var app = angular.module('rootAppShell',
	[
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
        'mgcrea.ngStrap.datepicker',
        'ngNumeraljs',
        //'sample.widgets.markdown',
        //'reports.widgets.grid',
        //'reports.widgets.table',
        //'reports.widgets.barchart',
        'ui.ace',
		, 'ui.grid', 'ui.grid.cellNav', 'ui.grid.edit', 'ui.grid.resizeColumns', 'ui.grid.pinning', 'ui.grid.selection', 'ui.grid.moveColumns', 'ui.grid.pagination'
		, 'ngTable'
		, "kendo.directives"
		, 'ui.tree'
		, 'treeControl'
	])
    .config(function ($httpProvider, $routeProvider, $parseProvider, $logProvider) {

        $logProvider.debugEnabled(true);

        //$parseProvider.unwrapPromises(true);

        //Route mapping
        $routeProvider

            // core
            .when('/main', { templateUrl: '../../app/core/views/main.html', controller: 'mainCtrl' })
            .when('/login', { templateUrl: '../../app/core/views/login.html', controller: 'loginCtrl' })
            .when('/logout', { templateUrl: '../../app/core/views/logout.html', controller: 'logoutCtrl' })

            // reports

            // configuration

            //processing

            //ScheduleStates
            .when('/scheduleStates', { templateUrl: '../../app/schedule/views/scheduleState-list.html', controller: 'scheduleStateListCtrl' })
            .when('/scheduleStateDetail/:detailId', { templateUrl: '../../app/schedule/views/scheduleState-detail.html', controller: 'scheduleStateDetailCtrl' })

            // Utilities
            .when('/db-cleanup', { templateUrl: '../../app/admin/views/db-cleanup.html', controller: 'dbCleanupCtrl' })
            .when('/deploy-scripts', { templateUrl: '../../app/admin/views/deploy-scripts.html', controller: 'deployScriptsCtrl' })

            //// Reports
            //.when('/work-summary', { templateUrl: '../../app/admin/views/work-summary.html', controller: 'workSummaryCtrl' })
            //.when('/elapsed-time-data', { templateUrl: '../../app/admin/views/elapsed-time.html', controller: 'elapsedTimeCtrl' })
            //.when('/jira-list', { templateUrl: '../../app/admin/views/jira-list.html', controller: 'jiraListCtrl' })

			// Demo Items
			.when('/ng-table-demo', { templateUrl: '../../app/BM/Demo/views/general-list-ng-table.html', controller: 'generalListNgTableCtrl', caseInsensitiveMatch: true })
			.when('/ng-grid-demo', { templateUrl: '../../app/BM/Demo/views/general-list-ng-grid.html', controller: 'generalListNgGridCtrl', caseInsensitiveMatch: true })
			.when('/sample-ui-tree', { templateUrl: '../../app/BM/Demo/views/sample-ui-tree.html', controller: 'treeCtrl', caseInsensitiveMatch: true })
			.when('/sample-angular-tree', { templateUrl: '../../app/BM/Demo/views/sample-angular-tree.html', controller: 'treeCtrl2', caseInsensitiveMatch: true })

            // Day Care Items
            .when('/dc-scheduleState-detail', { templateUrl: '../../app/DayCare/views/scheduleState-detail.html', controller: 'scheduleStateDetailCtrl' })
            .when('/dc-scheduleState-list', { templateUrl: '../../app/DayCare/views/scheduleState-list.html', controller: 'scheduleStateListCtrl' })

            //Generic
            .when('/:entityName/search', { templateUrl: '../../app/core/views/general-list.html', controller: 'generalListCtrl', caseInsensitiveMatch: true })
            .when('/:entityName/detail/:detailId', { templateUrl: '../../app/core/views/general-detail.html', controller: 'generalDetailCtrl', caseInsensitiveMatch: true })
            .when('/:entityName/save/:detailId', { templateUrl: '../../app/core/views/general-save.html', controller: 'generalSaveCtrl', caseInsensitiveMatch: true })

            .when('/:entityName/search2', { templateUrl: '../../app/core/views/general-list-table.html', controller: 'generalListTableCtrl', caseInsensitiveMatch: true })

            // help
            .when('/help', { templateUrl: '../../app/help/views/help/help.html' })
            .otherwise({ redirectTo: '/main' });

        //REQUIRED - capture all 401 http response errors
        $httpProvider.interceptors.push(function ($rootScope, $q) {
            return {
                'responseError': function (rejection) {
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
    .run(function ($rootScope, $location, userService) {

        $rootScope.ShowDetailedSupportInfo = true;

        $rootScope.$on('$routeChangeStart', function () {
            if ($location.path() != '/login') {
                userService.setPreviousRoute(userService.getNextRoute().path);
                userService.setNextRoute($location.path(), 'set next route');
            }
        });

        //REQUIRED - redirect 401 response errors to login
        $rootScope.$on('auth-loginRequired', function () {
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
                if (columnName == 'QuestionCategoryId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'QuestionCategoryId';
                    objInfo["MethodName"] = 'GetQuestionCategoryList';
                    objInfo["Found"] = true;
                }
                else if (columnName == 'CustomTimeCategoryId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'CustomTimeCategoryId';
                    objInfo["MethodName"] = 'GetCustomTimeCategoryList';
                    objInfo["Found"] = true;
                }
                else if (columnName == 'PersonId') {
                    objInfo["DataTextField"] = 'ApplicationUserName';
                    objInfo["DataValueField"] = 'ApplicationUserId';
                    objInfo["MethodName"] = 'GetApplicationUserList';
                    objInfo["Found"] = true;
                }
                else {
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



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
        //'sample.widgets.markdown',
        //'reports.widgets.grid',
        //'reports.widgets.table',
        //'reports.widgets.barchart',
        'ui.ace'
		, 'ui.grid', 'ui.grid.cellNav', 'ui.grid.edit', 'ui.grid.resizeColumns', 'ui.grid.pinning', 'ui.grid.selection', 'ui.grid.moveColumns', 'ui.grid.pagination'
		, 'ngTable'
		, 'kendo.directives'
    ])
    .config(function ($httpProvider, $routeProvider, $parseProvider, $logProvider) {

        $logProvider.debugEnabled(true);

        //$parseProvider.unwrapPromises(true);

        //Route mapping
        $routeProvider

            // core
            .when('/main', { templateUrl: '../../app/BM/ReferenceData/views/main.html', controller: 'mainCtrl' })
            .when('/login', { templateUrl: '../../app/core/views/login.html', controller: 'loginCtrl' })
            .when('/logout', { templateUrl: '../../app/core/views/logout.html', controller: 'logoutCtrl' })

            //Continent
            .when('/continent/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableContinent.html', controller: 'listTablecontinentCtrl', caseInsensitiveMatch: true })
            .when('/continent/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailContinent.html', controller: 'detailcontinentCtrl', caseInsensitiveMatch: true })
            .when('/continent/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveContinent.html', controller: 'savecontinentCtrl', caseInsensitiveMatch: true })

            //Country
            .when('/country/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableCountry.html', controller: 'listTablecountryCtrl', caseInsensitiveMatch: true })
            .when('/country/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailCountry.html', controller: 'detailcountryCtrl', caseInsensitiveMatch: true })
            .when('/country/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveCountry.html', controller: 'savecountryCtrl', caseInsensitiveMatch: true })

            //Currency
            .when('/currency/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableCurrency.html', controller: 'listTablecurrencyCtrl', caseInsensitiveMatch: true })
            .when('/currency/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailCurrency.html', controller: 'detailcurrencyCtrl', caseInsensitiveMatch: true })
            .when('/currency/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveCurrency.html', controller: 'savecurrencyCtrl', caseInsensitiveMatch: true })

             //GeographicRegion
            .when('/geographicRegion/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableGeographicRegion.html', controller: 'listTablegeographicRegionCtrl', caseInsensitiveMatch: true })
            .when('/geographicRegion/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailGeographicRegion.html', controller: 'detailgeographicRegionCtrl', caseInsensitiveMatch: true })
            .when('/geographicRegion/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveGeographicRegion.html', controller: 'savegeographicRegionCtrl', caseInsensitiveMatch: true })

            //Religion
            .when('/religion/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableReligion.html', controller: 'listTablereligionCtrl', caseInsensitiveMatch: true })
            .when('/religion/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailReligion.html', controller: 'detailreligionCtrl', caseInsensitiveMatch: true })
            .when('/religion/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveReligion.html', controller: 'savereligionCtrl', caseInsensitiveMatch: true })
            
            //Airport
            .when('/airport/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableAirport.html', controller: 'listTableairportCtrl', caseInsensitiveMatch: true })
            .when('/airport/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailAirport.html', controller: 'detailairportCtrl', caseInsensitiveMatch: true })
            .when('/airport/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveAirport.html', controller: 'saveairportCtrl', caseInsensitiveMatch: true })

            //City
            .when('/city/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableCity.html', controller: 'listTablecityCtrl', caseInsensitiveMatch: true })
            .when('/city/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailCity.html', controller: 'detailcityCtrl', caseInsensitiveMatch: true })
            .when('/city/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveCity.html', controller: 'savecityCtrl', caseInsensitiveMatch: true })

             //HelpLine
            .when('/helpLine/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableHelpLine.html', controller: 'listTablehelpLineCtrl', caseInsensitiveMatch: true })
            .when('/helpLine/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailHelpLine.html', controller: 'detailhelpLineCtrl', caseInsensitiveMatch: true })
            .when('/helpLine/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveHelpLine.html', controller: 'savehelpLineCtrl', caseInsensitiveMatch: true })

            //Mall
            .when('/mall/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableMall.html', controller: 'listTablemallCtrl', caseInsensitiveMatch: true })
            .when('/mall/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailMall.html', controller: 'detailmallCtrl', caseInsensitiveMatch: true })
            .when('/mall/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveMall.html', controller: 'savemallCtrl', caseInsensitiveMatch: true })

             //Monument
            .when('/monument/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableMonument.html', controller: 'listTablemonumentCtrl', caseInsensitiveMatch: true })
            .when('/monument/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailMonument.html', controller: 'detailmonumentCtrl', caseInsensitiveMatch: true })
            .when('/monument/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveMonument.html', controller: 'savemonumentCtrl', caseInsensitiveMatch: true })

            //Region
            .when('/region/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableRegion.html', controller: 'listTableregionCtrl', caseInsensitiveMatch: true })
            .when('/region/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailRegion.html', controller: 'detailregionCtrl', caseInsensitiveMatch: true })
            .when('/region/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveRegion.html', controller: 'saveregionCtrl', caseInsensitiveMatch: true })

            //State
            .when('/state/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableState.html', controller: 'listTablestateCtrl', caseInsensitiveMatch: true })
            .when('/state/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailState.html', controller: 'detailstateCtrl', caseInsensitiveMatch: true })
            .when('/state/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveState.html', controller: 'savestateCtrl', caseInsensitiveMatch: true })

            //TimeZone
            .when('/timeZone/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableTimeZone.html', controller: 'listTabletimeZoneCtrl', caseInsensitiveMatch: true })
            .when('/timeZone/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailTimeZone.html', controller: 'detailtimeZoneCtrl', caseInsensitiveMatch: true })
            .when('/timeZone/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveTimeZone.html', controller: 'savetimeZoneCtrl', caseInsensitiveMatch: true })

            //TrainStation
            .when('/trainStation/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableTrainStation.html', controller: 'listTabletrainStationCtrl', caseInsensitiveMatch: true })
            .when('/trainStation/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailTrainStation.html', controller: 'detailtrainStationCtrl', caseInsensitiveMatch: true })
            .when('/trainStation/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveTrainStation.html', controller: 'savetrainStationCtrl', caseInsensitiveMatch: true })

            //Calendar
            .when('/calendar/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableCalendar.html', controller: 'listTablecalendarCtrl', caseInsensitiveMatch: true })
            .when('/calendar/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailCalendar.html', controller: 'detailcalendarCtrl', caseInsensitiveMatch: true })
            .when('/calendar/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveCalendar.html', controller: 'savecalendarCtrl', caseInsensitiveMatch: true })

            //Holiday
            .when('/holiday/search', { templateUrl: '../../app/BM/ReferenceData/views/listTableHoliday.html', controller: 'listTableholidayCtrl', caseInsensitiveMatch: true })
            .when('/holiday/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/detailHoliday.html', controller: 'detailholidayCtrl', caseInsensitiveMatch: true })
            .when('/holiday/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/saveHoliday.html', controller: 'saveholidayCtrl', caseInsensitiveMatch: true })


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

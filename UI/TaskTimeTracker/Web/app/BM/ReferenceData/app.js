'use strict';

var app = angular.module('rootAppShell', [
          'ngResource'
        , 'ngSanitize'
        , 'ngRoute'
		, 'ngCookies'
        , 'ngTouch'
        , 'ui.select2'
        , 'ui.bootstrap'
        , 'btford.markdown'
        , 'ngNumeraljs'
        , 'restangular'
		, 'kendo.directives'
        , 'ui.ace'
		, 'ui.grid', 'cgBusy', 'ui.grid.cellNav', 'ui.grid.edit', 'ui.grid.resizeColumns', 'ui.grid.exporter', 'ui.grid.pinning', 'ui.grid.selection', 'ui.grid.moveColumns', 'ui.grid.pagination'
		, 'ngTable'
    ])
    .config(function ($httpProvider, $routeProvider, $parseProvider, $logProvider, RestangularProvider) {

        RestangularProvider.setBaseUrl('../../apiV2/rest/');
        RestangularProvider.setDefaultHeaders({ 'Content-Type': 'application/json' });

        $logProvider.debugEnabled(true);

        //$parseProvider.unwrapPromises(true);

        //Route mapping
        $routeProvider

            // core
            .when('/main', { templateUrl: '../../app/BM/ReferenceData/views/main.html', controller: 'mainCtrl' })
            .when('/login', { templateUrl: '../../app/core/views/login.html', controller: 'loginCtrl' })
            .when('/logout', { templateUrl: '../../app/core/views/logout.html', controller: 'logoutCtrl' })

            .when('/test-restangular/search', { templateUrl: '../../app/examples/views/testRestangular.html', controller: 'testRestangularCtrl', caseInsensitiveMatch: true })
            .when('/test-restangular/save/:detailId', { templateUrl: '../../app/examples/views/testRestangularSave.html', controller: 'testRestangularSaveCtrl', caseInsensitiveMatch: true })

            //Continent
            .when('/continent/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableContinent.html', controller: 'listTablecontinentCtrl', caseInsensitiveMatch: true })
            .when('/continent/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailContinent.html', controller: 'detailcontinentCtrl', caseInsensitiveMatch: true })
            .when('/continent/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveContinent.html', controller: 'saveContinentCtrl', caseInsensitiveMatch: true })

            //Country
            .when('/country/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableCountry.html', controller: 'listTablecountryCtrl', caseInsensitiveMatch: true })
            .when('/country/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailCountry.html', controller: 'detailcountryCtrl', caseInsensitiveMatch: true })
            .when('/country/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveCountry.html', controller: 'saveCountryCtrl', caseInsensitiveMatch: true })

            //Currency
            .when('/currency/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableCurrency.html', controller: 'listTablecurrencyCtrl', caseInsensitiveMatch: true })
            .when('/currency/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailCurrency.html', controller: 'detailcurrencyCtrl', caseInsensitiveMatch: true })
            .when('/currency/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveCurrency.html', controller: 'saveCurrencyCtrl', caseInsensitiveMatch: true })

             //GeographicRegion
            .when('/geographicRegion/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableGeographicRegion.html', controller: 'listTablegeographicRegionCtrl', caseInsensitiveMatch: true })
            .when('/geographicRegion/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailGeographicRegion.html', controller: 'detailgeographicRegionCtrl', caseInsensitiveMatch: true })
            .when('/geographicRegion/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveGeographicRegion.html', controller: 'saveGeographicRegionCtrl', caseInsensitiveMatch: true })

            //Religion
            .when('/religion/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableReligion.html', controller: 'listTablereligionCtrl', caseInsensitiveMatch: true })
            .when('/religion/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailReligion.html', controller: 'detailreligionCtrl', caseInsensitiveMatch: true })
            .when('/religion/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveReligion.html', controller: 'saveReligionCtrl', caseInsensitiveMatch: true })
            
            //Airport
            .when('/airport/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableAirport.html', controller: 'listTableairportCtrl', caseInsensitiveMatch: true })
            .when('/airport/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailAirport.html', controller: 'detailairportCtrl', caseInsensitiveMatch: true })
            .when('/airport/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveAirport.html', controller: 'saveAirportCtrl', caseInsensitiveMatch: true })

            //City
            .when('/city/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableCity.html', controller: 'listTablecityCtrl', caseInsensitiveMatch: true })
            .when('/city/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailCity.html', controller: 'detailcityCtrl', caseInsensitiveMatch: true })
            .when('/city/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveCity.html', controller: 'saveCityCtrl', caseInsensitiveMatch: true })

             //HelpLine
            .when('/helpLine/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableHelpLine.html', controller: 'listTablehelpLineCtrl', caseInsensitiveMatch: true })
            .when('/helpLine/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailHelpLine.html', controller: 'detailhelpLineCtrl', caseInsensitiveMatch: true })
            .when('/helpLine/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveHelpLine.html', controller: 'saveHelpLineCtrl', caseInsensitiveMatch: true })

            //Mall
            .when('/mall/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableMall.html', controller: 'listTablemallCtrl', caseInsensitiveMatch: true })
            .when('/mall/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailMall.html', controller: 'detailmallCtrl', caseInsensitiveMatch: true })
            .when('/mall/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveMall.html', controller: 'saveMallCtrl', caseInsensitiveMatch: true })

             //Monument
            .when('/monument/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableMonument.html', controller: 'listTablemonumentCtrl', caseInsensitiveMatch: true })
            .when('/monument/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailMonument.html', controller: 'detailmonumentCtrl', caseInsensitiveMatch: true })
            .when('/monument/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveMonument.html', controller: 'saveMonumentCtrl', caseInsensitiveMatch: true })

            //Region 
            .when('/region/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableRegion.html', controller: 'listTableregionCtrl', caseInsensitiveMatch: true })
            .when('/region/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailRegion.html', controller: 'detailregionCtrl', caseInsensitiveMatch: true })
            .when('/region/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveRegion.html', controller: 'saveRegionCtrl', caseInsensitiveMatch: true })

            //State
            .when('/state/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableState.html', controller: 'listTablestateCtrl', caseInsensitiveMatch: true })
            .when('/state/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailState.html', controller: 'detailstateCtrl', caseInsensitiveMatch: true })
            .when('/state/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveState.html', controller: 'saveStateCtrl', caseInsensitiveMatch: true })

            //TimeZone 
            .when('/timeZone/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableTimeZone.html', controller: 'listTabletimeZoneCtrl', caseInsensitiveMatch: true })
            .when('/timeZone/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailTimeZone.html', controller: 'detailtimeZoneCtrl', caseInsensitiveMatch: true })
            .when('/timeZone/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveTimeZone.html', controller: 'saveTimeZoneCtrl', caseInsensitiveMatch: true })
             
            //TrainStation
            .when('/trainStation/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableTrainStation.html', controller: 'listTabletrainStationCtrl', caseInsensitiveMatch: true })
            .when('/trainStation/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailTrainStation.html', controller: 'detailtrainStationCtrl', caseInsensitiveMatch: true })
            .when('/trainStation/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveTrainStation.html', controller: 'saveTrainStationCtrl', caseInsensitiveMatch: true })

             //PersonSuffix
            .when('/personSuffix/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTablePersonSuffix.html', controller: 'listTablepersonSuffixCtrl', caseInsensitiveMatch: true })
            .when('/personSuffix/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailPersonSuffix.html', controller: 'detailpersonSuffixCtrl', caseInsensitiveMatch: true })
            .when('/personSuffix/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/savePersonSuffix.html', controller: 'savePersonSuffixCtrl', caseInsensitiveMatch: true })

             //RaceType
            .when('/raceType/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableRaceType.html', controller: 'listTableraceTypeCtrl', caseInsensitiveMatch: true })
            .when('/raceType/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailRaceType.html', controller: 'detailraceTypeCtrl', caseInsensitiveMatch: true })
            .when('/raceType/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveRaceType.html', controller: 'saveRaceTypeCtrl', caseInsensitiveMatch: true })

             //Gender
            .when('/gender/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableGender.html', controller: 'listTablegenderCtrl', caseInsensitiveMatch: true })
            .when('/gender/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailGender.html', controller: 'detailgenderCtrl', caseInsensitiveMatch: true })
            .when('/gender/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveGender.html', controller: 'saveGenderCtrl', caseInsensitiveMatch: true })

            //Calendar
            .when('/calendar/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableCalendar.html', controller: 'listTablecalendarCtrl', caseInsensitiveMatch: true })
            .when('/calendar/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailCalendar.html', controller: 'detailcalendarCtrl', caseInsensitiveMatch: true })
            .when('/calendar/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveCalendar.html', controller: 'saveCalendarCtrl', caseInsensitiveMatch: true })

            //Holiday
            .when('/holiday/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableHoliday.html', controller: 'listTableholidayCtrl', caseInsensitiveMatch: true })
            .when('/holiday/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailHoliday.html', controller: 'detailholidayCtrl', caseInsensitiveMatch: true })
            .when('/holiday/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveHoliday.html', controller: 'saveHolidayCtrl', caseInsensitiveMatch: true })

            //Ethnicity
            .when('/ethnicity/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableEthnicity.html', controller: 'listTableethnicityCtrl', caseInsensitiveMatch: true })
            .when('/ethnicity/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailEthnicity.html', controller: 'detailethnicityCtrl', caseInsensitiveMatch: true })
            .when('/ethnicity/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveEthnicity.html', controller: 'saveEthnicityCtrl', caseInsensitiveMatch: true })

            //Address
            .when('/address/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableAddress.html', controller: 'listTableaddressCtrl', caseInsensitiveMatch: true })
            .when('/address/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailAddress.html', controller: 'detailaddressCtrl', caseInsensitiveMatch: true })
            .when('/address/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveAddress.html', controller: 'saveAddressCtrl', caseInsensitiveMatch: true })

            //ProvinceType
            .when('/provinceType/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableProvinceType.html', controller: 'listTableprovinceTypeCtrl', caseInsensitiveMatch: true })
            .when('/provinceType/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailProvinceType.html', controller: 'detailprovinceTypeCtrl', caseInsensitiveMatch: true })
            .when('/provinceType/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveProvinceType.html', controller: 'saveProvinceTypeCtrl', caseInsensitiveMatch: true })

            //Province
            .when('/province/search', { templateUrl: '../../app/BM/ReferenceData/views/Gen/List/listTableProvince.html', controller: 'listTableprovinceCtrl', caseInsensitiveMatch: true })
            .when('/province/detail/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Detail/detailProvince.html', controller: 'detailprovinceCtrl', caseInsensitiveMatch: true })
            .when('/province/save/:detailId', { templateUrl: '../../app/BM/ReferenceData/views/Gen/Save/saveProvince.html', controller: 'saveProvinceCtrl', caseInsensitiveMatch: true })

         
            // Time Zone X Country
            .when('/TimeZoneXCountry', { templateUrl: '../../app/BM/ReferenceData/views/Gen/CrossReference/TimeZoneXCountry.html', controller: 'timeZoneXCountryCtrl', caseInsensitiveMatch: true })

            // Holiday X Country // removing :moduleName coz module concept not yet implemented in RD app
            .when('/HolidayXCountry', { templateUrl: '../../app/BM/ReferenceData/views/Gen/CrossReference/HolidayXCountry.html', controller: 'holidayXCountryCtrl', caseInsensitiveMatch: true })

            // Country X Religion // removing :moduleName coz module concept not yet implemented in RD app
            .when('/CountryXReligion', { templateUrl: '../../app/BM/ReferenceData/views/Gen/CrossReference/CountryXReligion.html', controller: 'countryXReligionCtrl', caseInsensitiveMatch: true })
           

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
                if (columnName == 'CityId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'CityId';
                    objInfo["MethodName"] = 'GetCityList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'City';
                    objInfo["ModuleName"] = 'Core';
                }
                else if (columnName == 'StateId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'StateId';
                    objInfo["MethodName"] = 'GetStateList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'State';
                    objInfo["ModuleName"] = 'Core';
                }
                else if (columnName == 'CountryId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'CountryId';
                    objInfo["MethodName"] = 'GetCountryList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'Country';
                    objInfo["ModuleName"] = 'Core';
                }
                else if (columnName == 'ProvinceTypeId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'ProvinceTypeId';
                    objInfo["MethodName"] = 'GetProvinceTypeList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'ProvinceType';
                    objInfo["ModuleName"] = 'Core';
                }
                else if (columnName == 'TimeZoneId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'TimeZoneId';
                    objInfo["MethodName"] = 'GetTimeZoneList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'TimeZone';
                    objInfo["ModuleName"] = 'Core';
                }

                else if (columnName == 'HolidayId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'HolidayId';
                    objInfo["MethodName"] = 'GetHolidayList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'Holiday';
                    objInfo["ModuleName"] = 'Core';
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

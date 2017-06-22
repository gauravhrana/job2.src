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
            .when('/main', { templateUrl: '../../app/BM/DayCare/views/main.html', controller: 'mainCtrl' })
             .when('/:moduleName/main', { templateUrl: '../../app/BM/DayCare/views/main.html', controller: 'mainCtrl' })
            .when('/login', { templateUrl: '../../app/core/views/login.html', controller: 'loginCtrl' })
            .when('/logout', { templateUrl: '../../app/core/views/logout.html', controller: 'logoutCtrl' })


            .when('/:moduleName/test-restangular/search', { templateUrl: '../../app/examples/views/testRestangular.html', controller: 'testRestangularCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/test-restangular/save/:detailId', { templateUrl: '../../app/examples/views/testRestangularSave.html', controller: 'testRestangularSaveCtrl', caseInsensitiveMatch: true })

            //Schedule
            .when('/:moduleName/Schedule/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableSchedule.html', controller: 'listTableScheduleCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Schedule/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailSchedule.html', controller: 'detailScheduleCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Schedule/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveSchedule.html', controller: 'saveScheduleCtrl', caseInsensitiveMatch: true })

            //Schedule State
            .when('/:moduleName/ScheduleDetail/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableScheduleDetail.html', controller: 'listTableScheduleDetailCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ScheduleDetail/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailScheduleDetail.html', controller: 'detailScheduleDetailCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ScheduleDetail/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveScheduleDetail.html', controller: 'saveScheduleDetailCtrl', caseInsensitiveMatch: true })
            
            //Schedule State
            .when('/:moduleName/ScheduleState/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableScheduleState.html', controller: 'listTableScheduleStateCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ScheduleState/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailScheduleState.html', controller: 'detailScheduleStateCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ScheduleState/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveScheduleState.html', controller: 'saveScheduleStateCtrl', caseInsensitiveMatch: true })
            
            //Schedule State
            .when('/:moduleName/ScheduleDetailActivityCategory/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableScheduleDetailActivityCategory.html', controller: 'listTableScheduleDetailActivityCategoryCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ScheduleDetailActivityCategory/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailScheduleDetailActivityCategory.html', controller: 'detailScheduleDetailActivityCategoryCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ScheduleDetailActivityCategory/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveScheduleDetailActivityCategory.html', controller: 'saveScheduleDetailActivityCategoryCtrl', caseInsensitiveMatch: true })

            //Event Sub Type
            .when('/:moduleName/EventSubType/search', { templateUrl: '../../app/BM/DayCare/views/listTableEventSubType.html', controller: 'listTableEventSubTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/EventSubType/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailEventSubType.html', controller: 'detailEventSubTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/EventSubType/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveEventSubType.html', controller: 'saveEventSubTypeCtrl', caseInsensitiveMatch: true })

            //Activity Sub Type
            .when('/:moduleName/activitySubType/search', { templateUrl: '../../app/BM/DayCare/views/listTableActivitySubType.html', controller: 'listTableactivitySubTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/activitySubType/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailActivitySubType.html', controller: 'detailactivitySubTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/activitySubType/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveActivitySubType.html', controller: 'saveactivitySubTypeCtrl', caseInsensitiveMatch: true })

            //Activity Type
            .when('/:moduleName/activityType/search', { templateUrl: '../../app/BM/DayCare/views/listTableActivityType.html', controller: 'listTableactivityTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/activityType/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailActivityType.html', controller: 'detailactivityTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/activityType/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveActivityType.html', controller: 'saveactivityTypeCtrl', caseInsensitiveMatch: true })

            //Accident Report
            .when('/:moduleName/accidentReport/search', { templateUrl: '../../app/BM/DayCare/views/listTableAccidentReport.html', controller: 'listTableaccidentReportCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/accidentReport/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailAccidentReport.html', controller: 'detailaccidentReportCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/accidentReport/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveAccidentReport.html', controller: 'saveaccidentReportCtrl', caseInsensitiveMatch: true })
            
            //Accident Place
            .when('/:moduleName/accidentPlace/search', { templateUrl: '../../app/BM/DayCare/views/listTableAccidentPlace.html', controller: 'listTableaccidentPlaceCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/accidentPlace/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailAccidentPlace.html', controller: 'detailaccidentPlaceCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/accidentPlace/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveAccidentPlace.html', controller: 'saveaccidentPlaceCtrl', caseInsensitiveMatch: true })

            //Food Type
            .when('/:moduleName/FoodType/search', { templateUrl: '../../app/BM/DayCare/views/listTableFoodType.html', controller: 'listTableFoodTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FoodType/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailFoodType.html', controller: 'detailFoodTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FoodType/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveFoodType.html', controller: 'saveFoodTypeCtrl', caseInsensitiveMatch: true })

            //Meal Type
            .when('/:moduleName/mealType/search', { templateUrl: '../../app/BM/DayCare/views/listTableMealType.html', controller: 'listTablemealTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/mealType/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailMealType.html', controller: 'detailmealTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/mealType/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveMealType.html', controller: 'savemealTypeCtrl', caseInsensitiveMatch: true })

            //Event Type
            .when('/:moduleName/eventType/search', { templateUrl: '../../app/BM/DayCare/views/listTableEventType.html', controller: 'listTableeventTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/eventType/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailEventType.html', controller: 'detaileventTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/eventType/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveEventType.html', controller: 'saveeventTypeCtrl', caseInsensitiveMatch: true })

            //Diaper Status
            .when('/:moduleName/diaperStatus/search', { templateUrl: '../../app/BM/DayCare/views/listTableDiaperStatus.html', controller: 'listTablediaperStatusCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/diaperStatus/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailDiaperStatus.html', controller: 'detaildiaperStatusCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/diaperStatus/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveDiaperStatus.html', controller: 'savediaperStatusCtrl', caseInsensitiveMatch: true })

            //Need Item
            .when('/:moduleName/needItem/search', { templateUrl: '../../app/BM/DayCare/views/listTableNeedItem.html', controller: 'listTableneedItemCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/needItem/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailNeedItem.html', controller: 'detailneedItemCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/needItem/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveNeedItem.html', controller: 'saveneedItemCtrl', caseInsensitiveMatch: true })

            //BathRoom
            .when('/:moduleName/bathRoom/search', { templateUrl: '../../app/BM/DayCare/views/listTableBathRoom.html', controller: 'listTablebathRoomCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/bathRoom/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailBathRoom.html', controller: 'detailbathRoomCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/bathRoom/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveBathRoom.html', controller: 'savebathRoomCtrl', caseInsensitiveMatch: true })

            //Discount
            .when('/:moduleName/discount/search', { templateUrl: '../../app/BM/DayCare/views/listTableDiscount.html', controller: 'listTablediscountCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/discount/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailDiscount.html', controller: 'detaildiscountCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/discount/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveDiscount.html', controller: 'savediscountCtrl', caseInsensitiveMatch: true })

            //PaymentMethod
            .when('/:moduleName/paymentMethod/search', { templateUrl: '../../app/BM/DayCare/views/listTablePaymentMethod.html', controller: 'listTablepaymentMethodCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/paymentMethod/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailPaymentMethod.html', controller: 'detailpaymentMethodCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/paymentMethod/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/savePaymentMethod.html', controller: 'savepaymentMethodCtrl', caseInsensitiveMatch: true })


             //Comment
            .when('/:moduleName/Comment/search', { templateUrl: '../../app/BM/DayCare/views/listTableComment.html', controller: 'listTablecommentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Comment/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailComment.html', controller: 'detailcommentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Comment/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveComment.html', controller: 'savecommentCtrl', caseInsensitiveMatch: true })

             //Meal
            .when('/:moduleName/Meal/search', { templateUrl: '../../app/BM/DayCare/views/listTableMeal.html', controller: 'listTablemealCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Meal/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailMeal.html', controller: 'detailmealCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Meal/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveMeal.html', controller: 'savemealCtrl', caseInsensitiveMatch: true })

             //Meal Detail
            .when('/:moduleName/MealDetail/search', { templateUrl: '../../app/BM/DayCare/views/listTableMealDetail.html', controller: 'listTablemealDetailCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MealDetail/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailMealDetail.html', controller: 'detailmealDetailCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MealDetail/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveMealDetail.html', controller: 'savemealDetailCtrl', caseInsensitiveMatch: true })

             //Sick Report
            .when('/:moduleName/SickReport/search', { templateUrl: '../../app/BM/DayCare/views/listTableSickReport.html', controller: 'listTablesickReportCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SickReport/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailSickReport.html', controller: 'detailsickReportCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SickReport/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveSickReport.html', controller: 'savesickReportCtrl', caseInsensitiveMatch: true })

             //Tuition
            .when('/:moduleName/Tuition/search', { templateUrl: '../../app/BM/DayCare/views/listTableTuition.html', controller: 'listTabletuitionCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Tuition/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailTuition.html', controller: 'detailtuitionCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Tuition/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveTuition.html', controller: 'savetuitionCtrl', caseInsensitiveMatch: true })

             //Sleep
            .when('/:moduleName/Sleep/search', { templateUrl: '../../app/BM/DayCare/views/listTableSleep.html', controller: 'listTablesleepCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Sleep/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailSleep.html', controller: 'detailsleepCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Sleep/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveSleep.html', controller: 'savesleepCtrl', caseInsensitiveMatch: true })

            //Student
            .when('/:moduleName/Student/search', { templateUrl: '../../app/BM/DayCare/views/listTableStudent.html', controller: 'listTablestudentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Student/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailStudent.html', controller: 'detailstudentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Student/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveStudent.html', controller: 'savestudentCtrl', caseInsensitiveMatch: true })

			//Teacher
            .when('/:moduleName/Teacher/search', { templateUrl: '../../app/BM/DayCare/views/listTableTeacher.html', controller: 'listTableteacherCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Teacher/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailTeacher.html', controller: 'detailteacherCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Teacher/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveTeacher.html', controller: 'saveteacherCtrl', caseInsensitiveMatch: true })

            //Course
            .when('/:moduleName/Course/search', { templateUrl: '../../app/BM/DayCare/views/listTableCourse.html', controller: 'listTableCourseCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Course/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailCourse.html', controller: 'detailCourseCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Course/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveCourse.html', controller: 'saveCourseCtrl', caseInsensitiveMatch: true })

            //Department
            .when('/:moduleName/Department/search', { templateUrl: '../../app/BM/DayCare/views/listTableDepartment.html', controller: 'listTableDepartmentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Department/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailDepartment.html', controller: 'detailDepartmentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Department/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveDepartment.html', controller: 'saveDepartmentCtrl', caseInsensitiveMatch: true })

            //Registration
            .when('/:moduleName/Registration/search', { templateUrl: '../../app/BM/DayCare/views/listTableRegistration.html', controller: 'listTableRegistrationCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Registration/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailRegistration.html', controller: 'detailRegistrationCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Registration/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveRegistration.html', controller: 'saveRegistrationCtrl', caseInsensitiveMatch: true })

            //Generic
            //.when('/:entityName/search', { templateUrl: '../../app/core/views/general-list.html', controller: 'generalListCtrl', caseInsensitiveMatch: true })
            //.when('/:entityName/detail/:detailId', { templateUrl: '../../app/core/views/general-detail.html', controller: 'generalDetailCtrl', caseInsensitiveMatch: true })
            //.when('/:entityName/save/:detailId', { templateUrl: '../../app/core/views/general-save.html', controller: 'generalSaveCtrl', caseInsensitiveMatch: true })

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
    .directive('navMenu', ['$parse', '$compile', function ($parse, $compile) {
        return {
            restrict: 'C', //Element
            scope: true,
            link: function (scope, element, attrs) {
                scope.selectedNode = null;

                scope.$watch(attrs.menuData, function (val) {

                    var template = angular.element('<ul class="nav navbar-nav "><li data-dropdown ng-repeat="node in ' + attrs.menuData + '"><a ng-if="node.children.length>0" data-dropdown-toggle href="{{node.href}}">{{node.text}}<span class="caret"></span></a><a ng-if="node.children.length==0" href="{{node.href}}" >{{node.text}}</a><sub-navigation-tree></sub-navigation-tree></li></ul>');

                    var linkFunction = $compile(template);
                    linkFunction(scope);
                    element.html(null).append(template);

                }, true);
            }
        };
    }])
    .directive('subNavigationTree', ['$compile', function ($compile) {
        return {
            restrict: 'E', //Element
            scope: true,
            link: function (scope, element, attrs) {
                scope.tree = scope.node;

                if (scope.tree.children && scope.tree.children.length) {
                    var template = angular.element('<ul class="dropdown-menu"><li data-dropdown ng-repeat="node in tree.children" ng-class="{\'dropdown\' : node.children.length, \'dropdown-submenu\': node.children.length}"><a href="{{node.href}}">{{node.text}}</a><sub-navigation-tree  tree="node"></sub-navigation-tree></li></ul>');

                    var linkFunction = $compile(template);
                    linkFunction(scope);
                    element.replaceWith(template);
                }
                else {
                    element.remove();
                }
            }
        };
    }])
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



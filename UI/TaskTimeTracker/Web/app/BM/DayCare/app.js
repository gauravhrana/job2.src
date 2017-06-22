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
		, 'ui.grid', 'cgBusy', 'ui.grid.cellNav', 'ui.grid.edit', 'ui.grid.resizeColumns','ui.grid.exporter', 'ui.grid.pinning', 'ui.grid.selection', 'ui.grid.moveColumns', 'ui.grid.pagination'
		, 'ngTable'  
])
app.controller('MainCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.gridOptions = {
        enableGridMenu: true,
        enableSelectAll: true,
        //exporterCsvFilename: 'myFile.csv',
        exporterPdfDefaultStyle: {fontSize: 9},
        exporterPdfTableStyle: {margin: [30, 30, 30, 30]},
        exporterPdfTableHeaderStyle: {fontSize: 10, bold: true, italics: true, color: 'red'},
        exporterPdfHeader: { text: "My Header", style: 'headerStyle' },
        exporterPdfFooter: function ( currentPage, pageCount ) {
            return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
        },
        exporterPdfCustomFormatter: function ( docDefinition ) {
            docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
            docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
            return docDefinition;
        },
        exporterPdfOrientation: 'portrait',
        exporterPdfPageSize: 'LETTER',
        exporterPdfMaxGridWidth: 500,
        //exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function(gridApi){
            $scope.gridApi = gridApi;
        }
    };
 
    $http.get('/data/100.json')
    .success(function(data) {
        $scope.gridOptions.data = data;
    });
 
}])

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
               
            .when('/:moduleName/modalRowEditor', { templateUrl: '../../app/examples/views/modalRowEditor.html', controller: 'modalRowEditorCtrl', caseInsensitiveMatch: true })
           

            //Event Sub Type
            .when('/:moduleName/EventSubType/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableEventSubType.html', controller: 'listTableEventSubTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/EventSubType/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailEventSubType.html', controller: 'detailEventSubTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/EventSubType/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveEventSubType.html', controller: 'saveEventSubTypeCtrl', caseInsensitiveMatch: true })

            //Activity Sub Type
            .when('/:moduleName/ActivitySubType/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableActivitySubType.html', controller: 'listTableActivitySubTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ActivitySubType/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailActivitySubType.html', controller: 'detailActivitySubTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ActivitySubType/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveActivitySubType.html', controller: 'saveActivitySubTypeCtrl', caseInsensitiveMatch: true })

            //Activity Type
            .when('/:moduleName/ActivityType/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableActivityType.html', controller: 'listTableActivityTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ActivityType/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailActivityType.html', controller: 'detailActivityTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ActivityType/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveActivityType.html', controller: 'saveActivityTypeCtrl', caseInsensitiveMatch: true })

            //Accident Report
            .when('/:moduleName/AccidentReport/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableAccidentReport.html', controller: 'listTableAccidentReportCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccidentReport/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailAccidentReport.html', controller: 'detailAccidentReportCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccidentReport/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveAccidentReport.html', controller: 'saveAccidentReportCtrl', caseInsensitiveMatch: true })
            
            //Accident Place
            .when('/:moduleName/AccidentPlace/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableAccidentPlace.html', controller: 'listTableAccidentPlaceCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccidentPlace/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailAccidentPlace.html', controller: 'detailAccidentPlaceCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/AccidentPlace/save/:detailId', { templateUrl: '../../app/BM/DayCare/Gen/views/Gen/Save/saveAccidentPlace.html', controller: 'saveAccidentPlaceCtrl', caseInsensitiveMatch: true })

            //Food Type
            .when('/:moduleName/FoodType/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableFoodType.html', controller: 'listTableFoodTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FoodType/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailFoodType.html', controller: 'detailFoodTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/FoodType/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveFoodType.html', controller: 'saveFoodTypeCtrl', caseInsensitiveMatch: true })

            //Meal Type
            .when('/:moduleName/MealType/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableMealType.html', controller: 'listTableMealTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MealType/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailMealType.html', controller: 'detailMealTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MealType/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveMealType.html', controller: 'saveMealTypeCtrl', caseInsensitiveMatch: true })

            //Event Type
            .when('/:moduleName/EventType/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableEventType.html', controller: 'listTableEventTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/EventType/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailEventType.html', controller: 'detailEventTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/EventType/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveEventType.html', controller: 'saveEventTypeCtrl', caseInsensitiveMatch: true })

            //Diaper Status
            .when('/:moduleName/DiaperStatus/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableDiaperStatus.html', controller: 'listTableDiaperStatusCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/DiaperStatus/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailDiaperStatus.html', controller: 'detailDiaperStatusCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/DiaperStatus/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveDiaperStatus.html', controller: 'saveDiaperStatusCtrl', caseInsensitiveMatch: true })

            //Need Item
            .when('/:moduleName/NeedItem/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableNeedItem.html', controller: 'listTableNeedItemCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/NeedItem/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailNeedItem.html', controller: 'detailNeedItemCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/NeedItem/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveNeedItem.html', controller: 'saveNeedItemCtrl', caseInsensitiveMatch: true })

            //BathRoom
            .when('/:moduleName/BathRoom/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableBathRoom.html', controller: 'listTableBathRoomCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/BathRoom/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailBathRoom.html', controller: 'detailBathRoomCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/BathRoom/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveBathRoom.html', controller: 'saveBathRoomCtrl', caseInsensitiveMatch: true })

            //Discount
            .when('/:moduleName/Discount/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableDiscount.html', controller: 'listTableDiscountCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Discount/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailDiscount.html', controller: 'detailDiscountCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Discount/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveDiscount.html', controller: 'saveDiscountCtrl', caseInsensitiveMatch: true })

            //PaymentMethod
            .when('/:moduleName/PaymentMethod/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTablePaymentMethod.html', controller: 'listTablePaymentMethodCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PaymentMethod/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailPaymentMethod.html', controller: 'detailPaymentMethodCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PaymentMethod/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/savePaymentMethod.html', controller: 'savePaymentMethodCtrl', caseInsensitiveMatch: true })


             //Comment
            .when('/:moduleName/Comment/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableComment.html', controller: 'listTableCommentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Comment/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailComment.html', controller: 'detailCommentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Comment/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveComment.html', controller: 'saveCommentCtrl', caseInsensitiveMatch: true })

             //Meal
            .when('/:moduleName/Meal/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableMeal.html', controller: 'listTableMealCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Meal/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailMeal.html', controller: 'detailMealCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Meal/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveMeal.html', controller: 'saveMealCtrl', caseInsensitiveMatch: true })

             //Meal Detail
            .when('/:moduleName/MealDetail/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableMealDetail.html', controller: 'listTableMealDetailCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MealDetail/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailMealDetail.html', controller: 'detailMealDetailCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MealDetail/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveMealDetail.html', controller: 'saveMealDetailCtrl', caseInsensitiveMatch: true })

             //Sick Report
            .when('/:moduleName/SickReport/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableSickReport.html', controller: 'listTableSickReportCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SickReport/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailSickReport.html', controller: 'detailSickReportCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SickReport/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveSickReport.html', controller: 'saveSickReportCtrl', caseInsensitiveMatch: true })

             //Tuition
            .when('/:moduleName/Tuition/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableTuition.html', controller: 'listTableTuitionCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Tuition/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailTuition.html', controller: 'detailTuitionCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Tuition/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveTuition.html', controller: 'saveTuitionCtrl', caseInsensitiveMatch: true })

             //Sleep
            .when('/:moduleName/Sleep/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableSleep.html', controller: 'listTableSleepCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Sleep/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailSleep.html', controller: 'detailSleepCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Sleep/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveSleep.html', controller: 'saveSleepCtrl', caseInsensitiveMatch: true })

            //Student
            .when('/:moduleName/Student/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableStudent.html', controller: 'listTableStudentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Student/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailStudent.html', controller: 'detailStudentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Student/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveStudent.html', controller: 'saveStudentCtrl', caseInsensitiveMatch: true })

			//Teacher
            .when('/:moduleName/Teacher/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableTeacher.html', controller: 'listTableTeacherCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Teacher/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailTeacher.html', controller: 'detailTeacherCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Teacher/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveTeacher.html', controller: 'saveTeacherCtrl', caseInsensitiveMatch: true })

            //Course
            .when('/:moduleName/Course/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableCourse.html', controller: 'listTableCourseCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Course/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailCourse.html', controller: 'detailCourseCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Course/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveCourse.html', controller: 'saveCourseCtrl', caseInsensitiveMatch: true })

            //Department
            .when('/:moduleName/Department/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableDepartment.html', controller: 'listTableDepartmentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Department/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailDepartment.html', controller: 'detailDepartmentCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Department/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveDepartment.html', controller: 'saveDepartmentCtrl', caseInsensitiveMatch: true })

            //Registration
            .when('/:moduleName/Registration/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableRegistration.html', controller: 'listTableRegistrationCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Registration/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailRegistration.html', controller: 'detailRegistrationCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Registration/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveRegistration.html', controller: 'saveRegistrationCtrl', caseInsensitiveMatch: true })

            //Generic
            //.when('/:entityName/search', { templateUrl: '../../app/core/views/general-list.html', controller: 'generalListCtrl', caseInsensitiveMatch: true })
            //.when('/:entityName/detail/:detailId', { templateUrl: '../../app/core/views/general-detail.html', controller: 'generalDetailCtrl', caseInsensitiveMatch: true })
            //.when('/:entityName/save/:detailId', { templateUrl: '../../app/core/views/general-save.html', controller: 'generalSaveCtrl', caseInsensitiveMatch: true })

             //Schedule
            //.when('/:moduleName/Schedule/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableSchedule.html', controller: 'listTableScheduleCtrl', caseInsensitiveMatch: true })
            //.when('/:moduleName/Schedule/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailSchedule.html', controller: 'detailScheduleCtrl', caseInsensitiveMatch: true })
            //.when('/:moduleName/Schedule/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveSchedule.html', controller: 'saveScheduleCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Schedule/search', { templateUrl: '../../app/BM/DayCare/views/listTableSchedule.html', controller: 'listTableScheduleCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Schedule/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/detailSchedule.html', controller: 'detailScheduleCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Schedule/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/saveSchedule.html', controller: 'saveScheduleCtrl', caseInsensitiveMatch: true })

             //ScheduleDetail
            .when('/:moduleName/ScheduleDetail/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableScheduleDetail.html', controller: 'listTableScheduleDetailCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ScheduleDetail/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailScheduleDetail.html', controller: 'detailScheduleDetailCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ScheduleDetail/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveScheduleDetail.html', controller: 'saveScheduleDetailCtrl', caseInsensitiveMatch: true })

            //ScheduleState
            .when('/:moduleName/ScheduleState/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableScheduleState.html', controller: 'listTableScheduleStateCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ScheduleState/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailScheduleState.html', controller: 'detailScheduleStateCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ScheduleState/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveScheduleState.html', controller: 'saveScheduleStateCtrl', caseInsensitiveMatch: true })

             //ScheduleDetailActivityCategory
            .when('/:moduleName/ScheduleDetailActivityCategory/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableScheduleDetailActivityCategory.html', controller: 'listTableScheduleDetailActivityCategoryCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ScheduleDetailActivityCategory/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailScheduleDetailActivityCategory.html', controller: 'detailScheduleDetailActivityCategoryCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ScheduleDetailActivityCategory/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveScheduleDetailActivityCategory.html', controller: 'saveScheduleDetailActivityCategoryCtrl', caseInsensitiveMatch: true })

            //VacationPlan
            .when('/:moduleName/VacationPlan/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableVacationPlan.html', controller: 'listTableVacationPlanCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/VacationPlan/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailVacationPlan.html', controller: 'detailVacationPlanCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/VacationPlan/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveVacationPlan.html', controller: 'saveVacationPlanCtrl', caseInsensitiveMatch: true })
            
            //CourseInstance
            .when('/:moduleName/CourseInstance/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableCourseInstance.html', controller: 'listTableCourseInstanceCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CourseInstance/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailCourseInstance.html', controller: 'detailCourseInstanceCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CourseInstance/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveCourseInstance.html', controller: 'saveCourseInstanceCtrl', caseInsensitiveMatch: true })

            //SubjectMatter
            .when('/:moduleName/SubjectMatter/search', { templateUrl: '../../app/BM/DayCare/views/Gen/List/listTableSubjectMatter.html', controller: 'listTableSubjectMatterCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SubjectMatter/detail/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Detail/detailSubjectMatter.html', controller: 'detailSubjectMatterCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SubjectMatter/save/:detailId', { templateUrl: '../../app/BM/DayCare/views/Gen/Save/saveSubjectMatter.html', controller: 'saveSubjectMatterCtrl', caseInsensitiveMatch: true })

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
            restrict: 'E',
            require: 'ngModel',
            link: function (scope, element, attrs) {
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

                    var template = angular.element('<ul class="nav navbar-nav "><li class="dropdown" ng-repeat="node in ' + attrs.menuData + '"><a ng-if="node.children.length>0" class="dropdown-toggle" data-toggle="dropdown" href="{{node.href}}">{{node.text}}<span class="caret"></span></a><a ng-if="node.children.length==0" href="{{node.href}}" >{{node.text}}</a><sub-navigation-tree></sub-navigation-tree></li></ul>');

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
                    var template = angular.element('<ul class="dropdown-menu"><li class="dropdown" ng-repeat="node in tree.children" ng-class="{\'dropdown\' : node.children.length, \'dropdown-submenu\': node.children.length}"><a href="{{node.href}}">{{node.text}}</a><sub-navigation-tree  tree="node"></sub-navigation-tree></li></ul>');

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
                    objInfo["EntityName"] = 'EventType';
                    objInfo["ModuleName"] = 'Office';
                }
                else if (columnName == 'CourseId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'CourseId';
                    objInfo["MethodName"] = 'GetCourseList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'Course';
                    objInfo["ModuleName"] = 'Office';
                }
                else if (columnName == 'StudentId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'StudentId';
                    objInfo["MethodName"] = 'GetStudentList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'Student';
                    objInfo["ModuleName"] = 'Enrollment';
                }
                else if (columnName == 'PersonId') {
                    objInfo["DataTextField"] = 'ApplicationUserName';
                    objInfo["DataValueField"] = 'ApplicationUserId';
                    objInfo["MethodName"] = 'GetPersonList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'ApplicationUser';
                    objInfo["ModuleName"] = 'AA';
                    objInfo["ApplicationCode"] = 'DC';                   
                }
                else if (columnName == 'ScheduleStateId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'ScheduleStateId';
                    objInfo["MethodName"] = 'GetScheduleStateNameList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'ScheduleState';
                    objInfo["ModuleName"] = 'WorkSchedules';
                }
                else if (columnName == 'ScheduleDetailActivityCategoryId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'ScheduleDetailActivityCategoryId';
                    objInfo["MethodName"] = 'GetScheduleDetailActivityCategoryList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'ScheduleDetailActivityCategory';
                    objInfo["ModuleName"] = 'WorkSchedules';
                }
                else if (columnName == 'DepartmentId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'DepartmentId';
                    objInfo["MethodName"] = 'GetDepartmentList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'Department';
                    objInfo["ModuleName"] = 'Office';
                }
                else if (columnName == 'TeacherId') {
                    objInfo["DataTextField"] = 'Name';
                    objInfo["DataValueField"] = 'TeacherId';
                    objInfo["MethodName"] = 'GetTeacherList';
                    objInfo["Found"] = true;
                    objInfo["EntityName"] = 'Teacher';
                    objInfo["ModuleName"] = 'Enrollment';
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



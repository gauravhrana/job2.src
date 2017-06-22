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
            .when('/main', { templateUrl: '../../app/BM/Legal/views/main.html', controller: 'mainCtrl' })
            .when('/:moduleName/main', { templateUrl: '../../app/BM/Legal/views/main.html', controller: 'mainCtrl' })

            .when('/login', { templateUrl: '../../app/core/views/login.html', controller: 'loginCtrl' })
            .when('/logout', { templateUrl: '../../app/core/views/logout.html', controller: 'logoutCtrl' })

            //Restangular
            .when('/:moduleName/test-restangular/search', { templateUrl: '../../app/examples/views/testRestangular.html', controller: 'testRestangularCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/test-restangular/save/:detailId', { templateUrl: '../../app/examples/views/testRestangularSave.html', controller: 'testRestangularSaveCtrl', caseInsensitiveMatch: true })

            //CaseStatus
            .when('/:moduleName/CaseStatus/search', { templateUrl: '../../app/BM/Legal/views/Gen/List/listTableCaseStatus.html', controller: 'listTableCaseStatusCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CaseStatus/detail/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Detail/detailCaseStatus.html', controller: 'detailCaseStatusCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CaseStatus/save/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Save/saveCaseStatus.html', controller: 'saveCaseStatusCtrl', caseInsensitiveMatch: true })

            //CaseType
            .when('/:moduleName/CaseType/search', { templateUrl: '../../app/BM/Legal/views/Gen/List/listTableCaseType.html', controller: 'listTableCaseTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CaseType/detail/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Detail/detailCaseType.html', controller: 'detailCaseTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/CaseType/save/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Save/saveCaseType.html', controller: 'saveCaseTypeCtrl', caseInsensitiveMatch: true })

            //ClientType
            .when('/:moduleName/ClientType/search', { templateUrl: '../../app/BM/Legal/views/Gen/List/listTableClientType.html', controller: 'listTableClientTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ClientType/detail/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Detail/detailClientType.html', controller: 'detailClientTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ClientType/save/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Save/saveClientType.html', controller: 'saveClientTypeCtrl', caseInsensitiveMatch: true })

            //Counsel
            .when('/:moduleName/Counsel/search', { templateUrl: '../../app/BM/Legal/views/Gen/List/listTableCounsel.html', controller: 'listTableCounselCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Counsel/detail/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Detail/detailCounsel.html', controller: 'detailCounselCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Counsel/save/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Save/saveCounsel.html', controller: 'saveCounselCtrl', caseInsensitiveMatch: true })

            //Jurisdictions
            .when('/:moduleName/Jurisdictions/search', { templateUrl: '../../app/BM/Legal/views/Gen/List/listTableJurisdictions.html', controller: 'listTableJurisdictionsCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Jurisdictions/detail/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Detail/detailJurisdictions.html', controller: 'detailJurisdictionsCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/Jurisdictions/save/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Save/saveJurisdictions.html', controller: 'saveJurisdictionsCtrl', caseInsensitiveMatch: true })

            //MovantType
            .when('/:moduleName/MovantType/search', { templateUrl: '../../app/BM/Legal/views/Gen/List/listTableMovantType.html', controller: 'listTableMovantTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MovantType/detail/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Detail/detailMovantType.html', controller: 'detailMovantTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/MovantType/save/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Save/saveMovantType.html', controller: 'saveMovantTypeCtrl', caseInsensitiveMatch: true })

            //RetrievalMethod 
            .when('/:moduleName/RetrievalMethod/search', { templateUrl: '../../app/BM/Legal/views/Gen/List/listTableRetrievalMethod.html', controller: 'listTableRetrievalMethodCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/RetrievalMethod/detail/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Detail/detailRetrievalMethod.html', controller: 'detailRetrievalMethodCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/RetrievalMethod/save/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Save/saveRetrievalMethod.html', controller: 'saveRetrievalMethodCtrl', caseInsensitiveMatch: true })

            //ReportType
            .when('/:moduleName/ReportType/search', { templateUrl: '../../app/BM/Legal/views/Gen/List/listTableReportType.html', controller: 'listTableReportTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ReportType/detail/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Detail/detailReportType.html', controller: 'detailReportTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ReportType/save/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Save/saveReportType.html', controller: 'saveReportTypeCtrl', caseInsensitiveMatch: true })

            //ReportingRequirement	
            .when('/:moduleName/ReportingRequirement/search', { templateUrl: '../../app/BM/Legal/views/Gen/List/listTableReportingRequirement.html', controller: 'listTableReportingRequirementCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ReportingRequirement/detail/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Detail/detailReportingRequirement.html', controller: 'detailReportingRequirementCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/ReportingRequirement/save/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Save/saveReportingRequirement.html', controller: 'saveReportingRequirementCtrl', caseInsensitiveMatch: true })

            //SettlementStatus
             .when('/:moduleName/SettlementStatus/search', { templateUrl: '../../app/BM/Legal/views/Gen/List/listTableSettlementStatus.html', controller: 'listTableSettlementStatusCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SettlementStatus/detail/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Detail/detailSettlementStatus.html', controller: 'detailSettlementStatusCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/SettlementStatus/save/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Save/saveSettlementStatus.html', controller: 'saveSettlementStatusCtrl', caseInsensitiveMatch: true })

             //TransactionType
             .when('/:moduleName/TransactionType/search', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/List/listTableTransactionType.html', controller: 'listTableTransactionTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionType/detail/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Detail/detailTransactionType.html', controller: 'detailTransactionTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/TransactionType/save/:detailId', { templateUrl: '../../app/BM/CapitalMarkets/views/Gen/Save/saveTransactionType.html', controller: 'saveTransactionTypeCtrl', caseInsensitiveMatch: true })

            //PressReleaseType
            .when('/:moduleName/PressReleaseType/search', { templateUrl: '../../app/BM/Legal/views/Gen/List/listTablePressReleaseType.html', controller: 'listTablePressReleaseTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PressReleaseType/detail/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Detail/detailPressReleaseType.html', controller: 'detailPressReleaseTypeCtrl', caseInsensitiveMatch: true })
            .when('/:moduleName/PressReleaseType/save/:detailId', { templateUrl: '../../app/BM/Legal/views/Gen/Save/savePressReleaseType.html', controller: 'savePressReleaseTypeCtrl', caseInsensitiveMatch: true })



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
    			if (columnName == 'QuestionCategoryId') {
    				objInfo["DataTextField"] = 'Name';
    				objInfo["DataValueField"] = 'QuestionCategoryId';
    				objInfo["MethodName"] = 'GetQuestionCategoryList';
    				objInfo["Found"] = true;
    				objInfo["EntityName"] = 'QuestionCategory';
    				objInfo["ModuleName"] = '';
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
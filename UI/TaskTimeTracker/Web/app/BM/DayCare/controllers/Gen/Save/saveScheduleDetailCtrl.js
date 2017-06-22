'use strict';

angular.module('rootAppShell')
	.controller('saveScheduleDetailCtrl', [
		'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', '$cookies', 
		function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService, $cookies) {
		    
			$scope.entityName = 'ScheduleDetail';
			var entityService = $injector.get('scheduleDetailService');
			$scope.entityUrl = $scope.entityName;
			$scope.moduleName = $routeParams.moduleName;
			var formats = $cookies.get('DateFormat').replace('dddd', 'DD').replace('ddd', 'D').replace('yyyy', 'yy').replace('yy', 'y');
			$scope.DateFormat = formats;
			$scope.NewDateFormat = formats;
			$scope.ChangeFormat = function () {
				$scope.DateFormat = $scope.DateFormat === formats ? formats + ' hh:mm' : formats;
			}
			$scope.ChangeNewFormat  = function () {
				$scope.NewDateFormat = $scope.NewDateFormat === formats ? formats + ' hh:mm' : formats;
			}
			if ($routeParams.moduleName != undefined) {
			    $scope.entityUrl = $routeParams.moduleName + '/' + $scope.entityUrl;
			}

			dataAutoCompleteService.getComboSource({ methodName: 'GetPersonList' },
			function (response) {
				$scope.PersonList = response;
				$scope.selectedPersonItem = response[0];
			}, onFailedLoad);

			dataAutoCompleteService.getComboSource({ methodName: 'GetScheduleDetailActivityCategoryList' },
			function (response) {
				$scope.ScheduleDetailActivityCategoryList = response;
				$scope.selectedScheduleDetailActivityCategoryItem = response[0];
			}, onFailedLoad);

			$scope.WorkflowStateIsNew = function () {
				return ($routeParams.detailId == '{New}');
			};

			function onFailedLoad(serverResponse) {
				userService.AlertManager.logFailureAlert('', serverResponse.data, []);
			}

			function onSuccessLoad(data) {
				$scope.EntityItem = data;

				var itemsPerson = jQuery.grep($scope.PersonList, function (a) {
					return a.ApplicationUserId == data.PersonId
				});
				$scope.selectedPersonItem = itemsPerson[0];

				var itemsScheduleDetailActivityCategory = jQuery.grep($scope.ScheduleDetailActivityCategoryList, function (a) {
					return a.ScheduleDetailActivityCategoryId == data.ScheduleDetailActivityCategoryId
				});
				$scope.selectedScheduleDetailActivityCategoryItem = itemsScheduleDetailActivityCategory[0];
			}

			// init
			if (!$scope.WorkflowStateIsNew()) {
				entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
			}

			$scope.save = function () {

				$scope.EntityItem.PersonId = $scope.selectedPersonItem.ApplicationUserId;
				$scope.EntityItem.ScheduleDetailActivityCategoryId = $scope.selectedScheduleDetailActivityCategoryItem.ScheduleDetailActivityCategoryId;

				var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

				$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
					function () {

						if ($scope.WorkflowStateIsNew()) {
							userService.AlertManager.addSuccessAlert('New Record for Schedule Detail saved.');
						}
						else{
							userService.AlertManager.addSuccessAlert('Record for Schedule Detail saved.');
						}

						$location.url('/' + $scope.entityUrl + '/search');
					},
					function (errorResponse) {
						userService.AlertManager.addFailureAlert('Record for Schedule Detail failed to save. ' + [JSON.stringify(errorResponse)]);
					});
			};

			$scope.delete = function () {
				entityService.delete({ detailId: $routeParams.detailId },
					function () {
						userService.AlertManager.addSuccessAlert('Record for Schedule Detail deleted.');
						$location.url('/' + $scope.entityUrl + '/search');
					},
					function onFailedDelete(errorResponse) {
						userService.AlertManager.addFailureAlert('Record for Schedule Detail failed to delete.' + [JSON.stringify(errorResponse)]);
					});
			};

		}
	]);

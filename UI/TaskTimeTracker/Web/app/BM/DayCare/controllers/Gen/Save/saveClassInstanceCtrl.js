'use strict';

angular.module('rootAppShell')
	.controller('saveClassInstanceCtrl', [
		'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', '$cookies', 
		function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService, $cookies) {

			$scope.entityName = 'ClassInstance';
			var entityService = $injector.get('classInstanceService');
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

			dataAutoCompleteService.getComboSource({ methodName: 'GetCourseList' },
			function (response) {
				$scope.CourseList = response;
				$scope.selectedCourseItem = response[0];
			}, onFailedLoad);

			dataAutoCompleteService.getComboSource({ methodName: 'GetDepartmentList' },
			function (response) {
				$scope.DepartmentList = response;
				$scope.selectedDepartmentItem = response[0];
			}, onFailedLoad);

			dataAutoCompleteService.getComboSource({ methodName: 'GetTeacherList' },
			function (response) {
				$scope.TeacherList = response;
				$scope.selectedTeacherItem = response[0];
			}, onFailedLoad);

			$scope.WorkflowStateIsNew = function () {
				return ($routeParams.detailId == '{New}');
			};

			function onFailedLoad(serverResponse) {
				userService.AlertManager.logFailureAlert('', serverResponse.data, []);
			}

			function onSuccessLoad(data) {
				$scope.EntityItem = data;

				var itemsCourse = jQuery.grep($scope.CourseList, function (a) {
					return a.CourseId == data.CourseId
				});
				$scope.selectedCourseItem = itemsCourse[0];

				var itemsDepartment = jQuery.grep($scope.DepartmentList, function (a) {
					return a.DepartmentId == data.DepartmentId
				});
				$scope.selectedDepartmentItem = itemsDepartment[0];

				var itemsTeacher = jQuery.grep($scope.TeacherList, function (a) {
					return a.TeacherId == data.TeacherId
				});
				$scope.selectedTeacherItem = itemsTeacher[0];
			}

			// init
			if (!$scope.WorkflowStateIsNew()) {
				entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
			}

			$scope.save = function () {

				$scope.EntityItem.CourseId = $scope.selectedCourseItem.CourseId;
				$scope.EntityItem.DepartmentId = $scope.selectedDepartmentItem.DepartmentId;
				$scope.EntityItem.TeacherId = $scope.selectedTeacherItem.TeacherId;

				var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

				$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
					function () {

						if ($scope.WorkflowStateIsNew()) {
							userService.AlertManager.addSuccessAlert('New Record for Class Instance saved.');
						}
						else{
							userService.AlertManager.addSuccessAlert('Record for Class Instance saved.');
						}

						$location.url('/' + $scope.entityUrl + '/search');
					},
					function (errorResponse) {
						userService.AlertManager.addFailureAlert('Record for Class Instance failed to save. ' + [JSON.stringify(errorResponse)]);
					});
			};

			$scope.delete = function () {
				entityService.delete({ detailId: $routeParams.detailId },
					function () {
						userService.AlertManager.addSuccessAlert('Record for Class Instance deleted.');
						$location.url('/' + $scope.entityUrl + '/search');
					},
					function onFailedDelete(errorResponse) {
						userService.AlertManager.addFailureAlert('Record for Class Instance failed to delete.' + [JSON.stringify(errorResponse)]);
					});
			};

		}
	]);

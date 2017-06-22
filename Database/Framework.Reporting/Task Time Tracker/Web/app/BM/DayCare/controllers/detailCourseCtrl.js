		'use strict';

		angular.module('rootAppShell')
			.controller('detailCourseCtrl', [
				'$location', '$scope', '$injector', '$routeParams', 'userService',
				function ($location, $scope, $injector, $routeParams, userService) {

					$scope.entityName = 'Course';
					var entityService = $injector.get('courseService');

					$scope.WorkflowStateIsNew = function () {
						return ($routeParams.detailId == '{New}');
					};

					function onFailedLoad(serverResponse) {
						userService.AlertManager.logFailureAlert('', serverResponse.data, []);
					}

					function onSuccessLoad(data) {
						$scope.EntityItem = data;
						$scope.EntityItem.Id = $routeParams.detailId;
					}

					// init
					if (!$scope.WorkflowStateIsNew()) {
						entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
					}

					$scope.delete = function () {
						entityService.delete({ detailId: $routeParams.detailId },
							function () {
								userService.AlertManager.addSuccessAlert('Record for ' + $scope.entityName + ' deleted.');
								$location.url('/' + $scope.entityName + '/search');
							},
							function onFailedDelete(errorResponse) {
								userService.AlertManager.addFailureAlert('Record for ' + $scope.entityName + ' failed to delete.' + [JSON.stringify(errorResponse)]);
							});
					};

				}
			]);
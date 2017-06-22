'use strict';

angular.module('rootAppShell')
	.controller('detailaccidentPlaceCtrl', [
		'$location', '$scope', '$injector', '$routeParams', '$modal', 'userService', '$timeout', '$log',
		function ($location, $scope, $injector, $routeParams, $modal, userService, $timeout, $log) {

			$scope.entityName = 'accidentPlace';
			var entityService = $injector.get('accidentPlaceService');

			$scope.EntityItem = {
				Id: $routeParams.detailId,
				Name: '',
				Description: '',
				SortOrder: ''
			};

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
						userService.AlertManager.addSuccessAlert($scope.entityName + ' ' + $scope.EntityItem.Name + ' deleted.');
						$location.url('/' + $scope.entityName + '/search');
					},
					function onFailedDelete(errorResponse) {
						userService.AlertManager.addFailureAlert($scope.entityName + ' ' + $scope.EntityItem.Name + ' failed to delete.' + [JSON.stringify(errorResponse)]);
					});
			};

		}
	]);

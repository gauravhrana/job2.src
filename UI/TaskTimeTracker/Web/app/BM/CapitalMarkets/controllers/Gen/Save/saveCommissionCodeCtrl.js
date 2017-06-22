'use strict';

angular.module('rootAppShell')
	.controller('saveCommissionCodeCtrl', [
		'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', 
		function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService) {

			$scope.entityName = 'CommissionCode';
			var entityService = $injector.get('commissionCodeService');
			$scope.entityUrl = $scope.entityName;
			if ($routeParams.moduleName != undefined) {
			    $scope.entityUrl = $routeParams.moduleName + '/' + $scope.entityUrl;
			}

			dataAutoCompleteService.getComboSource({ methodName: 'GetBrokerList' },
			function (response) {
				$scope.BrokerList = response;
				$scope.selectedBrokerItem = response[0];
			}, onFailedLoad);

			$scope.WorkflowStateIsNew = function () {
				return ($routeParams.detailId == '{New}');
			};

			function onFailedLoad(serverResponse) {
				userService.AlertManager.logFailureAlert('', serverResponse.data, []);
			}

			function onSuccessLoad(data) {
				$scope.EntityItem = data;

				var itemsBroker = jQuery.grep($scope.BrokerList, function (a) {
					return a.BrokerId == data.BrokerId
				});
				$scope.selectedBrokerItem = itemsBroker[0];
			}

			// init
			if (!$scope.WorkflowStateIsNew()) {
				entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
			}

			$scope.save = function () {

				$scope.EntityItem.BrokerId = $scope.selectedBrokerItem.BrokerId;

				var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

				$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
					function () {

						if ($scope.WorkflowStateIsNew()) {
							userService.AlertManager.addSuccessAlert('New Record for Commission Code saved.');
						}
						else{
							userService.AlertManager.addSuccessAlert('Record for Commission Code saved.');
						}

						$location.url('/' + $scope.entityUrl + '/search');
					},
					function (errorResponse) {
						userService.AlertManager.addFailureAlert('Record for Commission Code failed to save. ' + [JSON.stringify(errorResponse)]);
					});
			};

			$scope.delete = function () {
				entityService.delete({ detailId: $routeParams.detailId },
					function () {
						userService.AlertManager.addSuccessAlert('Record for Commission Code deleted.');
						$location.url('/' + $scope.entityUrl + '/search');
					},
					function onFailedDelete(errorResponse) {
						userService.AlertManager.addFailureAlert('Record for Commission Code failed to delete.' + [JSON.stringify(errorResponse)]);
					});
			};

		}
	]);

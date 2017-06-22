	'use strict';

	angular.module('rootAppShell')
		.controller('saveCommissionRateCtrl', [
			'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', 
			function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService) {

				$scope.entityName = 'CommissionRate';
				var entityService = $injector.get('commissionRateService');
				$scope.entityUrl = $scope.entityName;
				if ($routeParams.moduleName != undefined) {
				    $scope.entityUrl = $routeParams.moduleName + '/' + $scope.entityUrl;
				}

				dataAutoCompleteService.getComboSource({ methodName: 'GetBrokerList' },
				function (response) {
					$scope.BrokerList = response;
					$scope.selectedBrokerItem = response[0];
				}, onFailedLoad);

				dataAutoCompleteService.getComboSource({ methodName: 'GetExchangeList' },
				function (response) {
					$scope.ExchangeList = response;
					$scope.selectedExchangeItem = response[0];
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

					var itemsExchange = jQuery.grep($scope.ExchangeList, function (a) {
						return a.ExchangeId == data.ExchangeId
					});
					$scope.selectedExchangeItem = itemsExchange[0];
				}

				// init
				if (!$scope.WorkflowStateIsNew()) {
					entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
				}

				$scope.save = function () {

					$scope.EntityItem.BrokerId = $scope.selectedBrokerItem.BrokerId;
					$scope.EntityItem.ExchangeId = $scope.selectedExchangeItem.ExchangeId;

					var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

					$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
						function () {

							if ($scope.WorkflowStateIsNew()) {
								userService.AlertManager.addSuccessAlert('New Record for Commission Rate saved.');
							}
							else{
								userService.AlertManager.addSuccessAlert('Record for Commission Rate saved.');
							}

							$location.url('/' + $scope.entityUrl + '/search');
						},
						function (errorResponse) {
							userService.AlertManager.addFailureAlert('Record for Commission Rate failed to save. ' + [JSON.stringify(errorResponse)]);
						});
				};

				$scope.delete = function () {
					entityService.delete({ detailId: $routeParams.detailId },
						function () {
							userService.AlertManager.addSuccessAlert('Record for Commission Rate deleted.');
							$location.url('/' + $scope.entityUrl + '/search');
						},
						function onFailedDelete(errorResponse) {
							userService.AlertManager.addFailureAlert('Record for Commission Rate failed to delete.' + [JSON.stringify(errorResponse)]);
						});
				};

			}
		]);

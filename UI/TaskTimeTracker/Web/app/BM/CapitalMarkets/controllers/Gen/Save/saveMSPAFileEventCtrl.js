	'use strict';

	angular.module('rootAppShell')
		.controller('saveMSPAFileEventCtrl', [
			'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', 
			function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService) {

				$scope.entityName = 'MSPAFileEvent';
				var entityService = $injector.get('mSPAFileEventService');
				$scope.entityUrl = $scope.entityName;
				if ($routeParams.moduleName != undefined) {
				    $scope.entityUrl = $routeParams.moduleName + '/' + $scope.entityUrl;
				}

				dataAutoCompleteService.getComboSource({ methodName: 'GetMSPAFileList' },
				function (response) {
					$scope.MSPAFileList = response;
					$scope.selectedMSPAFileItem = response[0];
				}, onFailedLoad);

				dataAutoCompleteService.getComboSource({ methodName: 'GetTradingEventTypeList' },
				function (response) {
					$scope.TradingEventTypeList = response;
					$scope.selectedTradingEventTypeItem = response[0];
				}, onFailedLoad);

				$scope.WorkflowStateIsNew = function () {
					return ($routeParams.detailId == '{New}');
				};

				function onFailedLoad(serverResponse) {
					userService.AlertManager.logFailureAlert('', serverResponse.data, []);
				}

				function onSuccessLoad(data) {
					$scope.EntityItem = data;

					var itemsMSPAFile = jQuery.grep($scope.MSPAFileList, function (a) {
						return a.MSPAFileId == data.MSPAFileId
					});
					$scope.selectedMSPAFileItem = itemsMSPAFile[0];

					var itemsTradingEventType = jQuery.grep($scope.TradingEventTypeList, function (a) {
						return a.TradingEventTypeId == data.TradingEventTypeId
					});
					$scope.selectedTradingEventTypeItem = itemsTradingEventType[0];
				}

				// init
				if (!$scope.WorkflowStateIsNew()) {
					entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
				}

				$scope.save = function () {

					$scope.EntityItem.MSPAFileId = $scope.selectedMSPAFileItem.MSPAFileId;
					$scope.EntityItem.TradingEventTypeId = $scope.selectedTradingEventTypeItem.TradingEventTypeId;

					var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

					$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
						function () {

							if ($scope.WorkflowStateIsNew()) {
								userService.AlertManager.addSuccessAlert('New Record for MSPA File Event saved.');
							}
							else{
								userService.AlertManager.addSuccessAlert('Record for MSPA File Event saved.');
							}

							$location.url('/' + $scope.entityUrl + '/search');
						},
						function (errorResponse) {
							userService.AlertManager.addFailureAlert('Record for MSPA File Event failed to save. ' + [JSON.stringify(errorResponse)]);
						});
				};

				$scope.delete = function () {
					entityService.delete({ detailId: $routeParams.detailId },
						function () {
							userService.AlertManager.addSuccessAlert('Record for MSPA File Event deleted.');
							$location.url('/' + $scope.entityUrl + '/search');
						},
						function onFailedDelete(errorResponse) {
							userService.AlertManager.addFailureAlert('Record for MSPA File Event failed to delete.' + [JSON.stringify(errorResponse)]);
						});
				};

			}
		]);

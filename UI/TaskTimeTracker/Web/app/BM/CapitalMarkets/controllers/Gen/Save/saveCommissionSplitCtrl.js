			'use strict';

			angular.module('rootAppShell')
				.controller('saveCommissionSplitCtrl', [
					'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', 
					function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService) {

						$scope.entityName = 'CommissionSplit';
						var entityService = $injector.get('commissionSplitService');
						$scope.entityUrl = $scope.entityName;
						if ($routeParams.moduleName != undefined) {
						    $scope.entityUrl = $routeParams.moduleName + '/' + $scope.entityUrl;
						}

						dataAutoCompleteService.getComboSource({ methodName: 'GetCommissionCodeList' },
						function (response) {
							$scope.CommissionCodeList = response;
							$scope.selectedCommissionCodeItem = response[0];
						}, onFailedLoad);

						$scope.WorkflowStateIsNew = function () {
							return ($routeParams.detailId == '{New}');
						};

						function onFailedLoad(serverResponse) {
							userService.AlertManager.logFailureAlert('', serverResponse.data, []);
						}

						function onSuccessLoad(data) {
							$scope.EntityItem = data;

							var itemsCommissionCode = jQuery.grep($scope.CommissionCodeList, function (a) {
								return a.CommissionCodeId == data.CommissionCodeId
							});
							$scope.selectedCommissionCodeItem = itemsCommissionCode[0];
						}

						// init
						if (!$scope.WorkflowStateIsNew()) {
							entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
						}

						$scope.save = function () {

							$scope.EntityItem.CommissionCodeId = $scope.selectedCommissionCodeItem.CommissionCodeId;

							var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

							$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
								function () {

									if ($scope.WorkflowStateIsNew()) {
										userService.AlertManager.addSuccessAlert('New Record for Commission Split saved.');
									}
									else{
										userService.AlertManager.addSuccessAlert('Record for Commission Split saved.');
									}

									$location.url('/' + $scope.entityUrl + '/search');
								},
								function (errorResponse) {
									userService.AlertManager.addFailureAlert('Record for Commission Split failed to save. ' + [JSON.stringify(errorResponse)]);
								});
						};

						$scope.delete = function () {
							entityService.delete({ detailId: $routeParams.detailId },
								function () {
									userService.AlertManager.addSuccessAlert('Record for Commission Split deleted.');
									$location.url('/' + $scope.entityUrl + '/search');
								},
								function onFailedDelete(errorResponse) {
									userService.AlertManager.addFailureAlert('Record for Commission Split failed to delete.' + [JSON.stringify(errorResponse)]);
								});
						};

					}
				]);

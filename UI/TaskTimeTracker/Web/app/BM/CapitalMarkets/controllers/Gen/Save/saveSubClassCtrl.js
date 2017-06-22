			'use strict';

			angular.module('rootAppShell')
				.controller('saveSubClassCtrl', [
					'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', 
					function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService) {

						$scope.entityName = 'SubClass';
						var entityService = $injector.get('subClassService');
						$scope.entityUrl = $scope.entityName;
						if ($routeParams.moduleName != undefined) {
						    $scope.entityUrl = $routeParams.moduleName + '/' + $scope.entityUrl;
						}

						dataAutoCompleteService.getComboSource({ methodName: 'GetFundList' },
						function (response) {
							$scope.FundList = response;
							$scope.selectedFundItem = response[0];
						}, onFailedLoad);

						$scope.WorkflowStateIsNew = function () {
							return ($routeParams.detailId == '{New}');
						};

						function onFailedLoad(serverResponse) {
							userService.AlertManager.logFailureAlert('', serverResponse.data, []);
						}

						function onSuccessLoad(data) {
							$scope.EntityItem = data;

							var itemsFund = jQuery.grep($scope.FundList, function (a) {
								return a.FundId == data.FundId
							});
							$scope.selectedFundItem = itemsFund[0];
						}

						// init
						if (!$scope.WorkflowStateIsNew()) {
							entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
						}

						$scope.save = function () {

							$scope.EntityItem.FundId = $scope.selectedFundItem.FundId;

							var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

							$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
								function () {

									if ($scope.WorkflowStateIsNew()) {
										userService.AlertManager.addSuccessAlert('New Record for ' + $scope.entityName + ' saved.');
									}
									else{
										userService.AlertManager.addSuccessAlert('Record for ' + $scope.entityName + ' saved.');
									}

									$location.url('/' + $scope.entityUrl + '/search');
								},
								function (errorResponse) {
									userService.AlertManager.addFailureAlert('Record for ' + $scope.entityName + ' failed to save. ' + [JSON.stringify(errorResponse)]);
								});
						};

						$scope.delete = function () {
							entityService.delete({ detailId: $routeParams.detailId },
								function () {
									userService.AlertManager.addSuccessAlert('Record for ' + $scope.entityName + ' deleted.');
									$location.url('/' + $scope.entityUrl + '/search');
								},
								function onFailedDelete(errorResponse) {
									userService.AlertManager.addFailureAlert('Record for ' + $scope.entityName + ' failed to delete.' + [JSON.stringify(errorResponse)]);
								});
						};

					}
				]);

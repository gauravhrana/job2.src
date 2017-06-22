								'use strict';

								angular.module('rootAppShell')
									.controller('saveFundCtrl', [
										'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', 
										function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService) {

											$scope.entityName = 'Fund';
											var entityService = $injector.get('fundService');
											$scope.entityUrl = $scope.entityName;
											if ($routeParams.moduleName != undefined) {
											    $scope.entityUrl = $routeParams.moduleName + '/' + $scope.entityUrl;
											}

											dataAutoCompleteService.getComboSource({ methodName: 'GetManagementFirmList' },
											function (response) {
												$scope.ManagementFirmList = response;
												$scope.selectedManagementFirmItem = response[0];
											}, onFailedLoad);

											$scope.WorkflowStateIsNew = function () {
												return ($routeParams.detailId == '{New}');
											};

											function onFailedLoad(serverResponse) {
												userService.AlertManager.logFailureAlert('', serverResponse.data, []);
											}

											function onSuccessLoad(data) {
												$scope.EntityItem = data;

												var itemsManagementFirm = jQuery.grep($scope.ManagementFirmList, function (a) {
													return a.ManagementFirmId == data.ManagementFirmId
												});
												$scope.selectedManagementFirmItem = itemsManagementFirm[0];
											}

											// init
											if (!$scope.WorkflowStateIsNew()) {
												entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
											}

											$scope.save = function () {

												$scope.EntityItem.ManagementFirmId = $scope.selectedManagementFirmItem.ManagementFirmId;

												var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

												$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
													function () {

														if ($scope.WorkflowStateIsNew()) {
															userService.AlertManager.addSuccessAlert('New Record for Fund saved.');
														}
														else{
															userService.AlertManager.addSuccessAlert('Record for Fund saved.');
														}

														$location.url('/' + $scope.entityUrl + '/search');
													},
													function (errorResponse) {
														userService.AlertManager.addFailureAlert('Record for Fund failed to save. ' + [JSON.stringify(errorResponse)]);
													});
											};

											$scope.delete = function () {
												entityService.delete({ detailId: $routeParams.detailId },
													function () {
														userService.AlertManager.addSuccessAlert('Record for Fund deleted.');
														$location.url('/' + $scope.entityUrl + '/search');
													},
													function onFailedDelete(errorResponse) {
														userService.AlertManager.addFailureAlert('Record for Fund failed to delete.' + [JSON.stringify(errorResponse)]);
													});
											};

										}
									]);

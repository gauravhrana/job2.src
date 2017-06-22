										'use strict';

										angular.module('rootAppShell')
											.controller('saveSettlementStatusCtrl', [
												'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', 
												function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService) {

													$scope.entityName = 'SettlementStatus';
													var entityService = $injector.get('settlementStatusService');
													$scope.entityUrl = $scope.entityName;
													if ($routeParams.moduleName != undefined) {
													    $scope.entityUrl = $routeParams.moduleName + '/' + $scope.entityUrl;
													}

													$scope.WorkflowStateIsNew = function () {
														return ($routeParams.detailId == '{New}');
													};

													function onFailedLoad(serverResponse) {
														userService.AlertManager.logFailureAlert('', serverResponse.data, []);
													}

													function onSuccessLoad(data) {
														$scope.EntityItem = data;
													}

													// init
													if (!$scope.WorkflowStateIsNew()) {
														entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
													}

													$scope.save = function () {


														var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

														$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
															function () {

																if ($scope.WorkflowStateIsNew()) {
																	userService.AlertManager.addSuccessAlert('New Record for Settlement Status saved.');
																}
																else{
																	userService.AlertManager.addSuccessAlert('Record for Settlement Status saved.');
																}

																$location.url('/' + $scope.entityUrl + '/search');
															},
															function (errorResponse) {
																userService.AlertManager.addFailureAlert('Record for Settlement Status failed to save. ' + [JSON.stringify(errorResponse)]);
															});
													};

													$scope.delete = function () {
														entityService.delete({ detailId: $routeParams.detailId },
															function () {
																userService.AlertManager.addSuccessAlert('Record for Settlement Status deleted.');
																$location.url('/' + $scope.entityUrl + '/search');
															},
															function onFailedDelete(errorResponse) {
																userService.AlertManager.addFailureAlert('Record for Settlement Status failed to delete.' + [JSON.stringify(errorResponse)]);
															});
													};

												}
											]);
																					'use strict';

																					angular.module('rootAppShell')
																						.controller('saveSecurityXIdentifierCtrl', [
																							'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', 
																							function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService) {

																								$scope.entityName = 'SecurityXIdentifier';
																								var entityService = $injector.get('securityXIdentifierService');
																								$scope.entityUrl = $scope.entityName;
																								if ($routeParams.moduleName != undefined) {
																								    $scope.entityUrl = $routeParams.moduleName + '/' + $scope.entityUrl;
																								}

																								dataAutoCompleteService.getComboSource({ methodName: 'GetSecurityList' },
																								function (response) {
																									$scope.SecurityList = response;
																									$scope.selectedSecurityItem = response[0];
																								}, onFailedLoad);

																								$scope.WorkflowStateIsNew = function () {
																									return ($routeParams.detailId == '{New}');
																								};

																								function onFailedLoad(serverResponse) {
																									userService.AlertManager.logFailureAlert('', serverResponse.data, []);
																								}

																								function onSuccessLoad(data) {
																									$scope.EntityItem = data;

																									var itemsSecurity = jQuery.grep($scope.SecurityList, function (a) {
																										return a.SecurityId == data.SecurityId
																									});
																									$scope.selectedSecurityItem = itemsSecurity[0];
																								}

																								// init
																								if (!$scope.WorkflowStateIsNew()) {
																									entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
																								}

																								$scope.save = function () {

																									$scope.EntityItem.SecurityId = $scope.selectedSecurityItem.SecurityId;

																									var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

																									$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
																										function () {

																											if ($scope.WorkflowStateIsNew()) {
																												userService.AlertManager.addSuccessAlert('New Record for Security X Identifier saved.');
																											}
																											else{
																												userService.AlertManager.addSuccessAlert('Record for Security X Identifier saved.');
																											}

																											$location.url('/' + $scope.entityUrl + '/search');
																										},
																										function (errorResponse) {
																											userService.AlertManager.addFailureAlert('Record for Security X Identifier failed to save. ' + [JSON.stringify(errorResponse)]);
																										});
																								};

																								$scope.delete = function () {
																									entityService.delete({ detailId: $routeParams.detailId },
																										function () {
																											userService.AlertManager.addSuccessAlert('Record for Security X Identifier deleted.');
																											$location.url('/' + $scope.entityUrl + '/search');
																										},
																										function onFailedDelete(errorResponse) {
																											userService.AlertManager.addFailureAlert('Record for Security X Identifier failed to delete.' + [JSON.stringify(errorResponse)]);
																										});
																								};

																							}
																						]);

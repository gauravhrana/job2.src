																				'use strict';

																				angular.module('rootAppShell')
																					.controller('saveSecurityXPartyCtrl', [
																						'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', 
																						function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService) {

																							$scope.entityName = 'SecurityXParty';
																							var entityService = $injector.get('securityXPartyService');
																							$scope.entityUrl = $scope.entityName;
																							if ($routeParams.moduleName != undefined) {
																							    $scope.entityUrl = $routeParams.moduleName + '/' + $scope.entityUrl;
																							}

																							dataAutoCompleteService.getComboSource({ methodName: 'GetExchangeList' },
																							function (response) {
																								$scope.ExchangeList = response;
																								$scope.selectedExchangeItem = response[0];
																							}, onFailedLoad);

																							dataAutoCompleteService.getComboSource({ methodName: 'GetIssuerList' },
																							function (response) {
																								$scope.IssuerList = response;
																								$scope.selectedIssuerItem = response[0];
																							}, onFailedLoad);

																							dataAutoCompleteService.getComboSource({ methodName: 'GetDeliveryAgentList' },
																							function (response) {
																								$scope.DeliveryAgentList = response;
																								$scope.selectedDeliveryAgentItem = response[0];
																							}, onFailedLoad);

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

																								var itemsExchange = jQuery.grep($scope.ExchangeList, function (a) {
																									return a.ExchangeId == data.ExchangeId
																								});
																								$scope.selectedExchangeItem = itemsExchange[0];

																								var itemsIssuer = jQuery.grep($scope.IssuerList, function (a) {
																									return a.IssuerId == data.IssuerId
																								});
																								$scope.selectedIssuerItem = itemsIssuer[0];

																								var itemsDeliveryAgent = jQuery.grep($scope.DeliveryAgentList, function (a) {
																									return a.DeliveryAgentId == data.DeliveryAgentId
																								});
																								$scope.selectedDeliveryAgentItem = itemsDeliveryAgent[0];

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

																								$scope.EntityItem.ExchangeId = $scope.selectedExchangeItem.ExchangeId;
																								$scope.EntityItem.IssuerId = $scope.selectedIssuerItem.IssuerId;
																								$scope.EntityItem.DeliveryAgentId = $scope.selectedDeliveryAgentItem.DeliveryAgentId;
																								$scope.EntityItem.SecurityId = $scope.selectedSecurityItem.SecurityId;

																								var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

																								$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
																									function () {

																										if ($scope.WorkflowStateIsNew()) {
																											userService.AlertManager.addSuccessAlert('New Record for Security X Party saved.');
																										}
																										else{
																											userService.AlertManager.addSuccessAlert('Record for Security X Party saved.');
																										}

																										$location.url('/' + $scope.entityUrl + '/search');
																									},
																									function (errorResponse) {
																										userService.AlertManager.addFailureAlert('Record for Security X Party failed to save. ' + [JSON.stringify(errorResponse)]);
																									});
																							};

																							$scope.delete = function () {
																								entityService.delete({ detailId: $routeParams.detailId },
																									function () {
																										userService.AlertManager.addSuccessAlert('Record for Security X Party deleted.');
																										$location.url('/' + $scope.entityUrl + '/search');
																									},
																									function onFailedDelete(errorResponse) {
																										userService.AlertManager.addFailureAlert('Record for Security X Party failed to delete.' + [JSON.stringify(errorResponse)]);
																									});
																							};

																						}
																					]);

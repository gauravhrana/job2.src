							'use strict';

							angular.module('rootAppShell')
								.controller('saveOrderItemCtrl', [
									'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', '$cookies', 
									function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService, $cookies) {

										$scope.entityName = 'OrderItem';
										var entityService = $injector.get('orderItemService');
										$scope.entityUrl = $scope.entityName;
										$scope.moduleName = $routeParams.moduleName;
										var formats = $cookies.get('DateFormat').replace('dddd', 'DD').replace('ddd', 'D').replace('yyyy', 'yy').replace('yy', 'y');
										$scope.DateFormat = formats;
										$scope.NewDateFormat = formats;
										$scope.ChangeFormat = function () {
											$scope.DateFormat = $scope.DateFormat === formats ? formats + ' hh:mm' : formats;
										}
										$scope.ChangeNewFormat  = function () {
											$scope.NewDateFormat = $scope.NewDateFormat === formats ? formats + ' hh:mm' : formats;
										}
										if ($routeParams.moduleName != undefined) {
										    $scope.entityUrl = $routeParams.moduleName + '/' + $scope.entityUrl;
										}

										dataAutoCompleteService.getComboSource({ methodName: 'GetOrderRequestList' },
										function (response) {
											$scope.OrderRequestList = response;
											$scope.selectedOrderRequestItem = response[0];
										}, onFailedLoad);

										dataAutoCompleteService.getComboSource({ methodName: 'GetOrderActionList' },
										function (response) {
											$scope.OrderActionList = response;
											$scope.selectedOrderActionItem = response[0];
										}, onFailedLoad);

										dataAutoCompleteService.getComboSource({ methodName: 'GetOrderTypeList' },
										function (response) {
											$scope.OrderTypeList = response;
											$scope.selectedOrderTypeItem = response[0];
										}, onFailedLoad);

										dataAutoCompleteService.getComboSource({ methodName: 'GetStrategyList' },
										function (response) {
											$scope.StrategyList = response;
											$scope.selectedStrategyItem = response[0];
										}, onFailedLoad);

										dataAutoCompleteService.getComboSource({ methodName: 'GetSecurityList' },
										function (response) {
											$scope.SecurityList = response;
											$scope.selectedSecurityItem = response[0];
										}, onFailedLoad);

										dataAutoCompleteService.getComboSource({ methodName: 'GetPortfolioList' },
										function (response) {
											$scope.PortfolioList = response;
											$scope.selectedPortfolioItem = response[0];
										}, onFailedLoad);

										$scope.WorkflowStateIsNew = function () {
											return ($routeParams.detailId == '{New}');
										};

										function onFailedLoad(serverResponse) {
											userService.AlertManager.logFailureAlert('', serverResponse.data, []);
										}

										function onSuccessLoad(data) {
											$scope.EntityItem = data;

											var itemsOrderRequest = jQuery.grep($scope.OrderRequestList, function (a) {
												return a.OrderRequestId == data.OrderRequestId
											});
											$scope.selectedOrderRequestItem = itemsOrderRequest[0];

											var itemsOrderAction = jQuery.grep($scope.OrderActionList, function (a) {
												return a.OrderActionId == data.OrderActionId
											});
											$scope.selectedOrderActionItem = itemsOrderAction[0];

											var itemsOrderType = jQuery.grep($scope.OrderTypeList, function (a) {
												return a.OrderTypeId == data.OrderTypeId
											});
											$scope.selectedOrderTypeItem = itemsOrderType[0];

											var itemsStrategy = jQuery.grep($scope.StrategyList, function (a) {
												return a.StrategyId == data.StrategyId
											});
											$scope.selectedStrategyItem = itemsStrategy[0];

											var itemsSecurity = jQuery.grep($scope.SecurityList, function (a) {
												return a.SecurityId == data.SecurityId
											});
											$scope.selectedSecurityItem = itemsSecurity[0];

											var itemsPortfolio = jQuery.grep($scope.PortfolioList, function (a) {
												return a.PortfolioId == data.PortfolioId
											});
											$scope.selectedPortfolioItem = itemsPortfolio[0];
										}

										// init
										if (!$scope.WorkflowStateIsNew()) {
											entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
										}

										$scope.save = function () {

											$scope.EntityItem.OrderRequestId = $scope.selectedOrderRequestItem.OrderRequestId;
											$scope.EntityItem.OrderActionId = $scope.selectedOrderActionItem.OrderActionId;
											$scope.EntityItem.OrderTypeId = $scope.selectedOrderTypeItem.OrderTypeId;
											$scope.EntityItem.StrategyId = $scope.selectedStrategyItem.StrategyId;
											$scope.EntityItem.SecurityId = $scope.selectedSecurityItem.SecurityId;
											$scope.EntityItem.PortfolioId = $scope.selectedPortfolioItem.PortfolioId;

											var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

											$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
												function () {

													if ($scope.WorkflowStateIsNew()) {
														userService.AlertManager.addSuccessAlert('New Record for Order Item saved.');
													}
													else{
														userService.AlertManager.addSuccessAlert('Record for Order Item saved.');
													}

													$location.url('/' + $scope.entityUrl + '/search');
												},
												function (errorResponse) {
													userService.AlertManager.addFailureAlert('Record for Order Item failed to save. ' + [JSON.stringify(errorResponse)]);
												});
										};

										$scope.delete = function () {
											entityService.delete({ detailId: $routeParams.detailId },
												function () {
													userService.AlertManager.addSuccessAlert('Record for Order Item deleted.');
													$location.url('/' + $scope.entityUrl + '/search');
												},
												function onFailedDelete(errorResponse) {
													userService.AlertManager.addFailureAlert('Record for Order Item failed to delete.' + [JSON.stringify(errorResponse)]);
												});
										};

									}
								]);

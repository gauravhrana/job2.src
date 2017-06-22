							'use strict';

							angular.module('rootAppShell')
								.controller('saveTransactionEventBuyCtrl', [
									'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', '$cookies', 
									function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService, $cookies) {

										$scope.entityName = 'TransactionEventBuy';
										var entityService = $injector.get('transactionEventBuyService');
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

										dataAutoCompleteService.getComboSource({ methodName: 'GetTransactionTypeList' },
										function (response) {
											$scope.TransactionTypeList = response;
											$scope.selectedTransactionTypeItem = response[0];
										}, onFailedLoad);

										dataAutoCompleteService.getComboSource({ methodName: 'GetCustodianList' },
										function (response) {
											$scope.CustodianList = response;
											$scope.selectedCustodianItem = response[0];
										}, onFailedLoad);

										dataAutoCompleteService.getComboSource({ methodName: 'GetStrategyList' },
										function (response) {
											$scope.StrategyList = response;
											$scope.selectedStrategyItem = response[0];
										}, onFailedLoad);

										dataAutoCompleteService.getComboSource({ methodName: 'GetAccountSpecificTypeList' },
										function (response) {
											$scope.AccountSpecificTypeList = response;
											$scope.selectedAccountSpecificTypeItem = response[0];
										}, onFailedLoad);

										dataAutoCompleteService.getComboSource({ methodName: 'GetInvestmentTypeList' },
										function (response) {
											$scope.InvestmentTypeList = response;
											$scope.selectedInvestmentTypeItem = response[0];
										}, onFailedLoad);

										$scope.WorkflowStateIsNew = function () {
											return ($routeParams.detailId == '{New}');
										};

										function onFailedLoad(serverResponse) {
											userService.AlertManager.logFailureAlert('', serverResponse.data, []);
										}

										function onSuccessLoad(data) {
											$scope.EntityItem = data;

											var itemsTransactionType = jQuery.grep($scope.TransactionTypeList, function (a) {
												return a.TransactionTypeId == data.TransactionTypeId
											});
											$scope.selectedTransactionTypeItem = itemsTransactionType[0];

											var itemsCustodian = jQuery.grep($scope.CustodianList, function (a) {
												return a.CustodianId == data.CustodianId
											});
											$scope.selectedCustodianItem = itemsCustodian[0];

											var itemsStrategy = jQuery.grep($scope.StrategyList, function (a) {
												return a.StrategyId == data.StrategyId
											});
											$scope.selectedStrategyItem = itemsStrategy[0];

											var itemsAccountSpecificType = jQuery.grep($scope.AccountSpecificTypeList, function (a) {
												return a.AccountSpecificTypeId == data.AccountSpecificTypeId
											});
											$scope.selectedAccountSpecificTypeItem = itemsAccountSpecificType[0];

											var itemsInvestmentType = jQuery.grep($scope.InvestmentTypeList, function (a) {
												return a.InvestmentTypeId == data.InvestmentTypeId
											});
											$scope.selectedInvestmentTypeItem = itemsInvestmentType[0];
										}

										// init
										if (!$scope.WorkflowStateIsNew()) {
											entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
										}

										$scope.save = function () {

											$scope.EntityItem.TransactionTypeId = $scope.selectedTransactionTypeItem.TransactionTypeId;
											$scope.EntityItem.CustodianId = $scope.selectedCustodianItem.CustodianId;
											$scope.EntityItem.StrategyId = $scope.selectedStrategyItem.StrategyId;
											$scope.EntityItem.AccountSpecificTypeId = $scope.selectedAccountSpecificTypeItem.AccountSpecificTypeId;
											$scope.EntityItem.InvestmentTypeId = $scope.selectedInvestmentTypeItem.InvestmentTypeId;

											var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

											$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
												function () {

													if ($scope.WorkflowStateIsNew()) {
														userService.AlertManager.addSuccessAlert('New Record for Transaction Event Buy saved.');
													}
													else{
														userService.AlertManager.addSuccessAlert('Record for Transaction Event Buy saved.');
													}

													$location.url('/' + $scope.entityUrl + '/search');
												},
												function (errorResponse) {
													userService.AlertManager.addFailureAlert('Record for Transaction Event Buy failed to save. ' + [JSON.stringify(errorResponse)]);
												});
										};

										$scope.delete = function () {
											entityService.delete({ detailId: $routeParams.detailId },
												function () {
													userService.AlertManager.addSuccessAlert('Record for Transaction Event Buy deleted.');
													$location.url('/' + $scope.entityUrl + '/search');
												},
												function onFailedDelete(errorResponse) {
													userService.AlertManager.addFailureAlert('Record for Transaction Event Buy failed to delete.' + [JSON.stringify(errorResponse)]);
												});
										};

									}
								]);

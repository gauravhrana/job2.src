										'use strict';

										angular.module('rootAppShell')
											.controller('saveRegionCtrl', [
												'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', '$cookies', 
												function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService, $cookies) {

													$scope.entityName = 'Region';
													var entityService = $injector.get('regionService');
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

													dataAutoCompleteService.getComboSource({ methodName: 'GetCountryList' },
													function (response) {
														$scope.CountryList = response;
														$scope.selectedCountryItem = response[0];
													}, onFailedLoad);

													$scope.WorkflowStateIsNew = function () {
														return ($routeParams.detailId == '{New}');
													};

													function onFailedLoad(serverResponse) {
														userService.AlertManager.logFailureAlert('', serverResponse.data, []);
													}

													function onSuccessLoad(data) {
														$scope.EntityItem = data;

														var itemsCountry = jQuery.grep($scope.CountryList, function (a) {
															return a.CountryId == data.CountryId
														});
														$scope.selectedCountryItem = itemsCountry[0];
													}

													// init
													if (!$scope.WorkflowStateIsNew()) {
														entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
													}

													$scope.save = function () {

														$scope.EntityItem.CountryId = $scope.selectedCountryItem.CountryId;

														var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

														$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
															function () {

																if ($scope.WorkflowStateIsNew()) {
																	userService.AlertManager.addSuccessAlert('New Record for Region saved.');
																}
																else{
																	userService.AlertManager.addSuccessAlert('Record for Region saved.');
																}

																$location.url('/' + $scope.entityUrl + '/search');
															},
															function (errorResponse) {
																userService.AlertManager.addFailureAlert('Record for Region failed to save. ' + [JSON.stringify(errorResponse)]);
															});
													};

													$scope.delete = function () {
														entityService.delete({ detailId: $routeParams.detailId },
															function () {
																userService.AlertManager.addSuccessAlert('Record for Region deleted.');
																$location.url('/' + $scope.entityUrl + '/search');
															},
															function onFailedDelete(errorResponse) {
																userService.AlertManager.addFailureAlert('Record for Region failed to delete.' + [JSON.stringify(errorResponse)]);
															});
													};

												}
											]);

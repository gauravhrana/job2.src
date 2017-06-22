																		'use strict';

																		angular.module('rootAppShell')
																			.controller('saveAddressCtrl', [
																				'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', '$cookies', 
																				function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService, $cookies) {

																					$scope.entityName = 'Address';
																					var entityService = $injector.get('addressService');
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

																					dataAutoCompleteService.getComboSource({ methodName: 'GetCityList' },
																					function (response) {
																						$scope.CityList = response;
																						$scope.selectedCityItem = response[0];
																					}, onFailedLoad);

																					dataAutoCompleteService.getComboSource({ methodName: 'GetStateList' },
																					function (response) {
																						$scope.StateList = response;
																						$scope.selectedStateItem = response[0];
																					}, onFailedLoad);

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

																						var itemsCity = jQuery.grep($scope.CityList, function (a) {
																							return a.CityId == data.CityId
																						});
																						$scope.selectedCityItem = itemsCity[0];

																						var itemsState = jQuery.grep($scope.StateList, function (a) {
																							return a.StateId == data.StateId
																						});
																						$scope.selectedStateItem = itemsState[0];

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

																						$scope.EntityItem.CityId = $scope.selectedCityItem.CityId;
																						$scope.EntityItem.StateId = $scope.selectedStateItem.StateId;
																						$scope.EntityItem.CountryId = $scope.selectedCountryItem.CountryId;

																						var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

																						$scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
																							function () {

																								if ($scope.WorkflowStateIsNew()) {
																									userService.AlertManager.addSuccessAlert('New Record for Address saved.');
																								}
																								else{
																									userService.AlertManager.addSuccessAlert('Record for Address saved.');
																								}

																								$location.url('/' + $scope.entityUrl + '/search');
																							},
																							function (errorResponse) {
																								userService.AlertManager.addFailureAlert('Record for Address failed to save. ' + [JSON.stringify(errorResponse)]);
																							});
																					};

																					$scope.delete = function () {
																						entityService.delete({ detailId: $routeParams.detailId },
																							function () {
																								userService.AlertManager.addSuccessAlert('Record for Address deleted.');
																								$location.url('/' + $scope.entityUrl + '/search');
																							},
																							function onFailedDelete(errorResponse) {
																								userService.AlertManager.addFailureAlert('Record for Address failed to delete.' + [JSON.stringify(errorResponse)]);
																							});
																					};

																				}
																			]);

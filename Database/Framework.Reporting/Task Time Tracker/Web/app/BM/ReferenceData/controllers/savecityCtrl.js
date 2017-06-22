						'use strict';

						angular.module('rootAppShell')
							.controller('savecityCtrl', [
								'$location', '$scope', '$injector', '$routeParams', '$modal', 'userService', '$timeout', '$log',
								function ($location, $scope, $injector, $routeParams, $modal, userService, $timeout, $log) {

									$scope.entityName = 'city';
									var entityService = $injector.get('cityService');

									$scope.EntityItem = {
										Id: $routeParams.detailId,
										Name: '',
										Description: '',
										SortOrder: ''
									};

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
												userService.AlertManager.addSuccessAlert($scope.entityName + ' ' + $scope.EntityItem.Name + ' saved.');
												$location.url('/' + $scope.entityName + '/search');
											},
											function (errorResponse) {
												userService.AlertManager.addFailureAlert($scope.entityName + ' failed to save. ' + [JSON.stringify(errorResponse)]);
											});
									};

									$scope.delete = function () {
										entityService.delete({ detailId: $routeParams.detailId },
											function () {
												userService.AlertManager.addSuccessAlert($scope.entityName + ' ' + $scope.EntityItem.Name + ' deleted.');
												$location.url('/' + $scope.entityName + '/search');
											},
											function onFailedDelete(errorResponse) {
												userService.AlertManager.addFailureAlert($scope.entityName + ' ' + $scope.EntityItem.Name + ' failed to delete.' + [JSON.stringify(errorResponse)]);
											});
									};

								}
							]);

								'use strict';

								angular.module('rootAppShell')
									.controller('saveReportTypeCtrl', [
										'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', 
										function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService) {

											$scope.entityName = 'ReportType';
											var entityService = $injector.get('reportTypeService');
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
															userService.AlertManager.addSuccessAlert('New Record for Report Type saved.');
														}
														else{
															userService.AlertManager.addSuccessAlert('Record for Report Type saved.');
														}

														$location.url('/' + $scope.entityUrl + '/search');
													},
													function (errorResponse) {
														userService.AlertManager.addFailureAlert('Record for Report Type failed to save. ' + [JSON.stringify(errorResponse)]);
													});
											};

											$scope.delete = function () {
												entityService.delete({ detailId: $routeParams.detailId },
													function () {
														userService.AlertManager.addSuccessAlert('Record for Report Type deleted.');
														$location.url('/' + $scope.entityUrl + '/search');
													},
													function onFailedDelete(errorResponse) {
														userService.AlertManager.addFailureAlert('Record for Report Type failed to delete.' + [JSON.stringify(errorResponse)]);
													});
											};

										}
									]);

					'use strict';

					angular.module('rootAppShell')
						.controller('saveCommentCtrl', [
							'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService', '$cookies', 
							function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService, $cookies) {

								$scope.entityName = 'Comment';
								var entityService = $injector.get('commentService');
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
												userService.AlertManager.addSuccessAlert('New Record for Comment saved.');
											}
											else{
												userService.AlertManager.addSuccessAlert('Record for Comment saved.');
											}

											$location.url('/' + $scope.entityUrl + '/search');
										},
										function (errorResponse) {
											userService.AlertManager.addFailureAlert('Record for Comment failed to save. ' + [JSON.stringify(errorResponse)]);
										});
								};

								$scope.delete = function () {
									entityService.delete({ detailId: $routeParams.detailId },
										function () {
											userService.AlertManager.addSuccessAlert('Record for Comment deleted.');
											$location.url('/' + $scope.entityUrl + '/search');
										},
										function onFailedDelete(errorResponse) {
											userService.AlertManager.addFailureAlert('Record for Comment failed to delete.' + [JSON.stringify(errorResponse)]);
										});
								};

							}
						]);

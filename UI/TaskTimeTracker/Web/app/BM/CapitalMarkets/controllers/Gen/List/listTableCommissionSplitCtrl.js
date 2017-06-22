			'use strict';

			angular.module('rootAppShell')
				.controller('listTableCommissionSplitCtrl', [
					'$scope', '$injector', '$routeParams', '$location', 'fieldConfigurationService', 'autoCompleteDataService',
					'dataAutoCompleteService', 'userService',

					function ($scope, $injector, $routeParams, $location, fieldConfigurationService, autoCompleteDataService, 
					dataAutoCompleteService, userService) {

						$scope.entityName = 'CommissionSplit';
						var entityService = $injector.get('commissionSplitService');
						$scope.entityUrl = $scope.entityName;
						if ($routeParams.moduleName != undefined) {
						    $scope.entityUrl = $routeParams.moduleName + '/' + $scope.entityUrl;
						}
						$scope.PrimaryKey = 'CommissionSplitId';
						$scope.applicationPath = $location.path();

						var settingCategory = $scope.entityName + 'AngularSearchControl';
						$scope.SubmitMessage = 'Add New';

						$scope.WorkflowStateIsNew = function () {
							return ($routeParams.detailId == '{New}');
						};

						function onFailedLoad(serverResponse) {
							userService.AlertManager.logFailureAlert('', serverResponse.data, []);
						}

						// get search filter columns
						fieldConfigurationService.getSearchFilterColumns({ value: $scope.entityName, value1: settingCategory }, onSuccessLoadSearchFC, onFailedLoad);

						// on successfull load of search filter columns
						function onSuccessLoadSearchFC(data) {
							$scope.searchFieldConfigurations = [];
							$scope.searchColumnSources = [];
							$scope.sourceItem = {};

							// loop thru all search filter columns
							for (var i = 0; i < data.length; i++) {
								var columnName = data[i].Name;

								if (columnName == 'GroupBy' || columnName == 'SubGroupBy') {

									var searchItem = {
										Name: data[i].Name
										, FieldConfigurationDisplayName: data[i].FieldConfigurationDisplayName
										, Value: data[i].Value
									};

									// define control type
									searchItem.ControlType = 'kendoComboGroupBy';
									getKendoKendoComboBoxGroupByData(columnName);
									$scope.searchFieldConfigurations.push(searchItem);

								}
								else if (columnName != 'GroupByDirection' && columnName != 'SubGroupByDirection') {

									var searchItem = {
										Name: data[i].Name
										, FieldConfigurationDisplayName: data[i].FieldConfigurationDisplayName
										, Value: data[i].Value
									};

									var methodInfo = autoCompleteDataService.getAutoCompleteMethod(columnName);
									if (methodInfo.Found == true) {

										// define control type
										searchItem.ControlType = 'kendoCombo';
										searchItem.URL = 'http://www.google.com'

										getKendoComboBoxSourceData(columnName, methodInfo);
										$scope.searchFieldConfigurations.push(searchItem);
									}
									else {

										// define control type
										searchItem.ControlType = 'kendoAutoComplete';

										getKendoAutoCompleteSourceData(columnName);
										$scope.searchFieldConfigurations.push(searchItem);
									}
								}
							}

							$scope.searchData();
						}

						function getKendoComboBoxSourceData(columnName, methodInfo) {

							dataAutoCompleteService.getKendoComboSource({ methodName: methodInfo.MethodName, columnName: columnName },
								function (response) {

									var options = [];
									var objOptionAll = {
										name: 'All',
										id: '-1'
									};
									options.push(objOptionAll);

									for (var i = 0; i < response.length; i++) {
										var objOption = {
											name: response[i][methodInfo.DataTextField],
											id: response[i][methodInfo.DataValueField]
										};

										options.push(objOption);
									}

									$scope.sourceItem[columnName] = options;

								}, onFailedLoad);
						}

						function getKendoAutoCompleteSourceData(columnName) {
							dataAutoCompleteService.getAutoCompleteList({ value: $scope.entityName, value1: columnName },
								function (response) {
									$scope.sourceItem[columnName] = response;
								}, onFailedLoad);
						}

						function getKendoKendoComboBoxGroupByData(columnName) {
							dataAutoCompleteService.getGroupByList({ value: $scope.entityName },
								function (response) {
									$scope.sourceItem[columnName] = response;
								}, onFailedLoad);
						}

						// data search function
						$scope.searchData = function () {
							var searchObj = {};

							// create dynamic search object which will have key, value pair
							for (var i = 0; i < $scope.searchFieldConfigurations.length; i++) {
								var filterName = $scope.searchFieldConfigurations[i].Name;
								var filterValue = $scope.searchFieldConfigurations[i].Value;

								searchObj[filterName] = filterValue;

								if (filterName == 'GroupBy') {
									$scope.groupByColumn = filterValue;
								}
								else if (filterName == 'SubGroupBy') {
									$scope.subGroupByColumn = filterValue;
								}
							}

							var searchString = JSON.stringify(searchObj);

							if ($scope.searchFieldConfigurations.length > 0) {
								// get entity records
								$scope.promise = entityService.getList({ value: searchString, value1: settingCategory }, onSuccessLoad, onFailedLoad);
							}

						};

						function onSuccessLoad(data) {

							$scope.groupingLevel = 0;

							// check if grouping exists
							if ($scope.groupByColumn != -1) {

								// sub grouping exists
								if ($scope.subGroupByColumn != -1 && $scope.groupByColumn != $scope.subGroupByColumn) {

									$scope.subGroups = [];
									$scope.groupingLevel = 2;

									var i = 0;

									// get all disticnt group by values
									var uGroup = [];
									for (i = 0; i < data.length; i++) {

										var grpByValue = data[i][$scope.groupByColumn];
										if (uGroup.indexOf(grpByValue) == -1) {
											uGroup.push(grpByValue);
										}
									}

									// get all disticnt sub group by values
									var uSubGroup = [];
									for (i = 0; i < data.length; i++) {

										var subGrpByValue = data[i][$scope.subGroupByColumn];
										if (uSubGroup.indexOf(subGrpByValue) == -1) {
											uSubGroup.push(subGrpByValue);
										}
									}

									$scope.subGroupValues = {};
									for (i = 0; i < uGroup.length; i++) {

										// current group by value
										var grpByValue = uGroup[i];

										// grouped item that will hold data for that particular group tab
										var groupedItem = {};
										for (var j = 0; j < uSubGroup.length ; j++) {

											var subGrpByValue = uSubGroup[j];

											// search for related records in result data
											var items = jQuery.grep(data, function (a) {
												return a[$scope.groupByColumn] == grpByValue && a[$scope.subGroupByColumn] == subGrpByValue;
											});

											// if data exists then add sub group item to group item's child
											if (items.length > 0) {

												// check for existance
												if ($scope.subGroupValues[grpByValue] == undefined) {
													$scope.subGroupValues[grpByValue] = [];

													groupedItem = {};
												}

												// assign data to grouped items resultant set
												groupedItem[subGrpByValue] = items;

												// push sub group by value item 
												$scope.subGroupValues[grpByValue].push(subGrpByValue);
											}
										}

										// add grouped data item as the value of the property
										$scope.subGroups[grpByValue] = groupedItem;
									}

									$scope.groupValues = uGroup;
								}
								else // only grouping
								{

									$scope.groups = {};
									$scope.groupingLevel = 1;

									var uGroup = [];
									for (var i = 0; i < data.length; i++) {

										var grpByValue = data[i][$scope.groupByColumn];
										if (uGroup.indexOf(grpByValue) == -1) {
											uGroup.push(grpByValue);
										}

										if ($scope.groups[grpByValue] == undefined) {
											$scope.groups[grpByValue] = [];
										}
										$scope.groups[grpByValue].push(data[i]);
									}

									$scope.groupValues = uGroup;
								}

							}
							else { // no grouping at all
								$scope.entityItems = data;
							}
						}

						// add new link function
						$scope.addNew = function () {
							$scope.SubmitMessage = 'Adding New ...';
							$location.url('/' + $scope.entityUrl + '/save/{New}');
						};

						$scope.reloadFC = function (myFcMode) {
							$scope.myFcMode = myFcMode;
							saveUserFieldConfiguration();
							getFieldConfigurations();
						};

						// get fc modes applicable to entity
						fieldConfigurationService.getFCModes({ value: $scope.entityName }, onSuccessLoadFCModes, onFailedLoad);

						function onSuccessLoadFCModes(data) {
							$scope.fcModes = data;

							// get user preferred fc mode
							fieldConfigurationService.getUserFieldConfigurationMode({ value: $scope.entityName }, onSuccessLoadUserFCMode, onFailedLoad);
						}

						function onSuccessLoadUserFCMode(data, getResponseHeaders) {
							$scope.myFcMode = data;

							if ($scope.myFcMode.FieldConfigurationModeId == -1) {
								$scope.myFcMode = $scope.fcModes[0];
								saveUserFieldConfiguration();
							}
							getFieldConfigurations();
						}

						function getFieldConfigurations() {

							// get user preferred columns based on user preferred fc mode
							fieldConfigurationService.getFieldConfigurations({ value: $scope.myFcMode.FieldConfigurationModeId, value1: $scope.entityName }, onSuccessLoadColumns, onFailedLoad);
						}

						function onSuccessLoadColumns(response) {

							var fcColumns = [];
							var tmpKey = $scope.entityName + 'Id';

							for (var i = 0; i < response.length; i++) {
								if (response[i].Name.toLowerCase() == tmpKey.toLowerCase()) {
									$scope.PrimaryKey = response[i].Name;
								}                    

								var isVisible = response[i].GridViewPriority == -1 ? false : true;
								if (isVisible) {

									var fcItem1 = {
										field: response[i].Name
										, displayName: response[i].FieldConfigurationDisplayName
										, HorizontalAlignment: response[i].HorizontalAlignment
										, visible: isVisible
									};
									fcColumns.push(fcItem1);
								}
							}

							$scope.columns = fcColumns;

						}

						function saveUserFieldConfiguration() {
							fieldConfigurationService.updateUserFieldConfigurationMode({ value: $scope.myFcMode.FieldConfigurationModeId, value1: $scope.entityName },
								function () { },
								function (serverResponse) {
									userService.AlertManager.logFailureAlert('', serverResponse.data, []);
								});
						}

						$scope.resetSearchData = function () {

							 // reset search filter columns
							fieldConfigurationService.resetSearchFilterColumns({ value: $scope.entityName, value1: settingCategory }, onSuccessLoadSearchFC, onFailedLoad);
						};

					}
				]);

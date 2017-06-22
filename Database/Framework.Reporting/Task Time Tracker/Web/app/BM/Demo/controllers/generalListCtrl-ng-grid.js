'use strict';

angular.module('rootAppShell')
    .controller('generalListNgGridCtrl', [
        '$scope', '$injector', '$routeParams', '$location', 'fieldConfigurationService', 'userService', 'cachingService', 'uiGridConstants', '$interval', '$http', '$timeout',

function ($scope, $injector, $routeParams, $location, fieldConfigurationService, userService, cachingService, uiGridConstants, $interval, $http,  $timeout) {

            $scope.entityName = $routeParams.entityName;
            $scope.PrimaryKey = '';


            $scope.UniqueCategories = [];
            $scope.UniqueCategoriesChunked = [];
            $scope.errors = [];
            $scope.CanSave = true;
            $scope.SubmitMessage = 'Add New';
            $scope.Grouped = [];
            $scope.searchText = cachingService.get('' + $scope.entityName + 'Search');

            function onFailedLoad(serverResponse) {
                userService.AlertManager.logFailureAlert('', serverResponse.data, []);
            }

            $scope.addNew = function () {
                $scope.SubmitMessage = 'Adding New ...';
                $location.url('/' + $scope.entityName + '/save/{New}');
            };

            $scope.reloadFC = function (myFcMode) {
                $scope.myFcMode = myFcMode;
                saveUserFieldConfiguration();
                getFieldConfigurations();
            };

            function onSuccessLoadFCModes(data) {
                $scope.fcModes = data;

                // get user preferred fc mode
                fieldConfigurationService.getUserFieldConfigurationMode({ entityName: $scope.entityName }, onSuccessLoadFC, onFailedLoad);
            }

            function onSuccessLoadFC(data, getResponseHeaders) {
                $scope.myFcMode = data;

                if ($scope.myFcMode.FieldConfigurationModeId == -1) {
                    $scope.myFcMode = $scope.fcModes[0];
                    saveUserFieldConfiguration();
                }
                getFieldConfigurations();
            }


            // get fc modes applicable to entity
            fieldConfigurationService.getFCModes({ entityName: $scope.entityName }, onSuccessLoadFCModes, onFailedLoad);

            function saveUserFieldConfiguration() {
                fieldConfigurationService.updateUserFieldConfigurationMode({ fcModeId: $scope.myFcMode.FieldConfigurationModeId, entityName: $scope.entityName },
                    function () { },
                    function (serverResponse) {
                        userService.AlertManager.logFailureAlert('', serverResponse.data, []);
                    });
            }

            function getFieldConfigurations() {

                // get user preferred columns based on user preferred fc mode
                fieldConfigurationService.getFieldConfigurations({ fcModeId: $scope.myFcMode.FieldConfigurationModeId, entityName: $scope.entityName },
                   function (response) {

						var fcColumns = [];

						var tmpKey = $scope.entityName + "Id";
						for (var i = 0; i < response.length; i++) {
                           if (response[i].Name.toLowerCase() == tmpKey.toLowerCase()) {
                               $scope.PrimaryKey = response[i].Name;
                           }

                           var fcItem1 = { field: response[i].Name, displayName: response[i].FieldConfigurationDisplayName };
                           fcColumns.push(fcItem1);
						}

                       var fcItem = {
                           field: $scope.PrimaryKey
                               , displayName: 'Application Mode Id'
                               , cellTemplate: '<a href="#/' + $scope.entityName + '/save/{{row.getProperty(col.field)}}">Edit</a>'
                       };
                       fcColumns.push(fcItem);

                       fcItem = {
                           field: $scope.PrimaryKey
                               , displayName: 'Action'
                               , cellTemplate: '<a href="#/' + $scope.entityName + '/detail/{{row.getProperty(col.field)}}">Detail</a>'
                       };
                       fcColumns.push(fcItem);

                       $scope.columns = fcColumns;

                   }, function (errorResponse) {
                       userService.AlertManager.addFailureResponse(errorResponse);
                   });
            }

            $scope.gridOptionsT = {
            	enablePaginationControls: false,
            	paginationPageSize: 25,
            };
            $scope.gridOptionsT.data = 'myData';
            $scope.gridOptionsT.enableColumnResizing = true;
            $scope.gridOptionsT.enableFiltering = true;
            $scope.gridOptionsT.enableGridMenu = true;
            $scope.gridOptionsT.showGridFooter = true;
            $scope.gridOptionsT.showColumnFooter = true;

            $scope.gridOptionsT.rowIdentity = function (row) {
				return row.id;
			};

            $scope.gridOptionsT.getRowIdentity = function (row) {
            	return row.id;
            };

            $scope.gridOptionsT.columnDefs = [
				{ name:'id', width:50 },
				{ name:'name', width:100 },
				{ name:'age', width: 100, enableCellEdit: true, aggregationType: uiGridConstants.aggregationTypes.avg,cellTemplate: '<div class="ui-grid-cell-contents"><span>{{COL_FIELD}}</span></div>'},
				{ name:'address.street', width:150, enableCellEdit: true, cellTemplate: '<div class="ui-grid-cell-contents"><span>{{COL_FIELD}}</span></div>'   },
				{ name:'address.city', width:150, enableCellEdit: true, cellTemplate: '<div class="ui-grid-cell-contents"><span>{{COL_FIELD}}</span></div>'  },
				{ name:'address.state', width:50, enableCellEdit: true, cellTemplate: '<div class="ui-grid-cell-contents"><span>{{COL_FIELD}}</span></div>'  },
				{ name:'agetemplate',field:'age', width:100, cellTemplate: '<div class="ui-grid-cell-contents"><span>{{COL_FIELD}}</span></div>' }
			];

            $scope.gridOptionsT.onRegisterApi = function (gridApi) {
            	$scope.grid1 = gridApi;
			}

			$scope.callsPending = 0;

			var i = 0;
			$scope.refreshData = function(){

				$scope.myData = [];

				var start = new Date();
				var sec = $interval(function () {
				$scope.callsPending++;

				$http.get('/data/500_complex.json')
					.success(function(data) {
						$scope.callsPending--;

						data.forEach(function(row){
							row.name = row.name + ' iter ' + i;
							row.id = i;
							i++;
							$scope.myData.push(row);
						});
					})
					.error(function() {
						$scope.callsPending--;
					});
				}, 200);

				$timeout(function() {
					$interval.cancel(sec);
					$scope.left = '';
					}, 2000);
			};
        }

    ]);
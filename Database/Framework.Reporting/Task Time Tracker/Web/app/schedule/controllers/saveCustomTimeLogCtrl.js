'use strict';

angular.module('rootAppShell')
	.controller('saveCustomTimeLogCtrl', [
		'dataAutoCompleteService', '$location', '$scope', '$injector', '$routeParams', 'userService',
            function (dataAutoCompleteService, $location, $scope, $injector, $routeParams, userService) {

		    $scope.entityName = 'CustomTimeLog';
		    var entityService = $injector.get('customTimeLogService');

		    dataAutoCompleteService.getComboSource({ methodName: 'GetPersonList' },
                function (response) {
                    $scope.PersonList = response;
                    $scope.selectedPersonItem = response[0];
                    //$scope.EntityItem.PersonId = $scope.selectedPersonItem.ApplicationUserId;

                }, onFailedLoad);

		    dataAutoCompleteService.getComboSource({ methodName: 'GetCustomTimeCategoryList' },
                function (response) {
                    $scope.CustomTimeCategoryList = response;
                    $scope.selectedCustomTimeCategoryItem = response[0];
                    //$scope.EntityItem.CustomTimeCategoryId = $scope.selectedCustomTimeCategoryItem.CustomTimeCategoryId;

                }, onFailedLoad);

		    $scope.WorkflowStateIsNew = function () {
		        return ($routeParams.detailId == '{New}');
		    };

		    function onFailedLoad(serverResponse) {
                debugger
		        userService.AlertManager.logFailureAlert('', serverResponse.data, []);
		    }

		    function onSuccessLoad(data) {

                $scope.EntityItem = data;

                var itemsCategory = jQuery.grep($scope.CustomTimeCategoryList, function (a) {
                    return a.CustomTimeCategoryId == data.CustomTimeCategoryId
                });

                $scope.selectedCustomTimeCategoryItem = itemsCategory[0];

                var itemsPerson = jQuery.grep($scope.PersonList, function (a) {
                    return a.ApplicationUserId == data.PersonId
                });

                $scope.selectedPersonItem = itemsPerson[0];
		    }

		    // init
		    if (!$scope.WorkflowStateIsNew()) {
		        entityService.getById({ detailId: $routeParams.detailId }, onSuccessLoad, onFailedLoad);
		    }

		    $scope.save = function () {

		        $scope.EntityItem.PersonId = $scope.selectedPersonItem.ApplicationUserId;
		        $scope.EntityItem.CustomTimeCategoryId = $scope.selectedCustomTimeCategoryItem.CustomTimeCategoryId;

		        var saveMethod = ($scope.WorkflowStateIsNew()) ? 'create' : 'update';

		        $scope.EntityItem = entityService[saveMethod]($scope.EntityItem,
                    function () {
                        userService.AlertManager.addSuccessAlert('New Record for ' + $scope.entityName + ' saved.');
                        $location.url('/' + $scope.entityName + '/search');
                    },
                    function (errorResponse) {
                        userService.AlertManager.addFailureAlert('Record for ' + $scope.entityName + ' failed to save. ' + [JSON.stringify(errorResponse)]);
                    });
		    };

		    $scope.delete = function () {
		        entityService.delete({ detailId: $routeParams.detailId },
					function () {
					    userService.AlertManager.addSuccessAlert('Record for ' + $scope.entityName + ' deleted.');
					    $location.url('/' + $scope.entityName + '/search');
					},
					function onFailedDelete(errorResponse) {
					    userService.AlertManager.addFailureAlert('Record for ' + $scope.entityName + ' failed to delete.' + [JSON.stringify(errorResponse)]);
					});
		    };

		}
	]);

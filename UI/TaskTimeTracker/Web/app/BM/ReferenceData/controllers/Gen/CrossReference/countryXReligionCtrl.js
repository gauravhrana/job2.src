'use strict';

angular.module('rootAppShell')
	.controller('countryXReligionCtrl', [
		'$location', '$scope', '$injector', '$routeParams', 'userService', 'countryXReligionService',
		function ($location, $scope, $injector, $routeParams, userService, countryXReligionService) {

			$scope.entityDisplayName = 'Country X Religion';

			$scope.sourceEntity = 'Country';

			function onFailedLoad(serverResponse) {
				alert(serverResponse.data);
				userService.AlertManager.logFailureAlert('', serverResponse.data, []);
			} 

			$scope.resetSourceEntity = function () {
				if ($scope.sourceEntity == 'Country') {
					$scope.targetEntity = 'Religion';
				} else {
					$scope.targetEntity = 'Country';
				}
				// get source entity items
				countryXReligionService.getSourceEntityList({ value: JSON.stringify($scope.sourceEntity) }, onSuccessLoadSourceEntityList, onFailedLoad);
			};

			$scope.resetSourceEntity();

			$scope.loadTargetEntity = function () {
				countryXReligionService.getSourceEntityList(
					{
						value: JSON.stringify($scope.targetEntity)
					},
					onSuccessLoadTargetEntityList, onFailedLoad);
			}

			// on successfull load of source entity list items
			function onSuccessLoadSourceEntityList(data) {

				$scope.sourceEntityList = data;
				$scope.sourceEntityItem = data[0];

				$scope.loadTargetEntity();
			}

			function onSuccessLoadTargetEntityList(data) {

				$scope.possibleTargetEntityList = data;

				var entityKey = 0;
				if ($scope.sourceEntityItem.EntityKey != undefined) {
					entityKey = $scope.sourceEntityItem.EntityKey;
				}

				countryXReligionService.getEntityRecords(
					{
						    value: JSON.stringify($scope.sourceEntity)
						,   value1: JSON.stringify($scope.sourceEntityItem.EntityKey)
					},
					onSuccessLoadAssignedTargetEntityList, onFailedLoad);
			}

			function onSuccessLoadAssignedTargetEntityList(data) {
				$scope.assignedTargetEntityList = [];

				if (data.length > 0) {
					 _.each(data, function (dataObject) {

					var filteredResult = _.filter($scope.possibleTargetEntityList,
						function (obj) {
							return obj.EntityKey == dataObject[$scope.targetEntity + 'Id']
						});

					if (filteredResult.length > 0) {
						$scope.assignedTargetEntityList.push(filteredResult[0]);
					}

					$scope.possibleTargetEntityList = _.filter($scope.possibleTargetEntityList,
						function (obj) {
							return obj.EntityKey.toString() != dataObject[$scope.targetEntity + 'Id']
						});

				});
			}
		}
		$scope.moveRight = function () {
			if ($scope.possibleTargetEntityItems.length > 0) {
				_.each($scope.possibleTargetEntityItems, function (dataObject) {

					var filteredResult = _.filter($scope.possibleTargetEntityList,
						function (obj) {
							return obj.EntityKey == dataObject.EntityKey
						});

					if (filteredResult.length > 0) {
						$scope.assignedTargetEntityList.push(filteredResult[0]);
					}

					$scope.possibleTargetEntityList = _.filter($scope.possibleTargetEntityList,
						function (obj) {
							return obj.EntityKey != dataObject.EntityKey
						});

				})
			}
			else {
				alert('No Item Selected');
			}

		};

		$scope.moveLeft = function () {
			if ($scope.assignedTargetEntityItems != undefined && $scope.assignedTargetEntityItems.length > 0) {
				_.each($scope.assignedTargetEntityItems, function (dataObject) {

					var filteredResult = _.filter($scope.assignedTargetEntityList,
						function (obj) {
							return obj.EntityKey == dataObject.EntityKey
						});

					if (filteredResult.length > 0) {
						$scope.possibleTargetEntityList.push(filteredResult[0]);
					}

					$scope.assignedTargetEntityList = _.filter($scope.assignedTargetEntityList,
						function (obj) {
							return obj.EntityKey != dataObject.EntityKey
						});

				})
			}
			else {
				alert('No Item Selected');
			}
		};

		$scope.saveEntityRecords = function () {
			countryXReligionService.removeEntityRecords(
				{
					value: JSON.stringify($scope.sourceEntity)
					, value1: JSON.stringify($scope.sourceEntityItem.EntityKey)
				},
				onSuccessRemoveRecords, onFailedLoad);

			};

			function onSuccessRemoveRecords(data) {
				if (data.Result == true) {
					if ($scope.assignedTargetEntityList.length > 0) {

						var assignedIds = [];
						_.each($scope.assignedTargetEntityList, function (dataObject) {
							assignedIds.push(dataObject.EntityKey);
						});

						countryXReligionService.addEntityRecords(
						{
							value1: JSON.stringify($scope.sourceEntity)
							, value2: JSON.stringify($scope.sourceEntityItem.EntityKey)
							, value3: JSON.stringify(assignedIds)
						},
						onSuccessAddRecords, onFailedLoad);
					}
				}
			}

			function onSuccessAddRecords(data) {
				if (data.Result == true) {
					userService.AlertManager.addSuccessAlert('Records Saved.');
				}
			}

		}
	]
);

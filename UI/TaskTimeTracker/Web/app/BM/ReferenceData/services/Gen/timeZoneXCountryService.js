'use strict';

angular.module('rootAppShell')
	.factory('timeZoneXCountryService', [
		'$resource',
		function ($resource) {
			return $resource('./api/timeZoneXCountry/:detailId'
				, null
				, {
					   'getSourceEntityList': { method: 'GET', url: '../../apiV2/timeZoneXCountry/GetSourceEntityList/:value', isArray: true }
					,   'getEntityRecords': { method: 'GET', url: '../../apiV2/timeZoneXCountry/GetEntityRecords/:value/:value1', isArray: true }
					,   'addEntityRecords': { method: 'GET', url: '../../apiV2/timeZoneXCountry/AddEntityRecords/:value1/:value2/:value3' }
					,   'removeEntityRecords': { method: 'GET', url: '../../apiV2/timeZoneXCountry/RemoveEntityRecords/:value/:value1' }
				}
				);
			}
		]);

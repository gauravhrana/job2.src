		'use strict';

		angular.module('rootAppShell')
			.factory('holidayXCountryService', [
				'$resource',
				function ($resource) {
					return $resource('./api/holidayXCountry/:detailId'
						, null
						, {
							   'getSourceEntityList': { method: 'GET', url: '../../apiV2/holidayXCountry/GetSourceEntityList/:value', isArray: true }
							,   'getEntityRecords': { method: 'GET', url: '../../apiV2/holidayXCountry/GetEntityRecords/:value/:value1', isArray: true }
							,   'addEntityRecords': { method: 'GET', url: '../../apiV2/holidayXCountry/AddEntityRecords/:value1/:value2/:value3' }
							,   'removeEntityRecords': { method: 'GET', url: '../../apiV2/holidayXCountry/RemoveEntityRecords/:value/:value1' }
						}
						);
					}
				]);

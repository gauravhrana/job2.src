				'use strict';

				angular.module('rootAppShell')
					.factory('countryXReligionService', [
						'$resource',
						function ($resource) {
							return $resource('./api/countryXReligion/:detailId'
								, null
								, {
									   'getSourceEntityList': { method: 'GET', url: '../../apiV2/countryXReligion/GetSourceEntityList/:value', isArray: true }
									,   'getEntityRecords': { method: 'GET', url: '../../apiV2/countryXReligion/GetEntityRecords/:value/:value1', isArray: true }
									,   'addEntityRecords': { method: 'GET', url: '../../apiV2/countryXReligion/AddEntityRecords/:value1/:value2/:value3' }
									,   'removeEntityRecords': { method: 'GET', url: '../../apiV2/countryXReligion/RemoveEntityRecords/:value/:value1' }
								}
								);
							}
						]);

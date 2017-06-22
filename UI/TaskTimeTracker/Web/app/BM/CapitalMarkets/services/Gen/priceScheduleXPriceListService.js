				'use strict';

				angular.module('rootAppShell')
					.factory('priceScheduleXPriceListService', [
						'$resource',
						function ($resource) {
							return $resource('./api/priceScheduleXPriceList/:detailId'
								, null
								, {
									   'getSourceEntityList': { method: 'GET', url: '../../apiV2/priceScheduleXPriceList/GetSourceEntityList/:value', isArray: true }
									,   'getEntityRecords': { method: 'GET', url: '../../apiV2/priceScheduleXPriceList/GetEntityRecords/:value/:value1', isArray: true }
									,   'addEntityRecords': { method: 'GET', url: '../../apiV2/priceScheduleXPriceList/AddEntityRecords/:value1/:value2/:value3' }
									,   'removeEntityRecords': { method: 'GET', url: '../../apiV2/priceScheduleXPriceList/RemoveEntityRecords/:value/:value1' }
								}
								);
							}
						]);

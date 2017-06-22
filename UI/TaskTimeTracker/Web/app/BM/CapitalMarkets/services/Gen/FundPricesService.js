		'use strict';

		angular.module('rootAppShell')
			.factory('fundPricesService', [
				'$resource',
				function ($resource) {
					return $resource('./api/fundPrices/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/fundPrices/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/fundPrices/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/fundPrices/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/fundPrices/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/fundPrices/Delete/:detailId' }
						}
						);
					}
				]);

'use strict';

angular.module('rootAppShell')
	.factory('investmentPricesService', [
		'$resource',
		function ($resource) {
			return $resource('./api/investmentPrices/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/investmentPrices/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/investmentPrices/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/investmentPrices/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/investmentPrices/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/investmentPrices/Delete/:detailId' }
				}
				);
			}
		]);

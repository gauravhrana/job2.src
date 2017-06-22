'use strict';

angular.module('rootAppShell')
	.factory('swapInvestmentService', [
		'$resource',
		function ($resource) {
			return $resource('./api/swapInvestment/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/swapInvestment/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/swapInvestment/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/swapInvestment/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/swapInvestment/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/swapInvestment/Delete/:detailId' }
				}
				);
			}
		]);

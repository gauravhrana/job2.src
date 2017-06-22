		'use strict';

		angular.module('rootAppShell')
			.factory('txTradeAndSettleDatesService', [
				'$resource',
				function ($resource) {
					return $resource('./api/txTradeAndSettleDates/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/txTradeAndSettleDates/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/txTradeAndSettleDates/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/txTradeAndSettleDates/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/txTradeAndSettleDates/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/txTradeAndSettleDates/Delete/:detailId' }
						}
						);
					}
				]);

														'use strict';

														angular.module('rootAppShell')
															.factory('transactionEventBuyService', [
																'$resource',
																function ($resource) {
																	return $resource('./api/transactionEventBuy/:detailId'
																		, null
																		, {
																				'getById': { method: 'GET', url: '../../apiV2/transactionEventBuy/GetById/:detailId' }
																			,   'getList': { method: 'GET', url: '../../apiV2/transactionEventBuy/GetList', isArray: true }
																			,   'create': { method: 'POST', url: '../../apiV2/transactionEventBuy/Create' }
																			,   'update': { method: 'POST', url: '../../apiV2/transactionEventBuy/Update' }
																			,   'delete': { method: 'DELETE', url: '../../apiV2/transactionEventBuy/Delete/:detailId' }
																		}
																		);
																	}
																]);

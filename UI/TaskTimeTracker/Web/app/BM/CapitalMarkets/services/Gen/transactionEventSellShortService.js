																		'use strict';

																		angular.module('rootAppShell')
																			.factory('transactionEventSellShortService', [
																				'$resource',
																				function ($resource) {
																					return $resource('./api/transactionEventSellShort/:detailId'
																						, null
																						, {
																								'getById': { method: 'GET', url: '../../apiV2/transactionEventSellShort/GetById/:detailId' }
																							,   'getList': { method: 'GET', url: '../../apiV2/transactionEventSellShort/GetList', isArray: true }
																							,   'create': { method: 'POST', url: '../../apiV2/transactionEventSellShort/Create' }
																							,   'update': { method: 'POST', url: '../../apiV2/transactionEventSellShort/Update' }
																							,   'delete': { method: 'DELETE', url: '../../apiV2/transactionEventSellShort/Delete/:detailId' }
																						}
																						);
																					}
																				]);

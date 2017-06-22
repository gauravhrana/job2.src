																'use strict';

																angular.module('rootAppShell')
																	.factory('transactionEventSellService', [
																		'$resource',
																		function ($resource) {
																			return $resource('./api/transactionEventSell/:detailId'
																				, null
																				, {
																						'getById': { method: 'GET', url: '../../apiV2/transactionEventSell/GetById/:detailId' }
																					,   'getList': { method: 'GET', url: '../../apiV2/transactionEventSell/GetList', isArray: true }
																					,   'create': { method: 'POST', url: '../../apiV2/transactionEventSell/Create' }
																					,   'update': { method: 'POST', url: '../../apiV2/transactionEventSell/Update' }
																					,   'delete': { method: 'DELETE', url: '../../apiV2/transactionEventSell/Delete/:detailId' }
																				}
																				);
																			}
																		]);

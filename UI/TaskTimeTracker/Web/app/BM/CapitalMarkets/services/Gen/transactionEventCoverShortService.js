																				'use strict';

																				angular.module('rootAppShell')
																					.factory('transactionEventCoverShortService', [
																						'$resource',
																						function ($resource) {
																							return $resource('./api/transactionEventCoverShort/:detailId'
																								, null
																								, {
																										'getById': { method: 'GET', url: '../../apiV2/transactionEventCoverShort/GetById/:detailId' }
																									,   'getList': { method: 'GET', url: '../../apiV2/transactionEventCoverShort/GetList', isArray: true }
																									,   'create': { method: 'POST', url: '../../apiV2/transactionEventCoverShort/Create' }
																									,   'update': { method: 'POST', url: '../../apiV2/transactionEventCoverShort/Update' }
																									,   'delete': { method: 'DELETE', url: '../../apiV2/transactionEventCoverShort/Delete/:detailId' }
																								}
																								);
																							}
																						]);

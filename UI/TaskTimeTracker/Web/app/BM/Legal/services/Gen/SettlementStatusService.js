																				'use strict';

																				angular.module('rootAppShell')
																					.factory('settlementStatusService', [
																						'$resource',
																						function ($resource) {
																							return $resource('./api/settlementStatus/:detailId'
																								, null
																								, {
																										'getById': { method: 'GET', url: '../../apiV2/settlementStatus/GetById/:detailId' }
																									,   'getList': { method: 'GET', url: '../../apiV2/settlementStatus/GetList', isArray: true }
																									,   'create': { method: 'POST', url: '../../apiV2/settlementStatus/Create' }
																									,   'update': { method: 'POST', url: '../../apiV2/settlementStatus/Update' }
																									,   'delete': { method: 'DELETE', url: '../../apiV2/settlementStatus/Delete/:detailId' }
																								}
																								);
																							}
																						]);

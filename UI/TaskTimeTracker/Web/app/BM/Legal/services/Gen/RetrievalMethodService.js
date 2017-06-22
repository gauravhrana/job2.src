																		'use strict';

																		angular.module('rootAppShell')
																			.factory('retrievalMethodService', [
																				'$resource',
																				function ($resource) {
																					return $resource('./api/retrievalMethod/:detailId'
																						, null
																						, {
																								'getById': { method: 'GET', url: '../../apiV2/retrievalMethod/GetById/:detailId' }
																							,   'getList': { method: 'GET', url: '../../apiV2/retrievalMethod/GetList', isArray: true }
																							,   'create': { method: 'POST', url: '../../apiV2/retrievalMethod/Create' }
																							,   'update': { method: 'POST', url: '../../apiV2/retrievalMethod/Update' }
																							,   'delete': { method: 'DELETE', url: '../../apiV2/retrievalMethod/Delete/:detailId' }
																						}
																						);
																					}
																				]);

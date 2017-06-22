																		'use strict';

																		angular.module('rootAppShell')
																			.factory('businessUnitService', [
																				'$resource',
																				function ($resource) {
																					return $resource('./api/businessUnit/:detailId'
																						, null
																						, {
																								'getById': { method: 'GET', url: '../../apiV2/businessUnit/GetById/:detailId' }
																							,   'getList': { method: 'GET', url: '../../apiV2/businessUnit/GetList', isArray: true }
																							,   'create': { method: 'POST', url: '../../apiV2/businessUnit/Create' }
																							,   'update': { method: 'POST', url: '../../apiV2/businessUnit/Update' }
																							,   'delete': { method: 'DELETE', url: '../../apiV2/businessUnit/Delete/:detailId' }
																						}
																						);
																					}
																				]);

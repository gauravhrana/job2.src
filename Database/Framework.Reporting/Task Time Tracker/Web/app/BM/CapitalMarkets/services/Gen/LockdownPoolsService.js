																		'use strict';

																		angular.module('rootAppShell')
																			.factory('lockdownPoolsService', [
																				'$resource',
																				function ($resource) {
																					return $resource('./api/lockdownPools/:detailId'
																						, null
																						, {
																								'getById': { method: 'GET', url: '../../apiV2/lockdownPools/GetById/:detailId' }
																							,   'getList': { method: 'GET', url: '../../apiV2/lockdownPools/GetList', isArray: true }
																							,   'create': { method: 'POST', url: '../../apiV2/lockdownPools/Create' }
																							,   'update': { method: 'POST', url: '../../apiV2/lockdownPools/Update' }
																							,   'delete': { method: 'DELETE', url: '../../apiV2/lockdownPools/Delete/:detailId' }
																						}
																						);
																					}
																				]);

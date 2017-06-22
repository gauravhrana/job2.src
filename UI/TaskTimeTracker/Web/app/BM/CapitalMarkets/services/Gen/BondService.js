																		'use strict';

																		angular.module('rootAppShell')
																			.factory('bondService', [
																				'$resource',
																				function ($resource) {
																					return $resource('./api/bond/:detailId'
																						, null
																						, {
																								'getById': { method: 'GET', url: '../../apiV2/bond/GetById/:detailId' }
																							,   'getList': { method: 'GET', url: '../../apiV2/bond/GetList', isArray: true }
																							,   'create': { method: 'POST', url: '../../apiV2/bond/Create' }
																							,   'update': { method: 'POST', url: '../../apiV2/bond/Update' }
																							,   'delete': { method: 'DELETE', url: '../../apiV2/bond/Delete/:detailId' }
																						}
																						);
																					}
																				]);

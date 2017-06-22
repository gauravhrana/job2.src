																		'use strict';

																		angular.module('rootAppShell')
																			.factory('managerService', [
																				'$resource',
																				function ($resource) {
																					return $resource('./api/manager/:detailId'
																						, null
																						, {
																								'getById': { method: 'GET', url: '../../apiV2/manager/GetById/:detailId' }
																							,   'getList': { method: 'GET', url: '../../apiV2/manager/GetList', isArray: true }
																							,   'create': { method: 'POST', url: '../../apiV2/manager/Create' }
																							,   'update': { method: 'POST', url: '../../apiV2/manager/Update' }
																							,   'delete': { method: 'DELETE', url: '../../apiV2/manager/Delete/:detailId' }
																						}
																						);
																					}
																				]);

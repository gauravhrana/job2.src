																		'use strict';

																		angular.module('rootAppShell')
																			.factory('eventTypeService', [
																				'$resource',
																				function ($resource) {
																					return $resource('./api/eventType/:detailId'
																						, null
																						, {
																								'getById': { method: 'GET', url: '../../apiV2/eventType/GetById/:detailId' }
																							,   'getList': { method: 'GET', url: '../../apiV2/eventType/GetList', isArray: true }
																							,   'create': { method: 'POST', url: '../../apiV2/eventType/Create' }
																							,   'update': { method: 'POST', url: '../../apiV2/eventType/Update' }
																							,   'delete': { method: 'DELETE', url: '../../apiV2/eventType/Delete/:detailId' }
																						}
																						);
																					}
																				]);

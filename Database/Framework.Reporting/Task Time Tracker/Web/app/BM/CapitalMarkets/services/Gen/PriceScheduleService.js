																'use strict';

																angular.module('rootAppShell')
																	.factory('priceScheduleService', [
																		'$resource',
																		function ($resource) {
																			return $resource('./api/priceSchedule/:detailId'
																				, null
																				, {
																						'getById': { method: 'GET', url: '../../apiV2/priceSchedule/GetById/:detailId' }
																					,   'getList': { method: 'GET', url: '../../apiV2/priceSchedule/GetList', isArray: true }
																					,   'create': { method: 'POST', url: '../../apiV2/priceSchedule/Create' }
																					,   'update': { method: 'POST', url: '../../apiV2/priceSchedule/Update' }
																					,   'delete': { method: 'DELETE', url: '../../apiV2/priceSchedule/Delete/:detailId' }
																				}
																				);
																			}
																		]);

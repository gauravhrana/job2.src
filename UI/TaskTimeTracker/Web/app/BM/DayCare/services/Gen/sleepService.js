																																'use strict';

																																angular.module('rootAppShell')
																																	.factory('sleepService', [
																																		'$resource',
																																		function ($resource) {
																																			return $resource('./api/sleep/:detailId'
																																				, null
																																				, {
																																						'getById': { method: 'GET', url: '../../apiV2/sleep/GetById/:detailId' }
																																					,   'getList': { method: 'GET', url: '../../apiV2/sleep/GetList', isArray: true }
																																					,   'create': { method: 'POST', url: '../../apiV2/sleep/Create' }
																																					,   'update': { method: 'POST', url: '../../apiV2/sleep/Update' }
																																					,   'delete': { method: 'DELETE', url: '../../apiV2/sleep/Delete/:detailId' }
																																				}
																																				);
																																			}
																																		]);

																'use strict';

																angular.module('rootAppShell')
																	.factory('diaperStatusService', [
																		'$resource',
																		function ($resource) {
																			return $resource('./api/diaperStatus/:detailId'
																				, null
																				, {
																						'getById': { method: 'GET', url: '../../apiV2/diaperStatus/GetById/:detailId' }
																					,   'getList': { method: 'GET', url: '../../apiV2/diaperStatus/GetList', isArray: true }
																					,   'create': { method: 'POST', url: '../../apiV2/diaperStatus/Create' }
																					,   'update': { method: 'POST', url: '../../apiV2/diaperStatus/Update' }
																					,   'delete': { method: 'DELETE', url: '../../apiV2/diaperStatus/Delete/:detailId' }
																				}
																				);
																			}
																		]);

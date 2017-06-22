																																		'use strict';

																																		angular.module('rootAppShell')
																																			.factory('ethnicityService', [
																																				'$resource',
																																				function ($resource) {
																																					return $resource('./api/ethnicity/:detailId'
																																						, null
																																						, {
																																								'getById': { method: 'GET', url: '../../apiV2/ethnicity/GetById/:detailId' }
																																							,   'getList': { method: 'GET', url: '../../apiV2/ethnicity/GetList', isArray: true }
																																							,   'create': { method: 'POST', url: '../../apiV2/ethnicity/Create' }
																																							,   'update': { method: 'POST', url: '../../apiV2/ethnicity/Update' }
																																							,   'delete': { method: 'DELETE', url: '../../apiV2/ethnicity/Delete/:detailId' }
																																						}
																																						);
																																					}
																																				]);

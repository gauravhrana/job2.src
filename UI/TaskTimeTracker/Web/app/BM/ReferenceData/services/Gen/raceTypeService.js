																																		'use strict';

																																		angular.module('rootAppShell')
																																			.factory('raceTypeService', [
																																				'$resource',
																																				function ($resource) {
																																					return $resource('./api/raceType/:detailId'
																																						, null
																																						, {
																																								'getById': { method: 'GET', url: '../../apiV2/raceType/GetById/:detailId' }
																																							,   'getList': { method: 'GET', url: '../../apiV2/raceType/GetList', isArray: true }
																																							,   'create': { method: 'POST', url: '../../apiV2/raceType/Create' }
																																							,   'update': { method: 'POST', url: '../../apiV2/raceType/Update' }
																																							,   'delete': { method: 'DELETE', url: '../../apiV2/raceType/Delete/:detailId' }
																																						}
																																						);
																																					}
																																				]);

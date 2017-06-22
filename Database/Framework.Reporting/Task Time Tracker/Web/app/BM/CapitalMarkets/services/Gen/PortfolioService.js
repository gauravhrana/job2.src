																		'use strict';

																		angular.module('rootAppShell')
																			.factory('portfolioService', [
																				'$resource',
																				function ($resource) {
																					return $resource('./api/portfolio/:detailId'
																						, null
																						, {
																								'getById': { method: 'GET', url: '../../apiV2/portfolio/GetById/:detailId' }
																							,   'getList': { method: 'GET', url: '../../apiV2/portfolio/GetList', isArray: true }
																							,   'create': { method: 'POST', url: '../../apiV2/portfolio/Create' }
																							,   'update': { method: 'POST', url: '../../apiV2/portfolio/Update' }
																							,   'delete': { method: 'DELETE', url: '../../apiV2/portfolio/Delete/:detailId' }
																						}
																						);
																					}
																				]);

																'use strict';

																angular.module('rootAppShell')
																	.factory('portfolioTypeService', [
																		'$resource',
																		function ($resource) {
																			return $resource('./api/portfolioType/:detailId'
																				, null
																				, {
																						'getById': { method: 'GET', url: '../../apiV2/portfolioType/GetById/:detailId' }
																					,   'getList': { method: 'GET', url: '../../apiV2/portfolioType/GetList', isArray: true }
																					,   'create': { method: 'POST', url: '../../apiV2/portfolioType/Create' }
																					,   'update': { method: 'POST', url: '../../apiV2/portfolioType/Update' }
																					,   'delete': { method: 'DELETE', url: '../../apiV2/portfolioType/Delete/:detailId' }
																				}
																				);
																			}
																		]);

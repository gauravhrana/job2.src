																'use strict';

																angular.module('rootAppShell')
																	.factory('mallService', [
																		'$resource',
																		function ($resource) {
																			return $resource('./api/mall/:detailId'
																				, null
																				, {
																						'getById': { method: 'GET', url: '../../apiV2/mall/GetById/:detailId' }
																					,   'getList': { method: 'GET', url: '../../apiV2/mall/GetList', isArray: true }
																					,   'create': { method: 'POST', url: '../../apiV2/mall/Create' }
																					,   'update': { method: 'POST', url: '../../apiV2/mall/Update' }
																					,   'delete': { method: 'DELETE', url: '../../apiV2/mall/Delete/:detailId' }
																				}
																				);
																			}
																		]);

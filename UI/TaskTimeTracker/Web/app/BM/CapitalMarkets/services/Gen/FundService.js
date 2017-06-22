																'use strict';

																angular.module('rootAppShell')
																	.factory('fundService', [
																		'$resource',
																		function ($resource) {
																			return $resource('./api/fund/:detailId'
																				, null
																				, {
																						'getById': { method: 'GET', url: '../../apiV2/fund/GetById/:detailId' }
																					,   'getList': { method: 'GET', url: '../../apiV2/fund/GetList', isArray: true }
																					,   'create': { method: 'POST', url: '../../apiV2/fund/Create' }
																					,   'update': { method: 'POST', url: '../../apiV2/fund/Update' }
																					,   'delete': { method: 'DELETE', url: '../../apiV2/fund/Delete/:detailId' }
																				}
																				);
																			}
																		]);

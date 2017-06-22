																'use strict';

																angular.module('rootAppShell')
																	.factory('reportTypeService', [
																		'$resource',
																		function ($resource) {
																			return $resource('./api/reportType/:detailId'
																				, null
																				, {
																						'getById': { method: 'GET', url: '../../apiV2/reportType/GetById/:detailId' }
																					,   'getList': { method: 'GET', url: '../../apiV2/reportType/GetList', isArray: true }
																					,   'create': { method: 'POST', url: '../../apiV2/reportType/Create' }
																					,   'update': { method: 'POST', url: '../../apiV2/reportType/Update' }
																					,   'delete': { method: 'DELETE', url: '../../apiV2/reportType/Delete/:detailId' }
																				}
																				);
																			}
																		]);

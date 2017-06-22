																'use strict';

																angular.module('rootAppShell')
																	.factory('analystService', [
																		'$resource',
																		function ($resource) {
																			return $resource('./api/analyst/:detailId'
																				, null
																				, {
																						'getById': { method: 'GET', url: '../../apiV2/analyst/GetById/:detailId' }
																					,   'getList': { method: 'GET', url: '../../apiV2/analyst/GetList', isArray: true }
																					,   'create': { method: 'POST', url: '../../apiV2/analyst/Create' }
																					,   'update': { method: 'POST', url: '../../apiV2/analyst/Update' }
																					,   'delete': { method: 'DELETE', url: '../../apiV2/analyst/Delete/:detailId' }
																				}
																				);
																			}
																		]);

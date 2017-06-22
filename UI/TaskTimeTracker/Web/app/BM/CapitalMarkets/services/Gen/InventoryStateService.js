																'use strict';

																angular.module('rootAppShell')
																	.factory('inventoryStateService', [
																		'$resource',
																		function ($resource) {
																			return $resource('./api/inventoryState/:detailId'
																				, null
																				, {
																						'getById': { method: 'GET', url: '../../apiV2/inventoryState/GetById/:detailId' }
																					,   'getList': { method: 'GET', url: '../../apiV2/inventoryState/GetList', isArray: true }
																					,   'create': { method: 'POST', url: '../../apiV2/inventoryState/Create' }
																					,   'update': { method: 'POST', url: '../../apiV2/inventoryState/Update' }
																					,   'delete': { method: 'DELETE', url: '../../apiV2/inventoryState/Delete/:detailId' }
																				}
																				);
																			}
																		]);

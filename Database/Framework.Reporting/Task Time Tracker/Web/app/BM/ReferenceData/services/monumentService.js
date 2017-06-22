																		'use strict';

																		angular.module('rootAppShell')
																			.factory('monumentService', [
																				'$resource',
																				function ($resource) {
																					return $resource('./api/monument/:detailId'
																						, null
																						, {
																								'getById': { method: 'GET', url: '../../apiV2/monument/GetById/:detailId' }
																							,   'getList': { method: 'GET', url: '../../apiV2/monument/GetList', isArray: true }
																							,   'create': { method: 'POST', url: '../../apiV2/monument/Create' }
																							,   'update': { method: 'POST', url: '../../apiV2/monument/Update' }
																							,   'delete': { method: 'DELETE', url: '../../apiV2/monument/Delete/:detailId' }
																						}
																						);
																					}
																				]);

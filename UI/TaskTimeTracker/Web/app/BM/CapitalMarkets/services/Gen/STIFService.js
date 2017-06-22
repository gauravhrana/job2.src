																																'use strict';

																																angular.module('rootAppShell')
																																	.factory('sTIFService', [
																																		'$resource',
																																		function ($resource) {
																																			return $resource('./api/sTIF/:detailId'
																																				, null
																																				, {
																																						'getById': { method: 'GET', url: '../../apiV2/sTIF/GetById/:detailId' }
																																					,   'getList': { method: 'GET', url: '../../apiV2/sTIF/GetList', isArray: true }
																																					,   'create': { method: 'POST', url: '../../apiV2/sTIF/Create' }
																																					,   'update': { method: 'POST', url: '../../apiV2/sTIF/Update' }
																																					,   'delete': { method: 'DELETE', url: '../../apiV2/sTIF/Delete/:detailId' }
																																				}
																																				);
																																			}
																																		]);

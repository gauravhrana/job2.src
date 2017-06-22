																																'use strict';

																																angular.module('rootAppShell')
																																	.factory('personSuffixService', [
																																		'$resource',
																																		function ($resource) {
																																			return $resource('./api/personSuffix/:detailId'
																																				, null
																																				, {
																																						'getById': { method: 'GET', url: '../../apiV2/personSuffix/GetById/:detailId' }
																																					,   'getList': { method: 'GET', url: '../../apiV2/personSuffix/GetList', isArray: true }
																																					,   'create': { method: 'POST', url: '../../apiV2/personSuffix/Create' }
																																					,   'update': { method: 'POST', url: '../../apiV2/personSuffix/Update' }
																																					,   'delete': { method: 'DELETE', url: '../../apiV2/personSuffix/Delete/:detailId' }
																																				}
																																				);
																																			}
																																		]);

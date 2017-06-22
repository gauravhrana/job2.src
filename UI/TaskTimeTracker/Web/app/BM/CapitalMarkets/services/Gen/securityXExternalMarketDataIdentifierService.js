																																		'use strict';

																																		angular.module('rootAppShell')
																																			.factory('securityXExternalMarketDataIdentifierService', [
																																				'$resource',
																																				function ($resource) {
																																					return $resource('./api/securityXExternalMarketDataIdentifier/:detailId'
																																						, null
																																						, {
																																								'getById': { method: 'GET', url: '../../apiV2/securityXExternalMarketDataIdentifier/GetById/:detailId' }
																																							,   'getList': { method: 'GET', url: '../../apiV2/securityXExternalMarketDataIdentifier/GetList', isArray: true }
																																							,   'create': { method: 'POST', url: '../../apiV2/securityXExternalMarketDataIdentifier/Create' }
																																							,   'update': { method: 'POST', url: '../../apiV2/securityXExternalMarketDataIdentifier/Update' }
																																							,   'delete': { method: 'DELETE', url: '../../apiV2/securityXExternalMarketDataIdentifier/Delete/:detailId' }
																																						}
																																						);
																																					}
																																				]);

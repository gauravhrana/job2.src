																																				'use strict';

																																				angular.module('rootAppShell')
																																					.factory('securityXOtherService', [
																																						'$resource',
																																						function ($resource) {
																																							return $resource('./api/securityXOther/:detailId'
																																								, null
																																								, {
																																										'getById': { method: 'GET', url: '../../apiV2/securityXOther/GetById/:detailId' }
																																									,   'getList': { method: 'GET', url: '../../apiV2/securityXOther/GetList', isArray: true }
																																									,   'create': { method: 'POST', url: '../../apiV2/securityXOther/Create' }
																																									,   'update': { method: 'POST', url: '../../apiV2/securityXOther/Update' }
																																									,   'delete': { method: 'DELETE', url: '../../apiV2/securityXOther/Delete/:detailId' }
																																								}
																																								);
																																							}
																																						]);

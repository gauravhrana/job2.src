										'use strict';

										angular.module('rootAppShell')
											.factory('securityTypeService', [
												'$resource',
												function ($resource) {
													return $resource('./api/securityType/:detailId'
														, null
														, {
																'getById': { method: 'GET', url: '../../apiV2/securityType/GetById/:detailId' }
															,   'getList': { method: 'GET', url: '../../apiV2/securityType/GetList', isArray: true }
															,   'create': { method: 'POST', url: '../../apiV2/securityType/Create' }
															,   'update': { method: 'POST', url: '../../apiV2/securityType/Update' }
															,   'delete': { method: 'DELETE', url: '../../apiV2/securityType/Delete/:detailId' }
														}
														);
													}
												]);

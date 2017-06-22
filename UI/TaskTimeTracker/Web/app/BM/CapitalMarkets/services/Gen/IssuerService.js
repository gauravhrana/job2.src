										'use strict';

										angular.module('rootAppShell')
											.factory('issuerService', [
												'$resource',
												function ($resource) {
													return $resource('./api/issuer/:detailId'
														, null
														, {
																'getById': { method: 'GET', url: '../../apiV2/issuer/GetById/:detailId' }
															,   'getList': { method: 'GET', url: '../../apiV2/issuer/GetList', isArray: true }
															,   'create': { method: 'POST', url: '../../apiV2/issuer/Create' }
															,   'update': { method: 'POST', url: '../../apiV2/issuer/Update' }
															,   'delete': { method: 'DELETE', url: '../../apiV2/issuer/Delete/:detailId' }
														}
														);
													}
												]);

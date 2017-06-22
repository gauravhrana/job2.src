										'use strict';

										angular.module('rootAppShell')
											.factory('movantTypeService', [
												'$resource',
												function ($resource) {
													return $resource('./api/movantType/:detailId'
														, null
														, {
																'getById': { method: 'GET', url: '../../apiV2/movantType/GetById/:detailId' }
															,   'getList': { method: 'GET', url: '../../apiV2/movantType/GetList', isArray: true }
															,   'create': { method: 'POST', url: '../../apiV2/movantType/Create' }
															,   'update': { method: 'POST', url: '../../apiV2/movantType/Update' }
															,   'delete': { method: 'DELETE', url: '../../apiV2/movantType/Delete/:detailId' }
														}
														);
													}
												]);

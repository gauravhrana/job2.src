										'use strict';

										angular.module('rootAppShell')
											.factory('orderStatusTypeService', [
												'$resource',
												function ($resource) {
													return $resource('./api/orderStatusType/:detailId'
														, null
														, {
																'getById': { method: 'GET', url: '../../apiV2/orderStatusType/GetById/:detailId' }
															,   'getList': { method: 'GET', url: '../../apiV2/orderStatusType/GetList', isArray: true }
															,   'create': { method: 'POST', url: '../../apiV2/orderStatusType/Create' }
															,   'update': { method: 'POST', url: '../../apiV2/orderStatusType/Update' }
															,   'delete': { method: 'DELETE', url: '../../apiV2/orderStatusType/Delete/:detailId' }
														}
														);
													}
												]);

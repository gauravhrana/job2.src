										'use strict';

										angular.module('rootAppShell')
											.factory('discountService', [
												'$resource',
												function ($resource) {
													return $resource('./api/discount/:detailId'
														, null
														, {
																'getById': { method: 'GET', url: '../../apiV2/discount/GetById/:detailId' }
															,   'getList': { method: 'GET', url: '../../apiV2/discount/GetList', isArray: true }
															,   'create': { method: 'POST', url: '../../apiV2/discount/Create' }
															,   'update': { method: 'POST', url: '../../apiV2/discount/Update' }
															,   'delete': { method: 'DELETE', url: '../../apiV2/discount/Delete/:detailId' }
														}
														);
													}
												]);

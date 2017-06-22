										'use strict';

										angular.module('rootAppShell')
											.factory('priceProviderService', [
												'$resource',
												function ($resource) {
													return $resource('./api/priceProvider/:detailId'
														, null
														, {
																'getById': { method: 'GET', url: '../../apiV2/priceProvider/GetById/:detailId' }
															,   'getList': { method: 'GET', url: '../../apiV2/priceProvider/GetList', isArray: true }
															,   'create': { method: 'POST', url: '../../apiV2/priceProvider/Create' }
															,   'update': { method: 'POST', url: '../../apiV2/priceProvider/Update' }
															,   'delete': { method: 'DELETE', url: '../../apiV2/priceProvider/Delete/:detailId' }
														}
														);
													}
												]);

								'use strict';

								angular.module('rootAppShell')
									.factory('priceMarketService', [
										'$resource',
										function ($resource) {
											return $resource('./api/priceMarket/:detailId'
												, null
												, {
														'getById': { method: 'GET', url: '../../apiV2/priceMarket/GetById/:detailId' }
													,   'getList': { method: 'GET', url: '../../apiV2/priceMarket/GetList', isArray: true }
													,   'create': { method: 'POST', url: '../../apiV2/priceMarket/Create' }
													,   'update': { method: 'POST', url: '../../apiV2/priceMarket/Update' }
													,   'delete': { method: 'DELETE', url: '../../apiV2/priceMarket/Delete/:detailId' }
												}
												);
											}
										]);

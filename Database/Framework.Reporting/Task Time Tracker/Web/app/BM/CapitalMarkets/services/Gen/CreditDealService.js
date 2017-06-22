								'use strict';

								angular.module('rootAppShell')
									.factory('creditDealService', [
										'$resource',
										function ($resource) {
											return $resource('./api/creditDeal/:detailId'
												, null
												, {
														'getById': { method: 'GET', url: '../../apiV2/creditDeal/GetById/:detailId' }
													,   'getList': { method: 'GET', url: '../../apiV2/creditDeal/GetList', isArray: true }
													,   'create': { method: 'POST', url: '../../apiV2/creditDeal/Create' }
													,   'update': { method: 'POST', url: '../../apiV2/creditDeal/Update' }
													,   'delete': { method: 'DELETE', url: '../../apiV2/creditDeal/Delete/:detailId' }
												}
												);
											}
										]);

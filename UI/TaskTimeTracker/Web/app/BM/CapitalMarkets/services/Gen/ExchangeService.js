												'use strict';

												angular.module('rootAppShell')
													.factory('exchangeService', [
														'$resource',
														function ($resource) {
															return $resource('./api/exchange/:detailId'
																, null
																, {
																		'getById': { method: 'GET', url: '../../apiV2/exchange/GetById/:detailId' }
																	,   'getList': { method: 'GET', url: '../../apiV2/exchange/GetList', isArray: true }
																	,   'create': { method: 'POST', url: '../../apiV2/exchange/Create' }
																	,   'update': { method: 'POST', url: '../../apiV2/exchange/Update' }
																	,   'delete': { method: 'DELETE', url: '../../apiV2/exchange/Delete/:detailId' }
																}
																);
															}
														]);

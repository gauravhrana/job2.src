												'use strict';

												angular.module('rootAppShell')
													.factory('orderStatusService', [
														'$resource',
														function ($resource) {
															return $resource('./api/orderStatus/:detailId'
																, null
																, {
																		'getById': { method: 'GET', url: '../../apiV2/orderStatus/GetById/:detailId' }
																	,   'getList': { method: 'GET', url: '../../apiV2/orderStatus/GetList', isArray: true }
																	,   'create': { method: 'POST', url: '../../apiV2/orderStatus/Create' }
																	,   'update': { method: 'POST', url: '../../apiV2/orderStatus/Update' }
																	,   'delete': { method: 'DELETE', url: '../../apiV2/orderStatus/Delete/:detailId' }
																}
																);
															}
														]);

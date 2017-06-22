												'use strict';

												angular.module('rootAppShell')
													.factory('paymentMethodService', [
														'$resource',
														function ($resource) {
															return $resource('./api/paymentMethod/:detailId'
																, null
																, {
																		'getById': { method: 'GET', url: '../../apiV2/paymentMethod/GetById/:detailId' }
																	,   'getList': { method: 'GET', url: '../../apiV2/paymentMethod/GetList', isArray: true }
																	,   'create': { method: 'POST', url: '../../apiV2/paymentMethod/Create' }
																	,   'update': { method: 'POST', url: '../../apiV2/paymentMethod/Update' }
																	,   'delete': { method: 'DELETE', url: '../../apiV2/paymentMethod/Delete/:detailId' }
																}
																);
															}
														]);

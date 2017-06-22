												'use strict';

												angular.module('rootAppShell')
													.factory('priceSourceService', [
														'$resource',
														function ($resource) {
															return $resource('./api/priceSource/:detailId'
																, null
																, {
																		'getById': { method: 'GET', url: '../../apiV2/priceSource/GetById/:detailId' }
																	,   'getList': { method: 'GET', url: '../../apiV2/priceSource/GetList', isArray: true }
																	,   'create': { method: 'POST', url: '../../apiV2/priceSource/Create' }
																	,   'update': { method: 'POST', url: '../../apiV2/priceSource/Update' }
																	,   'delete': { method: 'DELETE', url: '../../apiV2/priceSource/Delete/:detailId' }
																}
																);
															}
														]);

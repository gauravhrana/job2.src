												'use strict';

												angular.module('rootAppShell')
													.factory('cityService', [
														'$resource',
														function ($resource) {
															return $resource('./api/city/:detailId'
																, null
																, {
																		'getById': { method: 'GET', url: '../../apiV2/city/GetById/:detailId' }
																	,   'getList': { method: 'GET', url: '../../apiV2/city/GetList', isArray: true }
																	,   'create': { method: 'POST', url: '../../apiV2/city/Create' }
																	,   'update': { method: 'POST', url: '../../apiV2/city/Update' }
																	,   'delete': { method: 'DELETE', url: '../../apiV2/city/Delete/:detailId' }
																}
																);
															}
														]);

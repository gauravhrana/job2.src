												'use strict';

												angular.module('rootAppShell')
													.factory('creditContractService', [
														'$resource',
														function ($resource) {
															return $resource('./api/creditContract/:detailId'
																, null
																, {
																		'getById': { method: 'GET', url: '../../apiV2/creditContract/GetById/:detailId' }
																	,   'getList': { method: 'GET', url: '../../apiV2/creditContract/GetList', isArray: true }
																	,   'create': { method: 'POST', url: '../../apiV2/creditContract/Create' }
																	,   'update': { method: 'POST', url: '../../apiV2/creditContract/Update' }
																	,   'delete': { method: 'DELETE', url: '../../apiV2/creditContract/Delete/:detailId' }
																}
																);
															}
														]);

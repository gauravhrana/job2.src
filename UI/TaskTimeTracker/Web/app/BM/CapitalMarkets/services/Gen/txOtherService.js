												'use strict';

												angular.module('rootAppShell')
													.factory('txOtherService', [
														'$resource',
														function ($resource) {
															return $resource('./api/txOther/:detailId'
																, null
																, {
																		'getById': { method: 'GET', url: '../../apiV2/txOther/GetById/:detailId' }
																	,   'getList': { method: 'GET', url: '../../apiV2/txOther/GetList', isArray: true }
																	,   'create': { method: 'POST', url: '../../apiV2/txOther/Create' }
																	,   'update': { method: 'POST', url: '../../apiV2/txOther/Update' }
																	,   'delete': { method: 'DELETE', url: '../../apiV2/txOther/Delete/:detailId' }
																}
																);
															}
														]);

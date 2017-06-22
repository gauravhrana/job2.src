												'use strict';

												angular.module('rootAppShell')
													.factory('securityTypeGroupService', [
														'$resource',
														function ($resource) {
															return $resource('./api/securityTypeGroup/:detailId'
																, null
																, {
																		'getById': { method: 'GET', url: '../../apiV2/securityTypeGroup/GetById/:detailId' }
																	,   'getList': { method: 'GET', url: '../../apiV2/securityTypeGroup/GetList', isArray: true }
																	,   'create': { method: 'POST', url: '../../apiV2/securityTypeGroup/Create' }
																	,   'update': { method: 'POST', url: '../../apiV2/securityTypeGroup/Update' }
																	,   'delete': { method: 'DELETE', url: '../../apiV2/securityTypeGroup/Delete/:detailId' }
																}
																);
															}
														]);

																												'use strict';

																												angular.module('rootAppShell')
																													.factory('adjustmentCategoryService', [
																														'$resource',
																														function ($resource) {
																															return $resource('./api/adjustmentCategory/:detailId'
																																, null
																																, {
																																		'getById': { method: 'GET', url: '../../apiV2/adjustmentCategory/GetById/:detailId' }
																																	,   'getList': { method: 'GET', url: '../../apiV2/adjustmentCategory/GetList', isArray: true }
																																	,   'create': { method: 'POST', url: '../../apiV2/adjustmentCategory/Create' }
																																	,   'update': { method: 'POST', url: '../../apiV2/adjustmentCategory/Update' }
																																	,   'delete': { method: 'DELETE', url: '../../apiV2/adjustmentCategory/Delete/:detailId' }
																																}
																																);
																															}
																														]);

																												'use strict';

																												angular.module('rootAppShell')
																													.factory('forwardCashService', [
																														'$resource',
																														function ($resource) {
																															return $resource('./api/forwardCash/:detailId'
																																, null
																																, {
																																		'getById': { method: 'GET', url: '../../apiV2/forwardCash/GetById/:detailId' }
																																	,   'getList': { method: 'GET', url: '../../apiV2/forwardCash/GetList', isArray: true }
																																	,   'create': { method: 'POST', url: '../../apiV2/forwardCash/Create' }
																																	,   'update': { method: 'POST', url: '../../apiV2/forwardCash/Update' }
																																	,   'delete': { method: 'DELETE', url: '../../apiV2/forwardCash/Delete/:detailId' }
																																}
																																);
																															}
																														]);

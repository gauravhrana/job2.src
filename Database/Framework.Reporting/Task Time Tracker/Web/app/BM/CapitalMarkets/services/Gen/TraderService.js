																										'use strict';

																										angular.module('rootAppShell')
																											.factory('traderService', [
																												'$resource',
																												function ($resource) {
																													return $resource('./api/trader/:detailId'
																														, null
																														, {
																																'getById': { method: 'GET', url: '../../apiV2/trader/GetById/:detailId' }
																															,   'getList': { method: 'GET', url: '../../apiV2/trader/GetList', isArray: true }
																															,   'create': { method: 'POST', url: '../../apiV2/trader/Create' }
																															,   'update': { method: 'POST', url: '../../apiV2/trader/Update' }
																															,   'delete': { method: 'DELETE', url: '../../apiV2/trader/Delete/:detailId' }
																														}
																														);
																													}
																												]);

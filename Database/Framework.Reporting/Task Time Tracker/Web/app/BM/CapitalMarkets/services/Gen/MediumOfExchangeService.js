																										'use strict';

																										angular.module('rootAppShell')
																											.factory('mediumOfExchangeService', [
																												'$resource',
																												function ($resource) {
																													return $resource('./api/mediumOfExchange/:detailId'
																														, null
																														, {
																																'getById': { method: 'GET', url: '../../apiV2/mediumOfExchange/GetById/:detailId' }
																															,   'getList': { method: 'GET', url: '../../apiV2/mediumOfExchange/GetList', isArray: true }
																															,   'create': { method: 'POST', url: '../../apiV2/mediumOfExchange/Create' }
																															,   'update': { method: 'POST', url: '../../apiV2/mediumOfExchange/Update' }
																															,   'delete': { method: 'DELETE', url: '../../apiV2/mediumOfExchange/Delete/:detailId' }
																														}
																														);
																													}
																												]);

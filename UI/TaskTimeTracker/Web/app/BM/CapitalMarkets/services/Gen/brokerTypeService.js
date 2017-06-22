																								'use strict';

																								angular.module('rootAppShell')
																									.factory('brokerTypeService', [
																										'$resource',
																										function ($resource) {
																											return $resource('./api/brokerType/:detailId'
																												, null
																												, {
																														'getById': { method: 'GET', url: '../../apiV2/brokerType/GetById/:detailId' }
																													,   'getList': { method: 'GET', url: '../../apiV2/brokerType/GetList', isArray: true }
																													,   'create': { method: 'POST', url: '../../apiV2/brokerType/Create' }
																													,   'update': { method: 'POST', url: '../../apiV2/brokerType/Update' }
																													,   'delete': { method: 'DELETE', url: '../../apiV2/brokerType/Delete/:detailId' }
																												}
																												);
																											}
																										]);

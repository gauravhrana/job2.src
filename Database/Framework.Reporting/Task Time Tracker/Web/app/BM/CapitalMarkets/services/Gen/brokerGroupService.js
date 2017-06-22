																						'use strict';

																						angular.module('rootAppShell')
																							.factory('brokerGroupService', [
																								'$resource',
																								function ($resource) {
																									return $resource('./api/brokerGroup/:detailId'
																										, null
																										, {
																												'getById': { method: 'GET', url: '../../apiV2/brokerGroup/GetById/:detailId' }
																											,   'getList': { method: 'GET', url: '../../apiV2/brokerGroup/GetList', isArray: true }
																											,   'create': { method: 'POST', url: '../../apiV2/brokerGroup/Create' }
																											,   'update': { method: 'POST', url: '../../apiV2/brokerGroup/Update' }
																											,   'delete': { method: 'DELETE', url: '../../apiV2/brokerGroup/Delete/:detailId' }
																										}
																										);
																									}
																								]);

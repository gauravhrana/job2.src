																						'use strict';

																						angular.module('rootAppShell')
																							.factory('strategyService', [
																								'$resource',
																								function ($resource) {
																									return $resource('./api/strategy/:detailId'
																										, null
																										, {
																												'getById': { method: 'GET', url: '../../apiV2/strategy/GetById/:detailId' }
																											,   'getList': { method: 'GET', url: '../../apiV2/strategy/GetList', isArray: true }
																											,   'create': { method: 'POST', url: '../../apiV2/strategy/Create' }
																											,   'update': { method: 'POST', url: '../../apiV2/strategy/Update' }
																											,   'delete': { method: 'DELETE', url: '../../apiV2/strategy/Delete/:detailId' }
																										}
																										);
																									}
																								]);

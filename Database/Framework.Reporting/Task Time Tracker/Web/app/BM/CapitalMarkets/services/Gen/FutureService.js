																						'use strict';

																						angular.module('rootAppShell')
																							.factory('futureService', [
																								'$resource',
																								function ($resource) {
																									return $resource('./api/future/:detailId'
																										, null
																										, {
																												'getById': { method: 'GET', url: '../../apiV2/future/GetById/:detailId' }
																											,   'getList': { method: 'GET', url: '../../apiV2/future/GetList', isArray: true }
																											,   'create': { method: 'POST', url: '../../apiV2/future/Create' }
																											,   'update': { method: 'POST', url: '../../apiV2/future/Update' }
																											,   'delete': { method: 'DELETE', url: '../../apiV2/future/Delete/:detailId' }
																										}
																										);
																									}
																								]);

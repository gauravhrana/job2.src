																						'use strict';

																						angular.module('rootAppShell')
																							.factory('stateService', [
																								'$resource',
																								function ($resource) {
																									return $resource('./api/state/:detailId'
																										, null
																										, {
																												'getById': { method: 'GET', url: '../../apiV2/state/GetById/:detailId' }
																											,   'getList': { method: 'GET', url: '../../apiV2/state/GetList', isArray: true }
																											,   'create': { method: 'POST', url: '../../apiV2/state/Create' }
																											,   'update': { method: 'POST', url: '../../apiV2/state/Update' }
																											,   'delete': { method: 'DELETE', url: '../../apiV2/state/Delete/:detailId' }
																										}
																										);
																									}
																								]);

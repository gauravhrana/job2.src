																								'use strict';

																								angular.module('rootAppShell')
																									.factory('strategyGroupService', [
																										'$resource',
																										function ($resource) {
																											return $resource('./api/strategyGroup/:detailId'
																												, null
																												, {
																														'getById': { method: 'GET', url: '../../apiV2/strategyGroup/GetById/:detailId' }
																													,   'getList': { method: 'GET', url: '../../apiV2/strategyGroup/GetList', isArray: true }
																													,   'create': { method: 'POST', url: '../../apiV2/strategyGroup/Create' }
																													,   'update': { method: 'POST', url: '../../apiV2/strategyGroup/Update' }
																													,   'delete': { method: 'DELETE', url: '../../apiV2/strategyGroup/Delete/:detailId' }
																												}
																												);
																											}
																										]);

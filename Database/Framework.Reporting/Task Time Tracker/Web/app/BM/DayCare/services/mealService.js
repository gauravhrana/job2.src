																								'use strict';

																								angular.module('rootAppShell')
																									.factory('mealService', [
																										'$resource',
																										function ($resource) {
																											return $resource('./api/meal/:detailId'
																												, null
																												, {
																														'getById': { method: 'GET', url: '../../apiV2/meal/GetById/:detailId' }
																													,   'getList': { method: 'GET', url: '../../apiV2/meal/GetList', isArray: true }
																													,   'create': { method: 'POST', url: '../../apiV2/meal/Create' }
																													,   'update': { method: 'POST', url: '../../apiV2/meal/Update' }
																													,   'delete': { method: 'DELETE', url: '../../apiV2/meal/Delete/:detailId' }
																												}
																												);
																											}
																										]);

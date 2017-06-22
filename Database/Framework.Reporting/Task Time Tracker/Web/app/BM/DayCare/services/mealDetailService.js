																										'use strict';

																										angular.module('rootAppShell')
																											.factory('mealDetailService', [
																												'$resource',
																												function ($resource) {
																													return $resource('./api/mealDetail/:detailId'
																														, null
																														, {
																																'getById': { method: 'GET', url: '../../apiV2/mealDetail/GetById/:detailId' }
																															,   'getList': { method: 'GET', url: '../../apiV2/mealDetail/GetList', isArray: true }
																															,   'create': { method: 'POST', url: '../../apiV2/mealDetail/Create' }
																															,   'update': { method: 'POST', url: '../../apiV2/mealDetail/Update' }
																															,   'delete': { method: 'DELETE', url: '../../apiV2/mealDetail/Delete/:detailId' }
																														}
																														);
																													}
																												]);

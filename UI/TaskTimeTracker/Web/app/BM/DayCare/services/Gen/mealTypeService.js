																				'use strict';

																				angular.module('rootAppShell')
																					.factory('mealTypeService', [
																						'$resource',
																						function ($resource) {
																							return $resource('./api/mealType/:detailId'
																								, null
																								, {
																										'getById': { method: 'GET', url: '../../apiV2/mealType/GetById/:detailId' }
																									,   'getList': { method: 'GET', url: '../../apiV2/mealType/GetList', isArray: true }
																									,   'create': { method: 'POST', url: '../../apiV2/mealType/Create' }
																									,   'update': { method: 'POST', url: '../../apiV2/mealType/Update' }
																									,   'delete': { method: 'DELETE', url: '../../apiV2/mealType/Delete/:detailId' }
																								}
																								);
																							}
																						]);

																				'use strict';

																				angular.module('rootAppShell')
																					.factory('subBusinessUnitService', [
																						'$resource',
																						function ($resource) {
																							return $resource('./api/subBusinessUnit/:detailId'
																								, null
																								, {
																										'getById': { method: 'GET', url: '../../apiV2/subBusinessUnit/GetById/:detailId' }
																									,   'getList': { method: 'GET', url: '../../apiV2/subBusinessUnit/GetList', isArray: true }
																									,   'create': { method: 'POST', url: '../../apiV2/subBusinessUnit/Create' }
																									,   'update': { method: 'POST', url: '../../apiV2/subBusinessUnit/Update' }
																									,   'delete': { method: 'DELETE', url: '../../apiV2/subBusinessUnit/Delete/:detailId' }
																								}
																								);
																							}
																						]);

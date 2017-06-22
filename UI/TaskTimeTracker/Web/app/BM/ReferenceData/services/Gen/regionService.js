																				'use strict';

																				angular.module('rootAppShell')
																					.factory('regionService', [
																						'$resource',
																						function ($resource) {
																							return $resource('./api/region/:detailId'
																								, null
																								, {
																										'getById': { method: 'GET', url: '../../apiV2/region/GetById/:detailId' }
																									,   'getList': { method: 'GET', url: '../../apiV2/region/GetList', isArray: true }
																									,   'create': { method: 'POST', url: '../../apiV2/region/Create' }
																									,   'update': { method: 'POST', url: '../../apiV2/region/Update' }
																									,   'delete': { method: 'DELETE', url: '../../apiV2/region/Delete/:detailId' }
																								}
																								);
																							}
																						]);

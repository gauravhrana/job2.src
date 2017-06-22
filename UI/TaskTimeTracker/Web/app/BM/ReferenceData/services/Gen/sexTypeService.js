																																				'use strict';

																																				angular.module('rootAppShell')
																																					.factory('sexTypeService', [
																																						'$resource',
																																						function ($resource) {
																																							return $resource('./api/sexType/:detailId'
																																								, null
																																								, {
																																										'getById': { method: 'GET', url: '../../apiV2/sexType/GetById/:detailId' }
																																									,   'getList': { method: 'GET', url: '../../apiV2/sexType/GetList', isArray: true }
																																									,   'create': { method: 'POST', url: '../../apiV2/sexType/Create' }
																																									,   'update': { method: 'POST', url: '../../apiV2/sexType/Update' }
																																									,   'delete': { method: 'DELETE', url: '../../apiV2/sexType/Delete/:detailId' }
																																								}
																																								);
																																							}
																																						]);

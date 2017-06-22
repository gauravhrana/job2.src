																																								'use strict';

																																								angular.module('rootAppShell')
																																									.factory('provinceService', [
																																										'$resource',
																																										function ($resource) {
																																											return $resource('./api/province/:detailId'
																																												, null
																																												, {
																																														'getById': { method: 'GET', url: '../../apiV2/province/GetById/:detailId' }
																																													,   'getList': { method: 'GET', url: '../../apiV2/province/GetList', isArray: true }
																																													,   'create': { method: 'POST', url: '../../apiV2/province/Create' }
																																													,   'update': { method: 'POST', url: '../../apiV2/province/Update' }
																																													,   'delete': { method: 'DELETE', url: '../../apiV2/province/Delete/:detailId' }
																																												}
																																												);
																																											}
																																										]);

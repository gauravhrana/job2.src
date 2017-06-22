																																						'use strict';

																																						angular.module('rootAppShell')
																																							.factory('provinceTypeService', [
																																								'$resource',
																																								function ($resource) {
																																									return $resource('./api/provinceType/:detailId'
																																										, null
																																										, {
																																												'getById': { method: 'GET', url: '../../apiV2/provinceType/GetById/:detailId' }
																																											,   'getList': { method: 'GET', url: '../../apiV2/provinceType/GetList', isArray: true }
																																											,   'create': { method: 'POST', url: '../../apiV2/provinceType/Create' }
																																											,   'update': { method: 'POST', url: '../../apiV2/provinceType/Update' }
																																											,   'delete': { method: 'DELETE', url: '../../apiV2/provinceType/Delete/:detailId' }
																																										}
																																										);
																																									}
																																								]);

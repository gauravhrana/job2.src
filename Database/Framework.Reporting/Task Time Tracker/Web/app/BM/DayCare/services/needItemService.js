																						'use strict';

																						angular.module('rootAppShell')
																							.factory('needItemService', [
																								'$resource',
																								function ($resource) {
																									return $resource('./api/needItem/:detailId'
																										, null
																										, {
																												'getById': { method: 'GET', url: '../../apiV2/needItem/GetById/:detailId' }
																											,   'getList': { method: 'GET', url: '../../apiV2/needItem/GetList', isArray: true }
																											,   'create': { method: 'POST', url: '../../apiV2/needItem/Create' }
																											,   'update': { method: 'POST', url: '../../apiV2/needItem/Update' }
																											,   'delete': { method: 'DELETE', url: '../../apiV2/needItem/Delete/:detailId' }
																										}
																										);
																									}
																								]);

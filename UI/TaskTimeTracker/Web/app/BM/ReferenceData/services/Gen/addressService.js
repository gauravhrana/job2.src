																																				'use strict';

																																				angular.module('rootAppShell')
																																					.factory('addressService', [
																																						'$resource',
																																						function ($resource) {
																																							return $resource('./api/address/:detailId'
																																								, null
																																								, {
																																										'getById': { method: 'GET', url: '../../apiV2/address/GetById/:detailId' }
																																									,   'getList': { method: 'GET', url: '../../apiV2/address/GetList', isArray: true }
																																									,   'create': { method: 'POST', url: '../../apiV2/address/Create' }
																																									,   'update': { method: 'POST', url: '../../apiV2/address/Update' }
																																									,   'delete': { method: 'DELETE', url: '../../apiV2/address/Delete/:detailId' }
																																								}
																																								);
																																							}
																																						]);

																				'use strict';

																				angular.module('rootAppShell')
																					.factory('assetBackedService', [
																						'$resource',
																						function ($resource) {
																							return $resource('./api/assetBacked/:detailId'
																								, null
																								, {
																										'getById': { method: 'GET', url: '../../apiV2/assetBacked/GetById/:detailId' }
																									,   'getList': { method: 'GET', url: '../../apiV2/assetBacked/GetList', isArray: true }
																									,   'create': { method: 'POST', url: '../../apiV2/assetBacked/Create' }
																									,   'update': { method: 'POST', url: '../../apiV2/assetBacked/Update' }
																									,   'delete': { method: 'DELETE', url: '../../apiV2/assetBacked/Delete/:detailId' }
																								}
																								);
																							}
																						]);

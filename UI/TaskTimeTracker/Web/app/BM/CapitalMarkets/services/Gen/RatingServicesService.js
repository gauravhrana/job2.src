																				'use strict';

																				angular.module('rootAppShell')
																					.factory('ratingServicesService', [
																						'$resource',
																						function ($resource) {
																							return $resource('./api/ratingServices/:detailId'
																								, null
																								, {
																										'getById': { method: 'GET', url: '../../apiV2/ratingServices/GetById/:detailId' }
																									,   'getList': { method: 'GET', url: '../../apiV2/ratingServices/GetList', isArray: true }
																									,   'create': { method: 'POST', url: '../../apiV2/ratingServices/Create' }
																									,   'update': { method: 'POST', url: '../../apiV2/ratingServices/Update' }
																									,   'delete': { method: 'DELETE', url: '../../apiV2/ratingServices/Delete/:detailId' }
																								}
																								);
																							}
																						]);

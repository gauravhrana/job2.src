																						'use strict';

																						angular.module('rootAppShell')
																							.factory('taxStatusService', [
																								'$resource',
																								function ($resource) {
																									return $resource('./api/taxStatus/:detailId'
																										, null
																										, {
																												'getById': { method: 'GET', url: '../../apiV2/taxStatus/GetById/:detailId' }
																											,   'getList': { method: 'GET', url: '../../apiV2/taxStatus/GetList', isArray: true }
																											,   'create': { method: 'POST', url: '../../apiV2/taxStatus/Create' }
																											,   'update': { method: 'POST', url: '../../apiV2/taxStatus/Update' }
																											,   'delete': { method: 'DELETE', url: '../../apiV2/taxStatus/Delete/:detailId' }
																										}
																										);
																									}
																								]);

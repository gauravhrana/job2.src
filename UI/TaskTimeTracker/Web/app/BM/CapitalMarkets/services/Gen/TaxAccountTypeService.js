																				'use strict';

																				angular.module('rootAppShell')
																					.factory('taxAccountTypeService', [
																						'$resource',
																						function ($resource) {
																							return $resource('./api/taxAccountType/:detailId'
																								, null
																								, {
																										'getById': { method: 'GET', url: '../../apiV2/taxAccountType/GetById/:detailId' }
																									,   'getList': { method: 'GET', url: '../../apiV2/taxAccountType/GetList', isArray: true }
																									,   'create': { method: 'POST', url: '../../apiV2/taxAccountType/Create' }
																									,   'update': { method: 'POST', url: '../../apiV2/taxAccountType/Update' }
																									,   'delete': { method: 'DELETE', url: '../../apiV2/taxAccountType/Delete/:detailId' }
																								}
																								);
																							}
																						]);

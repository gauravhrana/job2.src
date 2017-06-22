																								'use strict';

																								angular.module('rootAppShell')
																									.factory('withholdingTaxTypeService', [
																										'$resource',
																										function ($resource) {
																											return $resource('./api/withholdingTaxType/:detailId'
																												, null
																												, {
																														'getById': { method: 'GET', url: '../../apiV2/withholdingTaxType/GetById/:detailId' }
																													,   'getList': { method: 'GET', url: '../../apiV2/withholdingTaxType/GetList', isArray: true }
																													,   'create': { method: 'POST', url: '../../apiV2/withholdingTaxType/Create' }
																													,   'update': { method: 'POST', url: '../../apiV2/withholdingTaxType/Update' }
																													,   'delete': { method: 'DELETE', url: '../../apiV2/withholdingTaxType/Delete/:detailId' }
																												}
																												);
																											}
																										]);

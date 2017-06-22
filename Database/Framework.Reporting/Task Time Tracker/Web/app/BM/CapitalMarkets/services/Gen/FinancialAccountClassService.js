																										'use strict';

																										angular.module('rootAppShell')
																											.factory('financialAccountClassService', [
																												'$resource',
																												function ($resource) {
																													return $resource('./api/financialAccountClass/:detailId'
																														, null
																														, {
																																'getById': { method: 'GET', url: '../../apiV2/financialAccountClass/GetById/:detailId' }
																															,   'getList': { method: 'GET', url: '../../apiV2/financialAccountClass/GetList', isArray: true }
																															,   'create': { method: 'POST', url: '../../apiV2/financialAccountClass/Create' }
																															,   'update': { method: 'POST', url: '../../apiV2/financialAccountClass/Update' }
																															,   'delete': { method: 'DELETE', url: '../../apiV2/financialAccountClass/Delete/:detailId' }
																														}
																														);
																													}
																												]);

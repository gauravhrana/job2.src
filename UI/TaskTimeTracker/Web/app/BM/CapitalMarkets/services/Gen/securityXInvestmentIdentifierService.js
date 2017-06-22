																																										'use strict';

																																										angular.module('rootAppShell')
																																											.factory('securityXInvestmentIdentifierService', [
																																												'$resource',
																																												function ($resource) {
																																													return $resource('./api/securityXInvestmentIdentifier/:detailId'
																																														, null
																																														, {
																																																'getById': { method: 'GET', url: '../../apiV2/securityXInvestmentIdentifier/GetById/:detailId' }
																																															,   'getList': { method: 'GET', url: '../../apiV2/securityXInvestmentIdentifier/GetList', isArray: true }
																																															,   'create': { method: 'POST', url: '../../apiV2/securityXInvestmentIdentifier/Create' }
																																															,   'update': { method: 'POST', url: '../../apiV2/securityXInvestmentIdentifier/Update' }
																																															,   'delete': { method: 'DELETE', url: '../../apiV2/securityXInvestmentIdentifier/Delete/:detailId' }
																																														}
																																														);
																																													}
																																												]);

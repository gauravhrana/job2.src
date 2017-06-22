												'use strict';

												angular.module('rootAppShell')
													.factory('chartOfAccountsService', [
														'$resource',
														function ($resource) {
															return $resource('./api/chartOfAccounts/:detailId'
																, null
																, {
																		'getById': { method: 'GET', url: '../../apiV2/chartOfAccounts/GetById/:detailId' }
																	,   'getList': { method: 'GET', url: '../../apiV2/chartOfAccounts/GetList', isArray: true }
																	,   'create': { method: 'POST', url: '../../apiV2/chartOfAccounts/Create' }
																	,   'update': { method: 'POST', url: '../../apiV2/chartOfAccounts/Update' }
																	,   'delete': { method: 'DELETE', url: '../../apiV2/chartOfAccounts/Delete/:detailId' }
																}
																);
															}
														]);

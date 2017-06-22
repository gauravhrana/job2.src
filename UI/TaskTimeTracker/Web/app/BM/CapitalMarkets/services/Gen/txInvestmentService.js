								'use strict';

								angular.module('rootAppShell')
									.factory('txInvestmentService', [
										'$resource',
										function ($resource) {
											return $resource('./api/txInvestment/:detailId'
												, null
												, {
														'getById': { method: 'GET', url: '../../apiV2/txInvestment/GetById/:detailId' }
													,   'getList': { method: 'GET', url: '../../apiV2/txInvestment/GetList', isArray: true }
													,   'create': { method: 'POST', url: '../../apiV2/txInvestment/Create' }
													,   'update': { method: 'POST', url: '../../apiV2/txInvestment/Update' }
													,   'delete': { method: 'DELETE', url: '../../apiV2/txInvestment/Delete/:detailId' }
												}
												);
											}
										]);

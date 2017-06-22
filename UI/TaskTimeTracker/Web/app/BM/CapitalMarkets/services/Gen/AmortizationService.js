								'use strict';

								angular.module('rootAppShell')
									.factory('amortizationService', [
										'$resource',
										function ($resource) {
											return $resource('./api/amortization/:detailId'
												, null
												, {
														'getById': { method: 'GET', url: '../../apiV2/amortization/GetById/:detailId' }
													,   'getList': { method: 'GET', url: '../../apiV2/amortization/GetList', isArray: true }
													,   'create': { method: 'POST', url: '../../apiV2/amortization/Create' }
													,   'update': { method: 'POST', url: '../../apiV2/amortization/Update' }
													,   'delete': { method: 'DELETE', url: '../../apiV2/amortization/Delete/:detailId' }
												}
												);
											}
										]);

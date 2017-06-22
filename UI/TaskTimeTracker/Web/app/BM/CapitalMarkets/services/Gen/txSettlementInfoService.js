										'use strict';

										angular.module('rootAppShell')
											.factory('txSettlementInfoService', [
												'$resource',
												function ($resource) {
													return $resource('./api/txSettlementInfo/:detailId'
														, null
														, {
																'getById': { method: 'GET', url: '../../apiV2/txSettlementInfo/GetById/:detailId' }
															,   'getList': { method: 'GET', url: '../../apiV2/txSettlementInfo/GetList', isArray: true }
															,   'create': { method: 'POST', url: '../../apiV2/txSettlementInfo/Create' }
															,   'update': { method: 'POST', url: '../../apiV2/txSettlementInfo/Update' }
															,   'delete': { method: 'DELETE', url: '../../apiV2/txSettlementInfo/Delete/:detailId' }
														}
														);
													}
												]);

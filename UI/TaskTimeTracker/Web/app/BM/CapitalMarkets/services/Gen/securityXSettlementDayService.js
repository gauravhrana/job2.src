																																						'use strict';

																																						angular.module('rootAppShell')
																																							.factory('securityXSettlementDayService', [
																																								'$resource',
																																								function ($resource) {
																																									return $resource('./api/securityXSettlementDay/:detailId'
																																										, null
																																										, {
																																												'getById': { method: 'GET', url: '../../apiV2/securityXSettlementDay/GetById/:detailId' }
																																											,   'getList': { method: 'GET', url: '../../apiV2/securityXSettlementDay/GetList', isArray: true }
																																											,   'create': { method: 'POST', url: '../../apiV2/securityXSettlementDay/Create' }
																																											,   'update': { method: 'POST', url: '../../apiV2/securityXSettlementDay/Update' }
																																											,   'delete': { method: 'DELETE', url: '../../apiV2/securityXSettlementDay/Delete/:detailId' }
																																										}
																																										);
																																									}
																																								]);

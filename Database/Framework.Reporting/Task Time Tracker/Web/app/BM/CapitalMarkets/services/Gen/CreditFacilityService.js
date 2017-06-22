										'use strict';

										angular.module('rootAppShell')
											.factory('creditFacilityService', [
												'$resource',
												function ($resource) {
													return $resource('./api/creditFacility/:detailId'
														, null
														, {
																'getById': { method: 'GET', url: '../../apiV2/creditFacility/GetById/:detailId' }
															,   'getList': { method: 'GET', url: '../../apiV2/creditFacility/GetList', isArray: true }
															,   'create': { method: 'POST', url: '../../apiV2/creditFacility/Create' }
															,   'update': { method: 'POST', url: '../../apiV2/creditFacility/Update' }
															,   'delete': { method: 'DELETE', url: '../../apiV2/creditFacility/Delete/:detailId' }
														}
														);
													}
												]);

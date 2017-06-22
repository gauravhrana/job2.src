														'use strict';

														angular.module('rootAppShell')
															.factory('managementFirmService', [
																'$resource',
																function ($resource) {
																	return $resource('./api/managementFirm/:detailId'
																		, null
																		, {
																				'getById': { method: 'GET', url: '../../apiV2/managementFirm/GetById/:detailId' }
																			,   'getList': { method: 'GET', url: '../../apiV2/managementFirm/GetList', isArray: true }
																			,   'create': { method: 'POST', url: '../../apiV2/managementFirm/Create' }
																			,   'update': { method: 'POST', url: '../../apiV2/managementFirm/Update' }
																			,   'delete': { method: 'DELETE', url: '../../apiV2/managementFirm/Delete/:detailId' }
																		}
																		);
																	}
																]);

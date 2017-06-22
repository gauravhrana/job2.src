																														'use strict';

																														angular.module('rootAppShell')
																															.factory('cashService', [
																																'$resource',
																																function ($resource) {
																																	return $resource('./api/cash/:detailId'
																																		, null
																																		, {
																																				'getById': { method: 'GET', url: '../../apiV2/cash/GetById/:detailId' }
																																			,   'getList': { method: 'GET', url: '../../apiV2/cash/GetList', isArray: true }
																																			,   'create': { method: 'POST', url: '../../apiV2/cash/Create' }
																																			,   'update': { method: 'POST', url: '../../apiV2/cash/Update' }
																																			,   'delete': { method: 'DELETE', url: '../../apiV2/cash/Delete/:detailId' }
																																		}
																																		);
																																	}
																																]);

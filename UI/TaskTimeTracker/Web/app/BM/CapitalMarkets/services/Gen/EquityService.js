														'use strict';

														angular.module('rootAppShell')
															.factory('equityService', [
																'$resource',
																function ($resource) {
																	return $resource('./api/equity/:detailId'
																		, null
																		, {
																				'getById': { method: 'GET', url: '../../apiV2/equity/GetById/:detailId' }
																			,   'getList': { method: 'GET', url: '../../apiV2/equity/GetList', isArray: true }
																			,   'create': { method: 'POST', url: '../../apiV2/equity/Create' }
																			,   'update': { method: 'POST', url: '../../apiV2/equity/Update' }
																			,   'delete': { method: 'DELETE', url: '../../apiV2/equity/Delete/:detailId' }
																		}
																		);
																	}
																]);

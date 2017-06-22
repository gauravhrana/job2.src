														'use strict';

														angular.module('rootAppShell')
															.factory('orderItemService', [
																'$resource',
																function ($resource) {
																	return $resource('./api/orderItem/:detailId'
																		, null
																		, {
																				'getById': { method: 'GET', url: '../../apiV2/orderItem/GetById/:detailId' }
																			,   'getList': { method: 'GET', url: '../../apiV2/orderItem/GetList', isArray: true }
																			,   'create': { method: 'POST', url: '../../apiV2/orderItem/Create' }
																			,   'update': { method: 'POST', url: '../../apiV2/orderItem/Update' }
																			,   'delete': { method: 'DELETE', url: '../../apiV2/orderItem/Delete/:detailId' }
																		}
																		);
																	}
																]);

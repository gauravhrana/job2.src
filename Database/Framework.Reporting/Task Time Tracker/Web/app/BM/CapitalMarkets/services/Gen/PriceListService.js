														'use strict';

														angular.module('rootAppShell')
															.factory('priceListService', [
																'$resource',
																function ($resource) {
																	return $resource('./api/priceList/:detailId'
																		, null
																		, {
																				'getById': { method: 'GET', url: '../../apiV2/priceList/GetById/:detailId' }
																			,   'getList': { method: 'GET', url: '../../apiV2/priceList/GetList', isArray: true }
																			,   'create': { method: 'POST', url: '../../apiV2/priceList/Create' }
																			,   'update': { method: 'POST', url: '../../apiV2/priceList/Update' }
																			,   'delete': { method: 'DELETE', url: '../../apiV2/priceList/Delete/:detailId' }
																		}
																		);
																	}
																]);

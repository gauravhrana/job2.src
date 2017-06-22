														'use strict';

														angular.module('rootAppShell')
															.factory('helpLineService', [
																'$resource',
																function ($resource) {
																	return $resource('./api/helpLine/:detailId'
																		, null
																		, {
																				'getById': { method: 'GET', url: '../../apiV2/helpLine/GetById/:detailId' }
																			,   'getList': { method: 'GET', url: '../../apiV2/helpLine/GetList', isArray: true }
																			,   'create': { method: 'POST', url: '../../apiV2/helpLine/Create' }
																			,   'update': { method: 'POST', url: '../../apiV2/helpLine/Update' }
																			,   'delete': { method: 'DELETE', url: '../../apiV2/helpLine/Delete/:detailId' }
																		}
																		);
																	}
																]);

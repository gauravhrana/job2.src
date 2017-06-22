																														'use strict';

																														angular.module('rootAppShell')
																															.factory('holidayService', [
																																'$resource',
																																function ($resource) {
																																	return $resource('./api/holiday/:detailId'
																																		, null
																																		, {
																																				'getById': { method: 'GET', url: '../../apiV2/holiday/GetById/:detailId' }
																																			,   'getList': { method: 'GET', url: '../../apiV2/holiday/GetList', isArray: true }
																																			,   'create': { method: 'POST', url: '../../apiV2/holiday/Create' }
																																			,   'update': { method: 'POST', url: '../../apiV2/holiday/Update' }
																																			,   'delete': { method: 'DELETE', url: '../../apiV2/holiday/Delete/:detailId' }
																																		}
																																		);
																																	}
																																]);

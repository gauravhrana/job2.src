														'use strict';

														angular.module('rootAppShell')
															.factory('recordTypeService', [
																'$resource',
																function ($resource) {
																	return $resource('./api/recordType/:detailId'
																		, null
																		, {
																				'getById': { method: 'GET', url: '../../apiV2/recordType/GetById/:detailId' }
																			,   'getList': { method: 'GET', url: '../../apiV2/recordType/GetList', isArray: true }
																			,   'create': { method: 'POST', url: '../../apiV2/recordType/Create' }
																			,   'update': { method: 'POST', url: '../../apiV2/recordType/Update' }
																			,   'delete': { method: 'DELETE', url: '../../apiV2/recordType/Delete/:detailId' }
																		}
																		);
																	}
																]);

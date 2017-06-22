																														'use strict';

																														angular.module('rootAppShell')
																															.factory('tuitionService', [
																																'$resource',
																																function ($resource) {
																																	return $resource('./api/tuition/:detailId'
																																		, null
																																		, {
																																				'getById': { method: 'GET', url: '../../apiV2/tuition/GetById/:detailId' }
																																			,   'getList': { method: 'GET', url: '../../apiV2/tuition/GetList', isArray: true }
																																			,   'create': { method: 'POST', url: '../../apiV2/tuition/Create' }
																																			,   'update': { method: 'POST', url: '../../apiV2/tuition/Update' }
																																			,   'delete': { method: 'DELETE', url: '../../apiV2/tuition/Delete/:detailId' }
																																		}
																																		);
																																	}
																																]);

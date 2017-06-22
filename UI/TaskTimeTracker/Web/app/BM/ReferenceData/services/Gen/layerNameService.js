																																														'use strict';

																																														angular.module('rootAppShell')
																																															.factory('layerNameService', [
																																																'$resource',
																																																function ($resource) {
																																																	return $resource('./api/layerName/:detailId'
																																																		, null
																																																		, {
																																																				'getById': { method: 'GET', url: '../../apiV2/layerName/GetById/:detailId' }
																																																			,   'getList': { method: 'GET', url: '../../apiV2/layerName/GetList', isArray: true }
																																																			,   'create': { method: 'POST', url: '../../apiV2/layerName/Create' }
																																																			,   'update': { method: 'POST', url: '../../apiV2/layerName/Update' }
																																																			,   'delete': { method: 'DELETE', url: '../../apiV2/layerName/Delete/:detailId' }
																																																		}
																																																		);
																																																	}
																																																]);

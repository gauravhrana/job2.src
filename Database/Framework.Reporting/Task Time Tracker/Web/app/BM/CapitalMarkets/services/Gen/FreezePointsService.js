														'use strict';

														angular.module('rootAppShell')
															.factory('freezePointsService', [
																'$resource',
																function ($resource) {
																	return $resource('./api/freezePoints/:detailId'
																		, null
																		, {
																				'getById': { method: 'GET', url: '../../apiV2/freezePoints/GetById/:detailId' }
																			,   'getList': { method: 'GET', url: '../../apiV2/freezePoints/GetList', isArray: true }
																			,   'create': { method: 'POST', url: '../../apiV2/freezePoints/Create' }
																			,   'update': { method: 'POST', url: '../../apiV2/freezePoints/Update' }
																			,   'delete': { method: 'DELETE', url: '../../apiV2/freezePoints/Delete/:detailId' }
																		}
																		);
																	}
																]);

														'use strict';

														angular.module('rootAppShell')
															.factory('commentService', [
																'$resource',
																function ($resource) {
																	return $resource('./api/comment/:detailId'
																		, null
																		, {
																				'getById': { method: 'GET', url: '../../apiV2/comment/GetById/:detailId' }
																			,   'getList': { method: 'GET', url: '../../apiV2/comment/GetList', isArray: true }
																			,   'create': { method: 'POST', url: '../../apiV2/comment/Create' }
																			,   'update': { method: 'POST', url: '../../apiV2/comment/Update' }
																			,   'delete': { method: 'DELETE', url: '../../apiV2/comment/Delete/:detailId' }
																		}
																		);
																	}
																]);

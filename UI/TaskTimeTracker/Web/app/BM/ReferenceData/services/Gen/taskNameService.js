																																												'use strict';

																																												angular.module('rootAppShell')
																																													.factory('taskNameService', [
																																														'$resource',
																																														function ($resource) {
																																															return $resource('./api/taskName/:detailId'
																																																, null
																																																, {
																																																		'getById': { method: 'GET', url: '../../apiV2/taskName/GetById/:detailId' }
																																																	,   'getList': { method: 'GET', url: '../../apiV2/taskName/GetList', isArray: true }
																																																	,   'create': { method: 'POST', url: '../../apiV2/taskName/Create' }
																																																	,   'update': { method: 'POST', url: '../../apiV2/taskName/Update' }
																																																	,   'delete': { method: 'DELETE', url: '../../apiV2/taskName/Delete/:detailId' }
																																																}
																																																);
																																															}
																																														]);

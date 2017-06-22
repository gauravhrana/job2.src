																																				'use strict';

																																				angular.module('rootAppShell')
																																					.factory('teacherService', [
																																						'$resource',
																																						function ($resource) {
																																							return $resource('./api/teacher/:detailId'
																																								, null
																																								, {
																																										'getById': { method: 'GET', url: '../../apiV2/teacher/GetById/:detailId' }
																																									,   'getList': { method: 'GET', url: '../../apiV2/teacher/GetList', isArray: true }
																																									,   'create': { method: 'POST', url: '../../apiV2/teacher/Create' }
																																									,   'update': { method: 'POST', url: '../../apiV2/teacher/Update' }
																																									,   'delete': { method: 'DELETE', url: '../../apiV2/teacher/Delete/:detailId' }
																																								}
																																								);
																																							}
																																						]);

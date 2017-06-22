																																		'use strict';

																																		angular.module('rootAppShell')
																																			.factory('studentService', [
																																				'$resource',
																																				function ($resource) {
																																					return $resource('./api/student/:detailId'
																																						, null
																																						, {
																																								'getById': { method: 'GET', url: '../../apiV2/student/GetById/:detailId' }
																																							,   'getList': { method: 'GET', url: '../../apiV2/student/GetList', isArray: true }
																																							,   'create': { method: 'POST', url: '../../apiV2/student/Create' }
																																							,   'update': { method: 'POST', url: '../../apiV2/student/Update' }
																																							,   'delete': { method: 'DELETE', url: '../../apiV2/student/Delete/:detailId' }
																																						}
																																						);
																																					}
																																				]);

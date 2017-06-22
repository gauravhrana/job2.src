												'use strict';

												angular.module('rootAppShell')
													.factory('pressReleaseTypeService', [
														'$resource',
														function ($resource) {
															return $resource('./api/pressReleaseType/:detailId'
																, null
																, {
																		'getById': { method: 'GET', url: '../../apiV2/pressReleaseType/GetById/:detailId' }
																	,   'getList': { method: 'GET', url: '../../apiV2/pressReleaseType/GetList', isArray: true }
																	,   'create': { method: 'POST', url: '../../apiV2/pressReleaseType/Create' }
																	,   'update': { method: 'POST', url: '../../apiV2/pressReleaseType/Update' }
																	,   'delete': { method: 'DELETE', url: '../../apiV2/pressReleaseType/Delete/:detailId' }
																}
																);
															}
														]);

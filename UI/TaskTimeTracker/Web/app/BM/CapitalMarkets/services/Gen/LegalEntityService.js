												'use strict';

												angular.module('rootAppShell')
													.factory('legalEntityService', [
														'$resource',
														function ($resource) {
															return $resource('./api/legalEntity/:detailId'
																, null
																, {
																		'getById': { method: 'GET', url: '../../apiV2/legalEntity/GetById/:detailId' }
																	,   'getList': { method: 'GET', url: '../../apiV2/legalEntity/GetList', isArray: true }
																	,   'create': { method: 'POST', url: '../../apiV2/legalEntity/Create' }
																	,   'update': { method: 'POST', url: '../../apiV2/legalEntity/Update' }
																	,   'delete': { method: 'DELETE', url: '../../apiV2/legalEntity/Delete/:detailId' }
																}
																);
															}
														]);

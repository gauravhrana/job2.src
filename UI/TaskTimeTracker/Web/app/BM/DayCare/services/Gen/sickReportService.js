																												'use strict';

																												angular.module('rootAppShell')
																													.factory('sickReportService', [
																														'$resource',
																														function ($resource) {
																															return $resource('./api/sickReport/:detailId'
																																, null
																																, {
																																		'getById': { method: 'GET', url: '../../apiV2/sickReport/GetById/:detailId' }
																																	,   'getList': { method: 'GET', url: '../../apiV2/sickReport/GetList', isArray: true }
																																	,   'create': { method: 'POST', url: '../../apiV2/sickReport/Create' }
																																	,   'update': { method: 'POST', url: '../../apiV2/sickReport/Update' }
																																	,   'delete': { method: 'DELETE', url: '../../apiV2/sickReport/Delete/:detailId' }
																																}
																																);
																															}
																														]);

																												'use strict';

																												angular.module('rootAppShell')
																													.factory('calendarService', [
																														'$resource',
																														function ($resource) {
																															return $resource('./api/calendar/:detailId'
																																, null
																																, {
																																		'getById': { method: 'GET', url: '../../apiV2/calendar/GetById/:detailId' }
																																	,   'getList': { method: 'GET', url: '../../apiV2/calendar/GetList', isArray: true }
																																	,   'create': { method: 'POST', url: '../../apiV2/calendar/Create' }
																																	,   'update': { method: 'POST', url: '../../apiV2/calendar/Update' }
																																	,   'delete': { method: 'DELETE', url: '../../apiV2/calendar/Delete/:detailId' }
																																}
																																);
																															}
																														]);

																								'use strict';

																								angular.module('rootAppShell')
																									.factory('timeZoneService', [
																										'$resource',
																										function ($resource) {
																											return $resource('./api/timeZone/:detailId'
																												, null
																												, {
																														'getById': { method: 'GET', url: '../../apiV2/timeZone/GetById/:detailId' }
																													,   'getList': { method: 'GET', url: '../../apiV2/timeZone/GetList', isArray: true }
																													,   'create': { method: 'POST', url: '../../apiV2/timeZone/Create' }
																													,   'update': { method: 'POST', url: '../../apiV2/timeZone/Update' }
																													,   'delete': { method: 'DELETE', url: '../../apiV2/timeZone/Delete/:detailId' }
																												}
																												);
																											}
																										]);

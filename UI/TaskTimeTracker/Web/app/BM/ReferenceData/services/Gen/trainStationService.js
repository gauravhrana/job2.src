																										'use strict';

																										angular.module('rootAppShell')
																											.factory('trainStationService', [
																												'$resource',
																												function ($resource) {
																													return $resource('./api/trainStation/:detailId'
																														, null
																														, {
																																'getById': { method: 'GET', url: '../../apiV2/trainStation/GetById/:detailId' }
																															,   'getList': { method: 'GET', url: '../../apiV2/trainStation/GetList', isArray: true }
																															,   'create': { method: 'POST', url: '../../apiV2/trainStation/Create' }
																															,   'update': { method: 'POST', url: '../../apiV2/trainStation/Update' }
																															,   'delete': { method: 'DELETE', url: '../../apiV2/trainStation/Delete/:detailId' }
																														}
																														);
																													}
																												]);

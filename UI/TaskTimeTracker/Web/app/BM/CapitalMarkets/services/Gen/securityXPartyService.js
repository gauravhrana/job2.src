																																								'use strict';

																																								angular.module('rootAppShell')
																																									.factory('securityXPartyService', [
																																										'$resource',
																																										function ($resource) {
																																											return $resource('./api/securityXParty/:detailId'
																																												, null
																																												, {
																																														'getById': { method: 'GET', url: '../../apiV2/securityXParty/GetById/:detailId' }
																																													,   'getList': { method: 'GET', url: '../../apiV2/securityXParty/GetList', isArray: true }
																																													,   'create': { method: 'POST', url: '../../apiV2/securityXParty/Create' }
																																													,   'update': { method: 'POST', url: '../../apiV2/securityXParty/Update' }
																																													,   'delete': { method: 'DELETE', url: '../../apiV2/securityXParty/Delete/:detailId' }
																																												}
																																												);
																																											}
																																										]);

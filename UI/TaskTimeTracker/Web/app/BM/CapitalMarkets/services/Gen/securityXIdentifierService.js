																																										'use strict';

																																										angular.module('rootAppShell')
																																											.factory('securityXIdentifierService', [
																																												'$resource',
																																												function ($resource) {
																																													return $resource('./api/securityXIdentifier/:detailId'
																																														, null
																																														, {
																																																'getById': { method: 'GET', url: '../../apiV2/securityXIdentifier/GetById/:detailId' }
																																															,   'getList': { method: 'GET', url: '../../apiV2/securityXIdentifier/GetList', isArray: true }
																																															,   'create': { method: 'POST', url: '../../apiV2/securityXIdentifier/Create' }
																																															,   'update': { method: 'POST', url: '../../apiV2/securityXIdentifier/Update' }
																																															,   'delete': { method: 'DELETE', url: '../../apiV2/securityXIdentifier/Delete/:detailId' }
																																														}
																																														);
																																													}
																																												]);

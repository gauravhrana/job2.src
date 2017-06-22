																																										'use strict';

																																										angular.module('rootAppShell')
																																											.factory('genderService', [
																																												'$resource',
																																												function ($resource) {
																																													return $resource('./api/gender/:detailId'
																																														, null
																																														, {
																																																'getById': { method: 'GET', url: '../../apiV2/gender/GetById/:detailId' }
																																															,   'getList': { method: 'GET', url: '../../apiV2/gender/GetList', isArray: true }
																																															,   'create': { method: 'POST', url: '../../apiV2/gender/Create' }
																																															,   'update': { method: 'POST', url: '../../apiV2/gender/Update' }
																																															,   'delete': { method: 'DELETE', url: '../../apiV2/gender/Delete/:detailId' }
																																														}
																																														);
																																													}
																																												]);

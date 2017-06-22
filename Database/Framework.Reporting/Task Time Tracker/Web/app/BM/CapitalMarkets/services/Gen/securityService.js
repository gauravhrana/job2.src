								'use strict';

								angular.module('rootAppShell')
									.factory('securityService', [
										'$resource',
										function ($resource) {
											return $resource('./api/security/:detailId'
												, null
												, {
														'getById': { method: 'GET', url: '../../apiV2/security/GetById/:detailId' }
													,   'getList': { method: 'GET', url: '../../apiV2/security/GetList', isArray: true }
													,   'create': { method: 'POST', url: '../../apiV2/security/Create' }
													,   'update': { method: 'POST', url: '../../apiV2/security/Update' }
													,   'delete': { method: 'DELETE', url: '../../apiV2/security/Delete/:detailId' }
												}
												);
											}
										]);

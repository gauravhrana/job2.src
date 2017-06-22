								'use strict';

								angular.module('rootAppShell')
									.factory('religionService', [
										'$resource',
										function ($resource) {
											return $resource('./api/religion/:detailId'
												, null
												, {
														'getById': { method: 'GET', url: '../../apiV2/religion/GetById/:detailId' }
													,   'getList': { method: 'GET', url: '../../apiV2/religion/GetList', isArray: true }
													,   'create': { method: 'POST', url: '../../apiV2/religion/Create' }
													,   'update': { method: 'POST', url: '../../apiV2/religion/Update' }
													,   'delete': { method: 'DELETE', url: '../../apiV2/religion/Delete/:detailId' }
												}
												);
											}
										]);

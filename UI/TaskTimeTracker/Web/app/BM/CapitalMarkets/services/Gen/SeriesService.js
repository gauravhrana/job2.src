								'use strict';

								angular.module('rootAppShell')
									.factory('seriesService', [
										'$resource',
										function ($resource) {
											return $resource('./api/series/:detailId'
												, null
												, {
														'getById': { method: 'GET', url: '../../apiV2/series/GetById/:detailId' }
													,   'getList': { method: 'GET', url: '../../apiV2/series/GetList', isArray: true }
													,   'create': { method: 'POST', url: '../../apiV2/series/Create' }
													,   'update': { method: 'POST', url: '../../apiV2/series/Update' }
													,   'delete': { method: 'DELETE', url: '../../apiV2/series/Delete/:detailId' }
												}
												);
											}
										]);

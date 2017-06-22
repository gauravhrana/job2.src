								'use strict';

								angular.module('rootAppShell')
									.factory('jurisdictionsService', [
										'$resource',
										function ($resource) {
											return $resource('./api/jurisdictions/:detailId'
												, null
												, {
														'getById': { method: 'GET', url: '../../apiV2/jurisdictions/GetById/:detailId' }
													,   'getList': { method: 'GET', url: '../../apiV2/jurisdictions/GetList', isArray: true }
													,   'create': { method: 'POST', url: '../../apiV2/jurisdictions/Create' }
													,   'update': { method: 'POST', url: '../../apiV2/jurisdictions/Update' }
													,   'delete': { method: 'DELETE', url: '../../apiV2/jurisdictions/Delete/:detailId' }
												}
												);
											}
										]);

										'use strict';

										angular.module('rootAppShell')
											.factory('airportService', [
												'$resource',
												function ($resource) {
													return $resource('./api/airport/:detailId'
														, null
														, {
																'getById': { method: 'GET', url: '../../apiV2/airport/GetById/:detailId' }
															,   'getList': { method: 'GET', url: '../../apiV2/airport/GetList', isArray: true }
															,   'create': { method: 'POST', url: '../../apiV2/airport/Create' }
															,   'update': { method: 'POST', url: '../../apiV2/airport/Update' }
															,   'delete': { method: 'DELETE', url: '../../apiV2/airport/Delete/:detailId' }
														}
														);
													}
												]);

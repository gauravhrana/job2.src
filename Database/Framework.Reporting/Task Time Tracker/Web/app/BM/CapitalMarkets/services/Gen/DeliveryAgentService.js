								'use strict';

								angular.module('rootAppShell')
									.factory('deliveryAgentService', [
										'$resource',
										function ($resource) {
											return $resource('./api/deliveryAgent/:detailId'
												, null
												, {
														'getById': { method: 'GET', url: '../../apiV2/deliveryAgent/GetById/:detailId' }
													,   'getList': { method: 'GET', url: '../../apiV2/deliveryAgent/GetList', isArray: true }
													,   'create': { method: 'POST', url: '../../apiV2/deliveryAgent/Create' }
													,   'update': { method: 'POST', url: '../../apiV2/deliveryAgent/Update' }
													,   'delete': { method: 'DELETE', url: '../../apiV2/deliveryAgent/Delete/:detailId' }
												}
												);
											}
										]);

								'use strict';

								angular.module('rootAppShell')
									.factory('orderStatusGroupService', [
										'$resource',
										function ($resource) {
											return $resource('./api/orderStatusGroup/:detailId'
												, null
												, {
														'getById': { method: 'GET', url: '../../apiV2/orderStatusGroup/GetById/:detailId' }
													,   'getList': { method: 'GET', url: '../../apiV2/orderStatusGroup/GetList', isArray: true }
													,   'create': { method: 'POST', url: '../../apiV2/orderStatusGroup/Create' }
													,   'update': { method: 'POST', url: '../../apiV2/orderStatusGroup/Update' }
													,   'delete': { method: 'DELETE', url: '../../apiV2/orderStatusGroup/Delete/:detailId' }
												}
												);
											}
										]);

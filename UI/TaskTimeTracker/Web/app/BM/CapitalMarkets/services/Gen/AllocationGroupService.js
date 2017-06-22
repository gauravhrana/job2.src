										'use strict';

										angular.module('rootAppShell')
											.factory('allocationGroupService', [
												'$resource',
												function ($resource) {
													return $resource('./api/allocationGroup/:detailId'
														, null
														, {
																'getById': { method: 'GET', url: '../../apiV2/allocationGroup/GetById/:detailId' }
															,   'getList': { method: 'GET', url: '../../apiV2/allocationGroup/GetList', isArray: true }
															,   'create': { method: 'POST', url: '../../apiV2/allocationGroup/Create' }
															,   'update': { method: 'POST', url: '../../apiV2/allocationGroup/Update' }
															,   'delete': { method: 'DELETE', url: '../../apiV2/allocationGroup/Delete/:detailId' }
														}
														);
													}
												]);

										'use strict';

										angular.module('rootAppShell')
											.factory('automatedFreezepointService', [
												'$resource',
												function ($resource) {
													return $resource('./api/automatedFreezepoint/:detailId'
														, null
														, {
																'getById': { method: 'GET', url: '../../apiV2/automatedFreezepoint/GetById/:detailId' }
															,   'getList': { method: 'GET', url: '../../apiV2/automatedFreezepoint/GetList', isArray: true }
															,   'create': { method: 'POST', url: '../../apiV2/automatedFreezepoint/Create' }
															,   'update': { method: 'POST', url: '../../apiV2/automatedFreezepoint/Update' }
															,   'delete': { method: 'DELETE', url: '../../apiV2/automatedFreezepoint/Delete/:detailId' }
														}
														);
													}
												]);

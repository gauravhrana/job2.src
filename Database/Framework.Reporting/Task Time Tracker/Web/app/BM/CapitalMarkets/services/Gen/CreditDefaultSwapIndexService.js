						'use strict';

						angular.module('rootAppShell')
							.factory('creditDefaultSwapIndexService', [
								'$resource',
								function ($resource) {
									return $resource('./api/creditDefaultSwapIndex/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/creditDefaultSwapIndex/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/creditDefaultSwapIndex/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/creditDefaultSwapIndex/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/creditDefaultSwapIndex/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/creditDefaultSwapIndex/Delete/:detailId' }
										}
										);
									}
								]);

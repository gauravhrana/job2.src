						'use strict';

						angular.module('rootAppShell')
							.factory('commissionSplitService', [
								'$resource',
								function ($resource) {
									return $resource('./api/commissionSplit/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/commissionSplit/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/commissionSplit/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/commissionSplit/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/commissionSplit/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/commissionSplit/Delete/:detailId' }
										}
										);
									}
								]);

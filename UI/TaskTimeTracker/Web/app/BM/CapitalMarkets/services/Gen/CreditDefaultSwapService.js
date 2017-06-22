				'use strict';

				angular.module('rootAppShell')
					.factory('creditDefaultSwapService', [
						'$resource',
						function ($resource) {
							return $resource('./api/creditDefaultSwap/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/creditDefaultSwap/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/creditDefaultSwap/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/creditDefaultSwap/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/creditDefaultSwap/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/creditDefaultSwap/Delete/:detailId' }
								}
								);
							}
						]);

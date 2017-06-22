				'use strict';

				angular.module('rootAppShell')
					.factory('orderRequestService', [
						'$resource',
						function ($resource) {
							return $resource('./api/orderRequest/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/orderRequest/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/orderRequest/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/orderRequest/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/orderRequest/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/orderRequest/Delete/:detailId' }
								}
								);
							}
						]);

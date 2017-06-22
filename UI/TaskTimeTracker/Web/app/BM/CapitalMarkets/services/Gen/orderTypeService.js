						'use strict';

						angular.module('rootAppShell')
							.factory('orderTypeService', [
								'$resource',
								function ($resource) {
									return $resource('./api/orderType/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/orderType/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/orderType/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/orderType/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/orderType/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/orderType/Delete/:detailId' }
										}
										);
									}
								]);

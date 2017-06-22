						'use strict';

						angular.module('rootAppShell')
							.factory('priceTypeService', [
								'$resource',
								function ($resource) {
									return $resource('./api/priceType/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/priceType/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/priceType/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/priceType/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/priceType/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/priceType/Delete/:detailId' }
										}
										);
									}
								]);

				'use strict';

				angular.module('rootAppShell')
					.factory('txTradeInfoService', [
						'$resource',
						function ($resource) {
							return $resource('./api/txTradeInfo/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/txTradeInfo/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/txTradeInfo/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/txTradeInfo/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/txTradeInfo/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/txTradeInfo/Delete/:detailId' }
								}
								);
							}
						]);

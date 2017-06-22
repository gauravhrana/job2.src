						'use strict';

						angular.module('rootAppShell')
							.factory('txTradeFiguresService', [
								'$resource',
								function ($resource) {
									return $resource('./api/txTradeFigures/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/txTradeFigures/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/txTradeFigures/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/txTradeFigures/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/txTradeFigures/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/txTradeFigures/Delete/:detailId' }
										}
										);
									}
								]);

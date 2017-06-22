						'use strict';

						angular.module('rootAppShell')
							.factory('counselService', [
								'$resource',
								function ($resource) {
									return $resource('./api/counsel/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/counsel/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/counsel/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/counsel/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/counsel/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/counsel/Delete/:detailId' }
										}
										);
									}
								]);

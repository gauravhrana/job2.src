						'use strict';

						angular.module('rootAppShell')
							.factory('industryService', [
								'$resource',
								function ($resource) {
									return $resource('./api/industry/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/industry/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/industry/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/industry/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/industry/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/industry/Delete/:detailId' }
										}
										);
									}
								]);

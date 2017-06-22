						'use strict';

						angular.module('rootAppShell')
							.factory('subClassService', [
								'$resource',
								function ($resource) {
									return $resource('./api/subClass/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/subClass/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/subClass/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/subClass/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/subClass/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/subClass/Delete/:detailId' }
										}
										);
									}
								]);

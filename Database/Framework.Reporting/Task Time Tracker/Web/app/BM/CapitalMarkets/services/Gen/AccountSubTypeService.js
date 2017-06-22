						'use strict';

						angular.module('rootAppShell')
							.factory('accountSubTypeService', [
								'$resource',
								function ($resource) {
									return $resource('./api/accountSubType/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/accountSubType/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/accountSubType/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/accountSubType/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/accountSubType/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/accountSubType/Delete/:detailId' }
										}
										);
									}
								]);

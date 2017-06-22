				'use strict';

				angular.module('rootAppShell')
					.factory('clientTypeService', [
						'$resource',
						function ($resource) {
							return $resource('./api/clientType/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/clientType/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/clientType/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/clientType/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/clientType/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/clientType/Delete/:detailId' }
								}
								);
							}
						]);

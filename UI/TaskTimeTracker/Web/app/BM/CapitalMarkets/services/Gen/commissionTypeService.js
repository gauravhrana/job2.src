				'use strict';

				angular.module('rootAppShell')
					.factory('commissionTypeService', [
						'$resource',
						function ($resource) {
							return $resource('./api/commissionType/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/commissionType/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/commissionType/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/commissionType/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/commissionType/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/commissionType/Delete/:detailId' }
								}
								);
							}
						]);

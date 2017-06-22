		'use strict';

		angular.module('rootAppShell')
			.factory('financialAccountTypeService', [
				'$resource',
				function ($resource) {
					return $resource('./api/financialAccountType/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/financialAccountType/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/financialAccountType/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/financialAccountType/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/financialAccountType/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/financialAccountType/Delete/:detailId' }
						}
						);
					}
				]);

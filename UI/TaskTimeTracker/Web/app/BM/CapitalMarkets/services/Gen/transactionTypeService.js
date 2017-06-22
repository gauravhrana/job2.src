'use strict';

angular.module('rootAppShell')
	.factory('transactionTypeService', [
		'$resource',
		function ($resource) {
			return $resource('./api/transactionType/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/transactionType/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/transactionType/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/transactionType/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/transactionType/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/transactionType/Delete/:detailId' }
				}
				);
			}
		]);

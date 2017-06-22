'use strict';

angular.module('rootAppShell')
	.factory('transactionEventService', [
		'$resource',
		function ($resource) {
			return $resource('./api/transactionEvent/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/transactionEvent/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/transactionEvent/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/transactionEvent/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/transactionEvent/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/transactionEvent/Delete/:detailId' }
				}
				);
			}
		]);

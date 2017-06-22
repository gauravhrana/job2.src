'use strict';

angular.module('rootAppShell')
	.factory('brokerService', [
		'$resource',
		function ($resource) {
			return $resource('./api/broker/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/broker/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/broker/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/broker/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/broker/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/broker/Delete/:detailId' }
				}
				);
			}
		]);

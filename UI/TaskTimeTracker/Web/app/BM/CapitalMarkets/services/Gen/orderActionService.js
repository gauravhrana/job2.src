		'use strict';

		angular.module('rootAppShell')
			.factory('orderActionService', [
				'$resource',
				function ($resource) {
					return $resource('./api/orderAction/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/orderAction/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/orderAction/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/orderAction/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/orderAction/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/orderAction/Delete/:detailId' }
						}
						);
					}
				]);

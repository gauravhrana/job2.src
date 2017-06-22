'use strict';

angular.module('rootAppShell')
	.factory('classInstanceService', [
		'$resource',
		function ($resource) {
			return $resource('./api/classInstance/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/classInstance/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/classInstance/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/classInstance/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/classInstance/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/classInstance/Delete/:detailId' }
				}
				);
			}
		]);

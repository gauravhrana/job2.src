'use strict';

angular.module('rootAppShell')
	.factory('foodTypeService', [
		'$resource',
		function ($resource) {
			return $resource('./api/foodType/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/foodType/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/foodType/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/foodType/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/foodType/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/foodType/Delete/:detailId' }
				}
				);
			}
		]);

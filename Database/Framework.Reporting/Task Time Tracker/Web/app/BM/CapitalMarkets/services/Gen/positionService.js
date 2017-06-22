'use strict';

angular.module('rootAppShell')
	.factory('positionService', [
		'$resource',
		function ($resource) {
			return $resource('./api/position/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/position/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/position/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/position/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/position/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/position/Delete/:detailId' }
				}
				);
			}
		]);

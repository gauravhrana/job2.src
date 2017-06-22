'use strict';

angular.module('rootAppShell')
	.factory('continentService', [
		'$resource',
		function ($resource) {
			return $resource('./api/continent/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/continent/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/continent/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/continent/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/continent/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/continent/Delete/:detailId' }
				}
				);
			}
		]);

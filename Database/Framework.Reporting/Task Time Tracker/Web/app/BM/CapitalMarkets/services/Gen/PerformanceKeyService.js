'use strict';

angular.module('rootAppShell')
	.factory('performanceKeyService', [
		'$resource',
		function ($resource) {
			return $resource('./api/performanceKey/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/performanceKey/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/performanceKey/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/performanceKey/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/performanceKey/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/performanceKey/Delete/:detailId' }
				}
				);
			}
		]);

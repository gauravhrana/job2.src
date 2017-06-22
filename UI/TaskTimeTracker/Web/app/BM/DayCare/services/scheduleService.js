'use strict';

angular.module('rootAppShell')
	.factory('scheduleService', [
		'$resource',
		function ($resource) {
			return $resource('./api/schedule/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/schedule/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/schedule/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/schedule/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/schedule/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/schedule/Delete/:detailId' }
				}
				);
			}
		]);

'use strict';

angular.module('rootAppShell')
	.factory('eventSubTypeService', [
		'$resource',
		function ($resource) {
			return $resource('./api/eventSubType/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/eventSubType/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/eventSubType/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/eventSubType/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/eventSubType/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/eventSubType/Delete/:detailId' }
				}
				);
			}
		]);

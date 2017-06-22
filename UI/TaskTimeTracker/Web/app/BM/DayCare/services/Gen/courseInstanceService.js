'use strict';

angular.module('rootAppShell')
	.factory('courseInstanceService', [
		'$resource',
		function ($resource) {
			return $resource('./api/courseInstance/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/courseInstance/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/courseInstance/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/courseInstance/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/courseInstance/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/courseInstance/Delete/:detailId' }
				}
				);
			}
		]);

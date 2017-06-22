		'use strict';

		angular.module('rootAppShell')
			.factory('rightService', [
				'$resource',
				function ($resource) {
					return $resource('./api/right/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/right/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/right/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/right/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/right/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/right/Delete/:detailId' }
						}
						);
					}
				]);

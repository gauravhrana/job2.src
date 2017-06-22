		'use strict';

		angular.module('rootAppShell')
			.factory('performanceParametersService', [
				'$resource',
				function ($resource) {
					return $resource('./api/performanceParameters/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/performanceParameters/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/performanceParameters/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/performanceParameters/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/performanceParameters/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/performanceParameters/Delete/:detailId' }
						}
						);
					}
				]);

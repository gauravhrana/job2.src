		'use strict';

		angular.module('rootAppShell')
			.factory('departmentService', [
				'$resource',
				function ($resource) {
					return $resource('./api/department/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/department/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/department/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/department/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/department/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/department/Delete/:detailId' }
						}
						);
					}
				]);

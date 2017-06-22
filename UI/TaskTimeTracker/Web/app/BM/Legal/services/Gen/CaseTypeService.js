		'use strict';

		angular.module('rootAppShell')
			.factory('caseTypeService', [
				'$resource',
				function ($resource) {
					return $resource('./api/caseType/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/caseType/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/caseType/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/caseType/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/caseType/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/caseType/Delete/:detailId' }
						}
						);
					}
				]);

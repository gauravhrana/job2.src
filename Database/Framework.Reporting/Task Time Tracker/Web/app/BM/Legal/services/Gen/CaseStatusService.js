'use strict';

angular.module('rootAppShell')
	.factory('caseStatusService', [
		'$resource',
		function ($resource) {
			return $resource('./api/caseStatus/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/caseStatus/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/caseStatus/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/caseStatus/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/caseStatus/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/caseStatus/Delete/:detailId' }
				}
				);
			}
		]);

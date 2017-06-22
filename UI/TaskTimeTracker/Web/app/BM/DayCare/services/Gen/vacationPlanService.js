'use strict';

angular.module('rootAppShell')
	.factory('vacationPlanService', [
		'$resource',
		function ($resource) {
			return $resource('./api/vacationPlan/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/vacationPlan/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/vacationPlan/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/vacationPlan/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/vacationPlan/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/vacationPlan/Delete/:detailId' }
				}
				);
			}
		]);

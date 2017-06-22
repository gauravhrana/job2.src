		'use strict';

		angular.module('rootAppShell')
			.factory('scheduleDetailService', [
				'$resource',
				function ($resource) {
					return $resource('./api/scheduleDetail/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/scheduleDetail/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/scheduleDetail/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/scheduleDetail/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/scheduleDetail/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/scheduleDetail/Delete/:detailId' }
						}
						);
					}
				]);

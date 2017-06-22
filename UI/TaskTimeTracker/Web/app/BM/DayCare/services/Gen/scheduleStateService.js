		'use strict';

		angular.module('rootAppShell')
			.factory('scheduleStateService', [
				'$resource',
				function ($resource) {
					return $resource('./api/scheduleState/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/scheduleState/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/scheduleState/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/scheduleState/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/scheduleState/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/scheduleState/Delete/:detailId' }
						}
						);
					}
				]);

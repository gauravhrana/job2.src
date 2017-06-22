'use strict';

angular.module('rootAppShell')
	.factory('businessCalenderService', [
		'$resource',
		function ($resource) {
			return $resource('./api/businessCalender/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/businessCalender/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/businessCalender/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/businessCalender/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/businessCalender/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/businessCalender/Delete/:detailId' }
				}
				);
			}
		]);

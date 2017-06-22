		'use strict';

		angular.module('rootAppShell')
			.factory('accountingCalenderService', [
				'$resource',
				function ($resource) {
					return $resource('./api/accountingCalender/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/accountingCalender/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/accountingCalender/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/accountingCalender/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/accountingCalender/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/accountingCalender/Delete/:detailId' }
						}
						);
					}
				]);

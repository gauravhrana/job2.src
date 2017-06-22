		'use strict';

		angular.module('rootAppShell')
			.factory('commissionRateService', [
				'$resource',
				function ($resource) {
					return $resource('./api/commissionRate/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/commissionRate/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/commissionRate/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/commissionRate/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/commissionRate/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/commissionRate/Delete/:detailId' }
						}
						);
					}
				]);

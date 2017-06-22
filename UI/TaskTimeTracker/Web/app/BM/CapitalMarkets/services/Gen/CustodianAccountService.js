		'use strict';

		angular.module('rootAppShell')
			.factory('custodianAccountService', [
				'$resource',
				function ($resource) {
					return $resource('./api/custodianAccount/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/custodianAccount/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/custodianAccount/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/custodianAccount/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/custodianAccount/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/custodianAccount/Delete/:detailId' }
						}
						);
					}
				]);

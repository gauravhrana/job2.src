		'use strict';

		angular.module('rootAppShell')
			.factory('portfolioXCustodianAccountService', [
				'$resource',
				function ($resource) {
					return $resource('./api/portfolioXCustodianAccount/:detailId'
						, null
						, {
							   'getSourceEntityList': { method: 'GET', url: '../../apiV2/portfolioXCustodianAccount/GetSourceEntityList/:value', isArray: true }
							,   'getEntityRecords': { method: 'GET', url: '../../apiV2/portfolioXCustodianAccount/GetEntityRecords/:value/:value1', isArray: true }
							,   'addEntityRecords': { method: 'GET', url: '../../apiV2/portfolioXCustodianAccount/AddEntityRecords/:value1/:value2/:value3' }
							,   'removeEntityRecords': { method: 'GET', url: '../../apiV2/portfolioXCustodianAccount/RemoveEntityRecords/:value/:value1' }
						}
						);
					}
				]);

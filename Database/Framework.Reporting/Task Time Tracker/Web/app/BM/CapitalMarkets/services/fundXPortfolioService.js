'use strict';

angular.module('rootAppShell')
	.factory('fundXPortfolioService', [
		'$resource',
		function ($resource) {
		    return $resource('./api/fundXPortfolio/:detailId'
				, null
				, {
				        'getSourceEntityList': { method: 'GET', url: '../../apiV2/fundXPortfolio/GetSourceEntityList/:value', isArray: true }
					,   'getEntityRecords': { method: 'GET', url: '../../apiV2/fundXPortfolio/GetEntityRecords/:value/:value1', isArray: true }
					,   'addEntityRecords': { method: 'GET', url: '../../apiV2/fundXPortfolio/AddEntityRecords/:value1/:value2/:value3' }
                    ,   'removeEntityRecords': { method: 'GET', url: '../../apiV2/fundXPortfolio/RemoveEntityRecords/:value/:value1' }
				}
				);
		}
	]);

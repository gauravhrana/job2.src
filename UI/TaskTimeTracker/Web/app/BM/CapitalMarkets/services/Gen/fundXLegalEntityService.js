'use strict';

angular.module('rootAppShell')
	.factory('fundXLegalEntityService', [
		'$resource',
		function ($resource) {
			return $resource('./api/fundXLegalEntity/:detailId'
				, null
				, {
					   'getSourceEntityList': { method: 'GET', url: '../../apiV2/fundXLegalEntity/GetSourceEntityList/:value', isArray: true }
					,   'getEntityRecords': { method: 'GET', url: '../../apiV2/fundXLegalEntity/GetEntityRecords/:value/:value1', isArray: true }
					,   'addEntityRecords': { method: 'GET', url: '../../apiV2/fundXLegalEntity/AddEntityRecords/:value1/:value2/:value3' }
					,   'removeEntityRecords': { method: 'GET', url: '../../apiV2/fundXLegalEntity/RemoveEntityRecords/:value/:value1' }
				}
				);
			}
		]);

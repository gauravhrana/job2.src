'use strict';

angular.module('rootAppShell')
	.factory('marketCapitalizationCategoryService', [
		'$resource',
		function ($resource) {
			return $resource('./api/marketCapitalizationCategory/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/marketCapitalizationCategory/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/marketCapitalizationCategory/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/marketCapitalizationCategory/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/marketCapitalizationCategory/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/marketCapitalizationCategory/Delete/:detailId' }
				}
				);
			}
		]);

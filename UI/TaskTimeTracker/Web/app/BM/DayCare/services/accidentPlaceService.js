'use strict';

angular.module('rootAppShell')
	.factory('accidentPlaceService', [
		'$resource',
		function ($resource) {
			return $resource('./api/accidentPlace/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/accidentPlace/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/accidentPlace/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/accidentPlace/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/accidentPlace/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/accidentPlace/Delete/:detailId' }
				}
				);
			}
		]);

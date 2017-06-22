'use strict';

angular.module('rootAppShell')
	.factory('ratingService', [
		'$resource',
		function ($resource) {
			return $resource('./api/rating/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/rating/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/rating/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/rating/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/rating/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/rating/Delete/:detailId' }
				}
				);
			}
		]);

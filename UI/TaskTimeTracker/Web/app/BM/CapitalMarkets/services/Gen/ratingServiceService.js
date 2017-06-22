		'use strict';

		angular.module('rootAppShell')
			.factory('ratingServiceService', [
				'$resource',
				function ($resource) {
					return $resource('./api/ratingService/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/ratingService/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/ratingService/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/ratingService/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/ratingService/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/ratingService/Delete/:detailId' }
						}
						);
					}
				]);

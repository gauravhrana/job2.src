'use strict';

angular.module('rootAppShell')
	.factory('investingFeederService', [
		'$resource',
		function ($resource) {
			return $resource('./api/investingFeeder/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/investingFeeder/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/investingFeeder/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/investingFeeder/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/investingFeeder/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/investingFeeder/Delete/:detailId' }
				}
				);
			}
		]);

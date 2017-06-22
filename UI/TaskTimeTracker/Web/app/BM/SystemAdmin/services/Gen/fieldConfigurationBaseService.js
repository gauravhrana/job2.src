'use strict';

angular.module('rootAppShell')
	.factory('fieldConfigurationBaseService', [
		'$resource',
		function ($resource) {
			return $resource('./api/fieldConfigurationBase/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/fieldConfigurationBase/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/fieldConfigurationBase/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/fieldConfigurationBase/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/fieldConfigurationBase/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/fieldConfigurationBase/Delete/:detailId' }
				}
				);
			}
		]);

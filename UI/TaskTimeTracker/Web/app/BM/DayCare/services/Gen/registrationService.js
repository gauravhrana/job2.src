'use strict';

angular.module('rootAppShell')
	.factory('registrationService', [
		'$resource',
		function ($resource) {
			return $resource('./api/registration/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/registration/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/registration/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/registration/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/registration/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/registration/Delete/:detailId' }
				}
				);
			}
		]);

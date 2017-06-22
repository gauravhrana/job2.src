'use strict';

angular.module('rootAppShell')
	.factory('optionService', [
		'$resource',
		function ($resource) {
			return $resource('./api/option/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/option/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/option/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/option/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/option/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/option/Delete/:detailId' }
				}
				);
			}
		]);

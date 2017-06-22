		'use strict';

		angular.module('rootAppShell')
			.factory('countryService', [
				'$resource',
				function ($resource) {
					return $resource('./api/country/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/country/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/country/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/country/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/country/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/country/Delete/:detailId' }
						}
						);
					}
				]);

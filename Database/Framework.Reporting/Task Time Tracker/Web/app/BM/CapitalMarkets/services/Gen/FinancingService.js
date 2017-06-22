		'use strict';

		angular.module('rootAppShell')
			.factory('financingService', [
				'$resource',
				function ($resource) {
					return $resource('./api/financing/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/financing/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/financing/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/financing/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/financing/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/financing/Delete/:detailId' }
						}
						);
					}
				]);

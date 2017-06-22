'use strict';

angular.module('rootAppShell')
	.factory('investmentTypeService', [
		'$resource',
		function ($resource) {
			return $resource('./api/investmentType/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/investmentType/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/investmentType/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/investmentType/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/investmentType/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/investmentType/Delete/:detailId' }
				}
				);
			}
		]);

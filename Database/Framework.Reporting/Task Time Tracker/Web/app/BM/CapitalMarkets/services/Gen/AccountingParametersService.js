'use strict';

angular.module('rootAppShell')
	.factory('accountingParametersService', [
		'$resource',
		function ($resource) {
			return $resource('./api/accountingParameters/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/accountingParameters/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/accountingParameters/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/accountingParameters/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/accountingParameters/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/accountingParameters/Delete/:detailId' }
				}
				);
			}
		]);

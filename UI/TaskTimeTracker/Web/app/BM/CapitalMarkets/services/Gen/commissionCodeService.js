'use strict';

angular.module('rootAppShell')
	.factory('commissionCodeService', [
		'$resource',
		function ($resource) {
			return $resource('./api/commissionCode/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/commissionCode/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/commissionCode/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/commissionCode/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/commissionCode/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/commissionCode/Delete/:detailId' }
				}
				);
			}
		]);

		'use strict';

		angular.module('rootAppShell')
			.factory('assetTypeService', [
				'$resource',
				function ($resource) {
					return $resource('./api/assetType/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/assetType/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/assetType/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/assetType/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/assetType/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/assetType/Delete/:detailId' }
						}
						);
					}
				]);

'use strict';

angular.module('rootAppShell')
	.factory('mSPAFileService', [
		'$resource',
		function ($resource) {
			return $resource('./api/mSPAFile/:detailId'
				, null
				, {
						'getById': { method: 'GET', url: '../../apiV2/mSPAFile/GetById/:detailId' }
					,   'getList': { method: 'GET', url: '../../apiV2/mSPAFile/GetList', isArray: true }
					,   'create': { method: 'POST', url: '../../apiV2/mSPAFile/Create' }
					,   'update': { method: 'POST', url: '../../apiV2/mSPAFile/Update' }
					,   'delete': { method: 'DELETE', url: '../../apiV2/mSPAFile/Delete/:detailId' }
				}
				);
			}
		]);

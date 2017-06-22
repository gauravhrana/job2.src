		'use strict';

		angular.module('rootAppShell')
			.factory('accountExcecutiveService', [
				'$resource',
				function ($resource) {
					return $resource('./api/accountExcecutive/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/accountExcecutive/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/accountExcecutive/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/accountExcecutive/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/accountExcecutive/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/accountExcecutive/Delete/:detailId' }
						}
						);
					}
				]);

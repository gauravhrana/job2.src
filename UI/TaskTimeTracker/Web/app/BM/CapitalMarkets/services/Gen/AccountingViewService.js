		'use strict';

		angular.module('rootAppShell')
			.factory('accountingViewService', [
				'$resource',
				function ($resource) {
					return $resource('./api/accountingView/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/accountingView/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/accountingView/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/accountingView/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/accountingView/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/accountingView/Delete/:detailId' }
						}
						);
					}
				]);

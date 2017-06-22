		'use strict';

		angular.module('rootAppShell')
			.factory('mSPAFileEventService', [
				'$resource',
				function ($resource) {
					return $resource('./api/mSPAFileEvent/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/mSPAFileEvent/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/mSPAFileEvent/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/mSPAFileEvent/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/mSPAFileEvent/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/mSPAFileEvent/Delete/:detailId' }
						}
						);
					}
				]);

				'use strict';

				angular.module('rootAppShell')
					.factory('activityTypeService', [
						'$resource',
						function ($resource) {
							return $resource('./api/activityType/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/activityType/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/activityType/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/activityType/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/activityType/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/activityType/Delete/:detailId' }
								}
								);
							}
						]);

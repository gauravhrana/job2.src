						'use strict';

						angular.module('rootAppShell')
							.factory('activitySubTypeService', [
								'$resource',
								function ($resource) {
									return $resource('./api/activitySubType/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/activitySubType/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/activitySubType/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/activitySubType/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/activitySubType/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/activitySubType/Delete/:detailId' }
										}
										);
									}
								]);

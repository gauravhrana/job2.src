				'use strict';

				angular.module('rootAppShell')
					.factory('scheduleDetailActivityCategoryService', [
						'$resource',
						function ($resource) {
							return $resource('./api/scheduleDetailActivityCategory/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/scheduleDetailActivityCategory/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/scheduleDetailActivityCategory/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/scheduleDetailActivityCategory/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/scheduleDetailActivityCategory/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/scheduleDetailActivityCategory/Delete/:detailId' }
								}
								);
							}
						]);

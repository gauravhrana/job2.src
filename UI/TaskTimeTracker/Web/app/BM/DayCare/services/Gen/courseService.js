				'use strict';

				angular.module('rootAppShell')
					.factory('courseService', [
						'$resource',
						function ($resource) {
							return $resource('./api/course/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/course/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/course/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/course/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/course/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/course/Delete/:detailId' }
								}
								);
							}
						]);

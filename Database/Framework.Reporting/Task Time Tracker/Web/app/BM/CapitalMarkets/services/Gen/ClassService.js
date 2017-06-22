				'use strict';

				angular.module('rootAppShell')
					.factory('classService', [
						'$resource',
						function ($resource) {
							return $resource('./api/class/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/class/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/class/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/class/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/class/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/class/Delete/:detailId' }
								}
								);
							}
						]);

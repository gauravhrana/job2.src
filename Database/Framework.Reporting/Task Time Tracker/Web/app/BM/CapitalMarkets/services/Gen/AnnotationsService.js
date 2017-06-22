				'use strict';

				angular.module('rootAppShell')
					.factory('annotationsService', [
						'$resource',
						function ($resource) {
							return $resource('./api/annotations/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/annotations/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/annotations/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/annotations/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/annotations/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/annotations/Delete/:detailId' }
								}
								);
							}
						]);

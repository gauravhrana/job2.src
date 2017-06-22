				'use strict';

				angular.module('rootAppShell')
					.factory('tWRSetService', [
						'$resource',
						function ($resource) {
							return $resource('./api/tWRSet/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/tWRSet/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/tWRSet/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/tWRSet/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/tWRSet/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/tWRSet/Delete/:detailId' }
								}
								);
							}
						]);

				'use strict';

				angular.module('rootAppShell')
					.factory('sectorService', [
						'$resource',
						function ($resource) {
							return $resource('./api/sector/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/sector/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/sector/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/sector/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/sector/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/sector/Delete/:detailId' }
								}
								);
							}
						]);

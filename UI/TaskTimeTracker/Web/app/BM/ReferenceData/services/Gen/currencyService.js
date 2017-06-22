				'use strict';

				angular.module('rootAppShell')
					.factory('currencyService', [
						'$resource',
						function ($resource) {
							return $resource('./api/currency/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/currency/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/currency/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/currency/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/currency/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/currency/Delete/:detailId' }
								}
								);
							}
						]);

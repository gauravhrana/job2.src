				'use strict';

				angular.module('rootAppShell')
					.factory('investmentThemeService', [
						'$resource',
						function ($resource) {
							return $resource('./api/investmentTheme/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/investmentTheme/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/investmentTheme/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/investmentTheme/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/investmentTheme/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/investmentTheme/Delete/:detailId' }
								}
								);
							}
						]);

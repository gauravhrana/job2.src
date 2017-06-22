						'use strict';

						angular.module('rootAppShell')
							.factory('securityClassService', [
								'$resource',
								function ($resource) {
									return $resource('./api/securityClass/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/securityClass/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/securityClass/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/securityClass/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/securityClass/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/securityClass/Delete/:detailId' }
										}
										);
									}
								]);

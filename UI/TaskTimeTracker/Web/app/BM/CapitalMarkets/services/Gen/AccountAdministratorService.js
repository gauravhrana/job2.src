						'use strict';

						angular.module('rootAppShell')
							.factory('accountAdministratorService', [
								'$resource',
								function ($resource) {
									return $resource('./api/accountAdministrator/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/accountAdministrator/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/accountAdministrator/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/accountAdministrator/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/accountAdministrator/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/accountAdministrator/Delete/:detailId' }
										}
										);
									}
								]);

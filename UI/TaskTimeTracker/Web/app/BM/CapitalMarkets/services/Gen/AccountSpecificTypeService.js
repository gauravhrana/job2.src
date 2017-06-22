				'use strict';

				angular.module('rootAppShell')
					.factory('accountSpecificTypeService', [
						'$resource',
						function ($resource) {
							return $resource('./api/accountSpecificType/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/accountSpecificType/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/accountSpecificType/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/accountSpecificType/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/accountSpecificType/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/accountSpecificType/Delete/:detailId' }
								}
								);
							}
						]);

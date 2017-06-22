				'use strict';

				angular.module('rootAppShell')
					.factory('custodianService', [
						'$resource',
						function ($resource) {
							return $resource('./api/custodian/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/custodian/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/custodian/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/custodian/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/custodian/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/custodian/Delete/:detailId' }
								}
								);
							}
						]);

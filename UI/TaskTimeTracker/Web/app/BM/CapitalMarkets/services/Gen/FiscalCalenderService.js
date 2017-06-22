				'use strict';

				angular.module('rootAppShell')
					.factory('fiscalCalenderService', [
						'$resource',
						function ($resource) {
							return $resource('./api/fiscalCalender/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/fiscalCalender/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/fiscalCalender/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/fiscalCalender/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/fiscalCalender/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/fiscalCalender/Delete/:detailId' }
								}
								);
							}
						]);

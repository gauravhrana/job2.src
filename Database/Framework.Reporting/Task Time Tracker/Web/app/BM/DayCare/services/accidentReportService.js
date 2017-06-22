		'use strict';

		angular.module('rootAppShell')
			.factory('accidentReportService', [
				'$resource',
				function ($resource) {
					return $resource('./api/accidentReport/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/accidentReport/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/accidentReport/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/accidentReport/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/accidentReport/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/accidentReport/Delete/:detailId' }
						}
						);
					}
				]);

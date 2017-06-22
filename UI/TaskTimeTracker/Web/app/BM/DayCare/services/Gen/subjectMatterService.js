		'use strict';

		angular.module('rootAppShell')
			.factory('subjectMatterService', [
				'$resource',
				function ($resource) {
					return $resource('./api/subjectMatter/:detailId'
						, null
						, {
								'getById': { method: 'GET', url: '../../apiV2/subjectMatter/GetById/:detailId' }
							,   'getList': { method: 'GET', url: '../../apiV2/subjectMatter/GetList', isArray: true }
							,   'create': { method: 'POST', url: '../../apiV2/subjectMatter/Create' }
							,   'update': { method: 'POST', url: '../../apiV2/subjectMatter/Update' }
							,   'delete': { method: 'DELETE', url: '../../apiV2/subjectMatter/Delete/:detailId' }
						}
						);
					}
				]);

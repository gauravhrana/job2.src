								'use strict';

								angular.module('rootAppShell')
									.factory('subIndustryService', [
										'$resource',
										function ($resource) {
											return $resource('./api/subIndustry/:detailId'
												, null
												, {
														'getById': { method: 'GET', url: '../../apiV2/subIndustry/GetById/:detailId' }
													,   'getList': { method: 'GET', url: '../../apiV2/subIndustry/GetList', isArray: true }
													,   'create': { method: 'POST', url: '../../apiV2/subIndustry/Create' }
													,   'update': { method: 'POST', url: '../../apiV2/subIndustry/Update' }
													,   'delete': { method: 'DELETE', url: '../../apiV2/subIndustry/Delete/:detailId' }
												}
												);
											}
										]);

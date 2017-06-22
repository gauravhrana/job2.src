																				'use strict';

																				angular.module('rootAppShell')
																					.factory('portfolioGroupRulesService', [
																						'$resource',
																						function ($resource) {
																							return $resource('./api/portfolioGroupRules/:detailId'
																								, null
																								, {
																										'getById': { method: 'GET', url: '../../apiV2/portfolioGroupRules/GetById/:detailId' }
																									,   'getList': { method: 'GET', url: '../../apiV2/portfolioGroupRules/GetList', isArray: true }
																									,   'create': { method: 'POST', url: '../../apiV2/portfolioGroupRules/Create' }
																									,   'update': { method: 'POST', url: '../../apiV2/portfolioGroupRules/Update' }
																									,   'delete': { method: 'DELETE', url: '../../apiV2/portfolioGroupRules/Delete/:detailId' }
																								}
																								);
																							}
																						]);

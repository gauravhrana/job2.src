														'use strict';

														angular.module('rootAppShell')
															.factory('reportingRequirementService', [
																'$resource',
																function ($resource) {
																	return $resource('./api/reportingRequirement/:detailId'
																		, null
																		, {
																				'getById': { method: 'GET', url: '../../apiV2/reportingRequirement/GetById/:detailId' }
																			,   'getList': { method: 'GET', url: '../../apiV2/reportingRequirement/GetList', isArray: true }
																			,   'create': { method: 'POST', url: '../../apiV2/reportingRequirement/Create' }
																			,   'update': { method: 'POST', url: '../../apiV2/reportingRequirement/Update' }
																			,   'delete': { method: 'DELETE', url: '../../apiV2/reportingRequirement/Delete/:detailId' }
																		}
																		);
																	}
																]);

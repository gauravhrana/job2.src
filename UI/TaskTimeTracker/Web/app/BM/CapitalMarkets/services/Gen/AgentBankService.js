														'use strict';

														angular.module('rootAppShell')
															.factory('agentBankService', [
																'$resource',
																function ($resource) {
																	return $resource('./api/agentBank/:detailId'
																		, null
																		, {
																				'getById': { method: 'GET', url: '../../apiV2/agentBank/GetById/:detailId' }
																			,   'getList': { method: 'GET', url: '../../apiV2/agentBank/GetList', isArray: true }
																			,   'create': { method: 'POST', url: '../../apiV2/agentBank/Create' }
																			,   'update': { method: 'POST', url: '../../apiV2/agentBank/Update' }
																			,   'delete': { method: 'DELETE', url: '../../apiV2/agentBank/Delete/:detailId' }
																		}
																		);
																	}
																]);

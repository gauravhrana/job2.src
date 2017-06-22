																								'use strict';

																								angular.module('rootAppShell')
																									.factory('forwardFXContractService', [
																										'$resource',
																										function ($resource) {
																											return $resource('./api/forwardFXContract/:detailId'
																												, null
																												, {
																														'getById': { method: 'GET', url: '../../apiV2/forwardFXContract/GetById/:detailId' }
																													,   'getList': { method: 'GET', url: '../../apiV2/forwardFXContract/GetList', isArray: true }
																													,   'create': { method: 'POST', url: '../../apiV2/forwardFXContract/Create' }
																													,   'update': { method: 'POST', url: '../../apiV2/forwardFXContract/Update' }
																													,   'delete': { method: 'DELETE', url: '../../apiV2/forwardFXContract/Delete/:detailId' }
																												}
																												);
																											}
																										]);

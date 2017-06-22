																												'use strict';

																												angular.module('rootAppShell')
																													.factory('companyDealTypeService', [
																														'$resource',
																														function ($resource) {
																															return $resource('./api/companyDealType/:detailId'
																																, null
																																, {
																																		'getById': { method: 'GET', url: '../../apiV2/companyDealType/GetById/:detailId' }
																																	,   'getList': { method: 'GET', url: '../../apiV2/companyDealType/GetList', isArray: true }
																																	,   'create': { method: 'POST', url: '../../apiV2/companyDealType/Create' }
																																	,   'update': { method: 'POST', url: '../../apiV2/companyDealType/Update' }
																																	,   'delete': { method: 'DELETE', url: '../../apiV2/companyDealType/Delete/:detailId' }
																																}
																																);
																															}
																														]);

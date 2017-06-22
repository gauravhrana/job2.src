						'use strict';

						angular.module('rootAppShell')
							.factory('geographicRegionService', [
								'$resource',
								function ($resource) {
									return $resource('./api/geographicRegion/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/geographicRegion/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/geographicRegion/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/geographicRegion/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/geographicRegion/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/geographicRegion/Delete/:detailId' }
										}
										);
									}
								]);

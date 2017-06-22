						'use strict';

						angular.module('rootAppShell')
							.factory('tWRBatchProcessingService', [
								'$resource',
								function ($resource) {
									return $resource('./api/tWRBatchProcessing/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/tWRBatchProcessing/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/tWRBatchProcessing/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/tWRBatchProcessing/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/tWRBatchProcessing/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/tWRBatchProcessing/Delete/:detailId' }
										}
										);
									}
								]);

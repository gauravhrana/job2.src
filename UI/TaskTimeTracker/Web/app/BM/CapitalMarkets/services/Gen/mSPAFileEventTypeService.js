						'use strict';

						angular.module('rootAppShell')
							.factory('mSPAFileEventTypeService', [
								'$resource',
								function ($resource) {
									return $resource('./api/mSPAFileEventType/:detailId'
										, null
										, {
												'getById': { method: 'GET', url: '../../apiV2/mSPAFileEventType/GetById/:detailId' }
											,   'getList': { method: 'GET', url: '../../apiV2/mSPAFileEventType/GetList', isArray: true }
											,   'create': { method: 'POST', url: '../../apiV2/mSPAFileEventType/Create' }
											,   'update': { method: 'POST', url: '../../apiV2/mSPAFileEventType/Update' }
											,   'delete': { method: 'DELETE', url: '../../apiV2/mSPAFileEventType/Delete/:detailId' }
										}
										);
									}
								]);

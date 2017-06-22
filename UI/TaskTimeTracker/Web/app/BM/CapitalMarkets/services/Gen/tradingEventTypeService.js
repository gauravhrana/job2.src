				'use strict';

				angular.module('rootAppShell')
					.factory('tradingEventTypeService', [
						'$resource',
						function ($resource) {
							return $resource('./api/tradingEventType/:detailId'
								, null
								, {
										'getById': { method: 'GET', url: '../../apiV2/tradingEventType/GetById/:detailId' }
									,   'getList': { method: 'GET', url: '../../apiV2/tradingEventType/GetList', isArray: true }
									,   'create': { method: 'POST', url: '../../apiV2/tradingEventType/Create' }
									,   'update': { method: 'POST', url: '../../apiV2/tradingEventType/Update' }
									,   'delete': { method: 'DELETE', url: '../../apiV2/tradingEventType/Delete/:detailId' }
								}
								);
							}
						]);

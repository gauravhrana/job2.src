								'use strict';

								angular.module('rootAppShell')
									.factory('bathRoomService', [
										'$resource',
										function ($resource) {
											return $resource('./api/bathRoom/:detailId'
												, null
												, {
														'getById': { method: 'GET', url: '../../apiV2/bathRoom/GetById/:detailId' }
													,   'getList': { method: 'GET', url: '../../apiV2/bathRoom/GetList', isArray: true }
													,   'create': { method: 'POST', url: '../../apiV2/bathRoom/Create' }
													,   'update': { method: 'POST', url: '../../apiV2/bathRoom/Update' }
													,   'delete': { method: 'DELETE', url: '../../apiV2/bathRoom/Delete/:detailId' }
												}
												);
											}
										]);

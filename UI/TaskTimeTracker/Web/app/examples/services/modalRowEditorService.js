'use strict';

	angular.module('rootAppShell').factory('scheduleStateService', [
      '$resource',
      function ($resource) {
          return $resource('./api/scheduleState/:detailId'
              , null
              , {
                  'getList': { method: 'GET', url: '/apiV2/scheduleState/GetListByApplication/:value', isArray: true }

              }
              );
      }
]);


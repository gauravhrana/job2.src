'use strict';

angular.module('rootAppShell')
    .controller('logoutCtrl', [
        '$location', 'userService', function($location, userService) {
            userService.removeAuthentication();
            $location.url('main');
        }
    ]);


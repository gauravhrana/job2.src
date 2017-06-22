/*global jQuery:true */
'use strict';

angular.module('rootAppShell')
    .controller('loginCtrl', [
        '$scope', '$location', 'userService', function($scope, $location, userService) {
            $scope.username = '';
            $scope.password = '';
            $scope.errors = [];
            $scope.persist = false;
            $scope.CanSubmit = true;
            $scope.SubmitMessage = 'Submit';

            //identify the next route to go to after login
            var nextRoute = null;
            try {
                nextRoute = userService.getNextRoute();
            } catch (e) {
                nextRoute = null;
            }

            if (nextRoute !== null) {
                var nameBuffer = nextRoute.path + '';
                nextRoute = {
                    path: nameBuffer
                };
                userService.clearNextRoute();
            }

            //identify any errors that might have resulted in coming to login            
            var nextError = userService.getNextError();
            var errorBuffer = nextError.message + '';
            nextError = {
                message: errorBuffer
            };
            userService.clearNextError();


            if (typeof nextError.message === 'string' && nextError.message !== '' && $scope.errors.indexOf(nextError.message) === -1) {
                $scope.errors.push(nextError.message);
            }

            function disableLoginButton(message) {
                if (typeof message !== 'string') {
                    message = 'Attempting login...';
                }
                $scope.SubmitMessage = message;
                $scope.CanSubmit = false;

            }

            function enableLoginButton(message) {
                if (typeof message !== 'string') {
                    message = 'Submit';
                }
                $scope.SubmitMessage = message;
                $scope.CanSubmit = true;
            }

            function onSuccessfulLogin() {
                $scope.SubmitMessage = "GoodToGo";
                if (nextRoute !== null && typeof nextRoute.path === 'string' && nextRoute.path !== '') {
                    $location.url(nextRoute.path);
                } else {
                    $location.url('/main');
                }
            }

            function onFailedLogin(error) {
                if (typeof error === 'string' && $scope.errors.indexOf(error) === -1) {
                    $scope.errors.push(error);
                }
                enableLoginButton();
            }

            $scope.login = function() {
                disableLoginButton();
                userService.authenticate($scope.username, $scope.password, onSuccessfulLogin, onFailedLogin, $scope.persist);
            };
            
                //this.VerifyString = 'George';// +
                //this.VerifyFunction = function () {
                //    return 'George the Function';
                //}
        }
    ]);
'use strict';

angular.module('rootAppShell')
    .service('userService', [
        '$http', '$cacheFactory', 'loggingService', '$location', '$timeout'
        , function ($http, $cacheFactory, loggingService, $location, $timeout) {

            function NoAuthenticationException(message) {
                this.name = 'AuthenticationRequired';
                this.message = message;
            }

            function AuthenticationExpiredException(message) {
                this.name = 'AuthenticationExpired';
                this.message = message;
            }

            function AuthenticationRetrievalException(message) {
                this.name = 'AuthenticationRetrieval';
                this.message = message;
            }

            var userData = {
                isAuthenticated: false,
                username: 'none',
                bearerToken: '',
                expirationDate: null,
            };

            var nextRoute = {
                path: ''
            };

            var previousRoute = {
                path: ''
            };

            var nextError = {
                message: ''
            };

            function isAuthenticationExpired(expirationDate) {
                var now = new Date();
                expirationDate = new Date(expirationDate);
                if (expirationDate - now > 0) {
                    return false;
                } else {
                    return true;
                }
            }

            function saveData(persistData) {
                removeData();
                var expires = null;
                if (persistData) {
                    expires = 3650;
                }
                $.cookie("auth_data", JSON.stringify(userData), { expires: expires });
                //$.cookie("auth_data", 'd', '1/1/2013');
            }

            function removeData() {
                $.removeCookie('auth_data');
                //$cacheFactory.get('$http').removeAll();
                //return;
            }

            function retrieveSavedData() {
                var cookieData = $.cookie("auth_data");
                var savedData;
                if (cookieData != undefined) {
                    savedData = JSON.parse($.cookie("auth_data"));
                }

                if (typeof savedData === 'undefined') {
                    throw new AuthenticationRetrievalException('No authentication data exists');
                } else if (isAuthenticationExpired(savedData.expirationDate)) {
                    throw new AuthenticationExpiredException('Authentication token has already expired');
                } else {
                    userData.isAuthenticated = savedData.isAuthenticated;
                    userData.username = savedData.username;
                    userData.bearerToken = savedData.bearerToken;
                    userData.expirationDate = savedData.expirationDate;
                    setHttpAuthHeader();
                }
            }

            function clearUserData() {
                userData.isAuthenticated = false;
                userData.username = '';
                userData.bearerToken = '';
                userData.expirationDate = null;
            }

            function setHttpAuthHeader() {
                $http.defaults.headers.common.Authorization = 'Bearer ' + userData.bearerToken;
            }

            this.isAuthenticated = function() {
                if (userData.isAuthenticated && !isAuthenticationExpired(userData.expirationDate)) {
                    return true;
                } else {
                    try {
                        retrieveSavedData();
                    } catch(e) {
                        throw new NoAuthenticationException(e.message);
                    }
                    return true;
                }
            };

            this.getPreviousRoute = function() {
                return previousRoute;
            };

            this.setPreviousRoute = function(path) {
                previousRoute.path = path;
            };

            this.clearPreviousRoute = function() {
                previousRoute.path = '';
            };


            this.getNextRoute = function() {
                return nextRoute;
            };

            this.setNextRoute = function(path) {
                nextRoute.path = path;
            };

            this.clearNextRoute = function() {
                nextRoute.path = '';
            };

            this.getNextError = function() {
                return nextError;
            };

            this.setNextError = function(message) {
                nextError.message = message;
            };

            this.clearNextError = function() {
                nextError.message = '';
            };

            this.getUserData = function() {
                //userData.isAuthenticated = this.isAuthenticated();
                return userData;
            };

            this.removeAuthentication = function() {
                removeData();
                clearUserData();
                $http.defaults.headers.common.Authorization = null;
            };

            this.authenticate = function(username, password, successCallback, errorCallback, persistData) {
                this.removeAuthentication();
                var config = {
                    method: 'POST',
                    url: './token',
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded',
                    },
                    data: 'grant_type=password&username=' + username + '&password=' + password,
                };

                $http(config)
                    .success(function(data) {
                        userData.isAuthenticated = true;
                        userData.username = data.userName;
                        userData.bearerToken = data.access_token;
                        userData.expirationDate = new Date(data['.expires']);
                        setHttpAuthHeader();
                        saveData(persistData);
                        if (typeof successCallback === 'function') {
                            successCallback();
                        }
                    })
                    .error(function(data) {
                        if (typeof errorCallback === 'function') {
                            if (data.error_description) {
                                errorCallback(data.error_description);
                            } else {
                                errorCallback('Unable to contact server; please, try again later.');
                            }
                        }
                    });
            };

            this.AlertManager = new function() {

                this.successList = [
                    { type: 'success', msg: 'Starter - Well done! 1' },
                    { type: 'success', msg: 'Well done! 2' }
                ];

                this.failureList = [
                    { type: 'danger', msg: 'Starter - Fail 1.' },
                    { type: 'danger', msg: 'Starter - Fail 2.' },
                    { type: 'danger', msg: 'Starter - Fail 3.' }
                ];

                this.addSuccessAlert = function (message) {
                    message = new Date().toLocaleTimeString() + ' - ' + message;
                    this.successList.push({ type: 'success', msg: message });

                    //var successList = this.successList;
                    
                    //$timeout(function () {
                    //    successList.splice(0, 1);
                    //}, 5000);
                };

                this.logFailureAlert = function (clientMessage, serverError, viewModel) {
                    
                    var message = clientMessage + '\n';
                    
                    message += '\tExceptionMessage: "' + serverError.ExceptionMessage + '"\n';
                    message += '\tExceptionType: "' + serverError.ExceptionType + '"\n';
                    message += '\tMessage: "' + serverError.Message + '"\n';
                                                                                
                    loggingService.create(
                        {   
                                Message: message
                            ,   Level: "Error"
                            ,   ViewModel: viewModel
                            ,   URL: $location.url()
                            ,   StackTrace: serverError.StackTrace
                        }
                    );
                };
                                
                this.addFailureAlert = function (message) {
                    message = new Date().toLocaleTimeString() + ' - ' + message;
                    this.failureList.push({ type: 'danger', msg: message });
                };

                this.addFailureResponse = function (failureResonse, viewModel) {

                    this.logFailureAlert('', failureResonse.data, viewModel);
                    
                    var ex = angular.copy(failureResonse.data);

                    while (ex.InnerException != undefined) {
                        ex = angular.copy(ex.InnerException);
                    }
                    if (ex.ExceptionMessage == undefined) {
                        this.addFailureAlert("an unknown error has occurred.");
                    } else {
                        this.addFailureAlert(ex.ExceptionMessage);
                    }
                };

                this.clearAllFailure = function() {
                    while (this.failureList.length > 0) {
                        this.failureList.pop();
                    }
                };

                this.clearAllSuccess = function () {
                    while (this.successList.length > 0) {
                        this.successList.pop();
                    }
                };

                this.Init = function() {
                    this.clearAllFailure();
                    this.clearAllSuccess();
                };

                this.Init();
            };
        }
    ]);


/**
 * Requires jQuery and jQuery.cookie (https://github.com/carhartl/jquery-cookie)
 * - Adds an 'options' parameter to $cookieStore.put and $cookieStore.remove.
 * - Default options can be set by calling $cookieStoreProvider.setDefaultOptions
 */
angular.module('rootAppShell')
    .provider('cookieStore', [function () {
        var self = this;
        self.defaultOptions = {};

        self.setDefaultOptions = function (options) {
            self.defaultOptions = options;
        };

        self.$get = function () {
            return {
                get: function (name) {
                    var jsonCookie = $.cookie(name);
                    if (jsonCookie) {
                        return angular.fromJson(jsonCookie);
                    }
                },
                put: function (name, value, options) {
                    options = $.extend({}, self.defaultOptions, options);
                    $.cookie(name, angular.toJson(value), options);
                },
                remove: function (name, options) {
                    options = $.extend({}, self.defaultOptions, options);
                    $.removeCookie(name, options);
                }
            };
        };
    }]);
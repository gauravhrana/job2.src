﻿'use strict';

/*
This directive allows us to pass a function in on an enter key to do what we want.
ng-enter="myFunction()"
*/
angular.module('rootAppShell')
    .directive('ngEnter', function () {        
        return function (scope, element, attrs) {            
            element.bind("keydown keypress", function(event) {
                if (event.which === 13) {
                    scope.$apply(function() {
                        scope.$eval(attrs.ngEnter);
                    });

                    event.preventDefault();
                }
            });
        };
    });

'use strict';

angular.module('rootAppShell')
    .directive('fillHeight', function($window) {
            return {
                link:
                    function(scope, element,attrs) {
                        scope.e = angular.element(element);
                        scope.w = angular.element($window);
                        scope.b = parseInt(attrs["bottomOffset"]);
                        if (isNaN(scope.b)) {
                            scope.b = 0;
                        }

                        scope.getDimensions = function() {
                            return {
                                'height': scope.w.height(),
                                'top': scope.e.offset().top,
                                'bottomOffset': scope.b
                            };
                        };

                        scope.$watch(scope.getDimensions, function (newValue, oldValue) {
                            scope.windowHeight = newValue.height;
                            scope.elementTop = newValue.top;
                            scope.bottomOffset = newValue.bottomOffset;
                            scope.style = function() {
                                return {
                                    'height': (scope.windowHeight - scope.elementTop - scope.bottomOffset) + 'px',
                                    //'background-color': 'red'
                                };
                            };
                        }, true);
                        
                        scope.w.bind('resize', function() {
                            scope.$apply();
                        });
                    }
            }
        }
    );

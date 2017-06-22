'use strict';

angular.module('rootAppShell')
    .controller('navCtrl', [
        '$scope', '$window', 'dashboardService', function ($scope, $window, dashboardService) {
            $scope.isCollapsed = true;
            $scope.$on('$routeChangeSuccess', function () {
                $scope.isCollapsed = true;
            });

            $scope.swapStyleSheet = function (name) {
                var route = "";
                var parts = location.href.split("#");
                if (parts.length > 1)
                    route = parts[1];
                $window.location.href = "./Home/SetTheme/" + name + "?" + route;
            }

            //$scope.dashboardList = dashboardService.getList();

            $scope.menu = [
                            {
                                "itemId": 1, "title": "Working Items", "description": "Google Search Engine",
                                "subMenu": [
                                        { "title": "Application Mode", "href": "#/applicationMode/search" },
                                        { "title": "Application Role", "href": "#/applicationRole/search" },
                                        { "title": "Menu Category", "href": "#/menuCategory/search" },
                                        { "title": "Field Configuration Mode", "href": "#/fieldConfigurationMode/search" },
                                        { "title": "Language", "href": "#/language/search" }

                                ]
                            }
                        ];

            $scope.subMenu = null;
            $scope.activeMenu = null;

            // Default submenu left padding to 0
            $scope.subLeft = { 'padding-left': '0px' };

            /*
             * Set active item and submenu links
             */
            $scope.showSubMenu = function (item, pos) {
                // Move submenu based on position of parent
                $scope.subLeft = { 'padding-left': (80 * pos) + 'px' };
                // Set activeItem and sublinks to the currectly
                // selected item.
                $scope.activeMenu = item;
                $scope.subMenu = item.subMenu;
            };

        }
    ]);

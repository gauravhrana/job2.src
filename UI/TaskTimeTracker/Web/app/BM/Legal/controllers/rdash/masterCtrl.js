'use strict';

/**
 * Master Controller
 */

angular.module('rootAppShell')
    .controller('MasterCtrl', ['$scope', '$cookieStore', '$injector', 'menuService', '$routeParams', '$location', 'userService',
function MasterCtrl($scope, $cookieStore, $injector, menuService, $routeParams, $location, userService) {

    $scope.$on('$routeChangeSuccess', function () {

        // $routeParams should be populated here
        var x = $routeParams.moduleName;
        $scope.module = $routeParams.moduleName;

        if (angular.isDefined($cookieStore.get('applicationModuleLegal'))) {

            // switch in cookie if different module encountered
            var cookieModule = $cookieStore.get('applicationModuleLegal');
            if (cookieModule != $scope.module) {

                if ($scope.module == undefined) {
                    $scope.module = cookieModule;
                }

                $cookieStore.put('applicationModuleLegal', $scope.module);
                switchApplicationModuleServerSide($scope.module);
            }
        }
        else {          // set module in cookie if it not exists     

            if ($scope.module == '' || $scope.module == undefined) {
                $scope.module = 'Office';
            }

            $cookieStore.put('applicationModuleLegal', $scope.module);
            switchApplicationModuleServerSide($scope.module);
        }

        function switchApplicationModuleServerSide(newApplicationModule) {

            // call service to swtich module on server side also, so proper asp.net can be loaded.
            menuService.setApplicationModule({ value: JSON.stringify(newApplicationModule) }, onSuccessSwitchApplicationModule, onFailedLoad);
        }

        function onSuccessSwitchApplicationModule(data) {
        }

        $scope.CurrentApplicationCode = "";
        menuService.getApplicationCode(null, onSuccessLoadApplicationCode, onFailedLoad);

        function onSuccessLoadApplicationCode(data) {
            $scope.CurrentApplicationCode = data.Result;
            menuService.getUPMenu(null, onSuccessLoadUPMenu, onFailedLoad);
        }

        function onSuccessLoadUPMenu(data) {

            var moduleAAMenuData = _.where(data, { ApplicationModule: "AA" });

            $scope.menuAAModule = [];

            var parentMenus = _.where(moduleAAMenuData, { ParentMenuId: null });
            _.each(parentMenus, function (parentMenuObject) {

                var parentMenu = { text: parentMenuObject.MenuDisplayName, href: getMenuUrl(parentMenuObject.NavigateURL) };

                var childMenus = getChildMenus(parentMenuObject.MenuId, moduleAAMenuData);
                parentMenu.children = childMenus;

                $scope.menuAAModule.push(parentMenu);
            })
        }

        function getChildMenus(parentMenuId, moduleAAMenuData) {

            var childMenus = [];

            var menuItems = _.where(moduleAAMenuData, { ParentMenuId: parentMenuId });
            _.each(menuItems, function (menuItemObject) {

                var menuItem = { text: menuItemObject.MenuDisplayName, href: getMenuUrl(menuItemObject.NavigateURL) };

                var tmpChildMenus = getChildMenus(menuItemObject.MenuId, moduleAAMenuData);
                menuItem.children = tmpChildMenus;

                childMenus.push(menuItem);
            })

            return childMenus;

        }

        function getMenuUrl(navigateUrl) {

            var finalUrl = navigateUrl.replace("~/", "/");
            if (finalUrl.indexOf(".aspx") == -1 && finalUrl.indexOf("/Home") == -1)
            {
                if (finalUrl != "#") {
                    finalUrl = "/" + $scope.CurrentApplicationCode + "/" + $scope.module + finalUrl;
                }
            }

            return finalUrl;
        }

        function onFailedLoad(serverResponse) {
            userService.AlertManager.logFailureAlert('', serverResponse.data, []);
        }

        /**
         * Sidebar Toggle & Cookie Control
         */
        var mobileView = 992;

        $scope.SwitchModule = function (module) {

        };

        $scope.getWidth = function () {
            return window.innerWidth;
        };


        $scope.$watch($scope.getWidth, function (newValue, oldValue) {
            if (newValue >= mobileView) {
                if (angular.isDefined($cookieStore.get('toggle'))) {
                    $scope.toggle = !$cookieStore.get('toggle') ? false : true;
                } else {
                    $scope.toggle = true;
                }
            } else {
                $scope.toggle = false;
            }
        });

        $scope.toggleSidebar = function () {
            $scope.toggle = !$scope.toggle;
            $cookieStore.put('toggle', $scope.toggle);
        };

        window.onresize = function () {
            $scope.$apply();
        };
    });
}
    ]);
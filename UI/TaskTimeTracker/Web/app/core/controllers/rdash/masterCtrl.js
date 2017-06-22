'use strict';

/**
 * Master Controller
 */

angular.module('rootAppShell')
    .controller('MasterCtrl', ['$scope', '$cookieStore', 'userPreferenceUtilityService', MasterCtrl]);

function MasterCtrl($scope, $cookieStore, userPreferenceUtilityService) {
    /**
     * Sidebar Toggle & Cookie Control
     */

    $scope.toggle = false;
    var settingCategory = 'General';
    var upKey = 'DashBoardToggle';
    var mobileView = 992;

    $scope.getWidth = function () {
        return window.innerWidth;
    };

    
    $scope.$watch($scope.getWidth, function (newValue, oldValue) {

        if (newValue >= mobileView) {
            if (angular.isDefined($cookieStore.get('toggle'))) {
                $scope.toggle = !$cookieStore.get('toggle') ? false : true;
            } else {
                //$scope.toggle = true;

                //get from  UP Initial value
                userPreferenceUtilityService.getUPData({
                    value: upKey
                    , value1: settingCategory
                }, onSuccessGetUPData, onError);
            }
        } else {
            $scope.toggle = false;

            userPreferenceUtilityService.setUPData({
                value1: upKey
            , value2: settingCategory
            , value3: $scope.toggle
            }, onSuccessSetUPData, onError);
        }
    });

    $scope.toggleSidebar = function () {
        $scope.toggle = !$scope.toggle;
        $cookieStore.put('toggle', $scope.toggle);

        userPreferenceUtilityService.setUPData({
            value1: upKey
            , value2: settingCategory
            , value3: $scope.toggle
        }, onSuccessSetUPData, onError);
    };

    function onError(serverResponse) {
        alert(serverResponse.data);
    }

    function onSuccessSetUPData(serverResponse) { }

    function onSuccessGetUPData(data) {
        $scope.toggle = (data.Result === 'true');
    }

    window.onresize = function () {
        $scope.$apply();
    };
}
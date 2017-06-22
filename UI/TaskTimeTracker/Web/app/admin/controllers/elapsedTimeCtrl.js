'use strict';

angular.module('rootAppShell')
    .controller('elapsedTimeCtrl', [
        '$location', '$scope', '$injector', '$routeParams', '$modal', 'userService', 'dataAutoCompleteService', 'elapsedTimeService', '$timeout', '$log',
        function ($location, $scope, $injector, $routeParams, $modal, userService, dataAutoCompleteService, elapsedTimeService, $timeout, $log) {

            $scope.settingCategoryName = "elapsedTimeData";
            $scope.groupByOptions = [
                        {   name: "None"},
                        {   name: "ApplicationUser"},
                        {   name: "Computer"},
                        {   name: "ConnectionKey" }];

            // get computer names
            dataAutoCompleteService.getAutoCompleteList({ value: "Log4Net", value1: "Computer" },
                    function (response) {
                        $scope.computerNames = response;
                    }, onFailedLoad);

            // get computer key list
            dataAutoCompleteService.getAutoCompleteList({ value: "Log4Net", value1: "ConnectionKey" },
                    function (response) {
                        $scope.connectionKeyList = response;
                    }, onFailedLoad);

            //GetAllApplicationUserList

            elapsedTimeService.getStoredSearchData({
                value: $scope.settingCategoryName
            }, onSuccessGetStoredSearchData, onFailedLoad);

            $scope.saveSearch = false;

            function onSuccessGetStoredSearchData(data) {

                $scope.computerName                      = data.Computer;
                $scope.connectionKey                     = data.ConnectionKey;
                $scope.applicationUser                   = data.LogUser;
                $scope.groupBy                           = data.GroupBy;

                $scope.generateReport();
            } 

            function onFailedLoad(serverResponse) {
                userService.AlertManager.logFailureAlert('', serverResponse.data, []);
            }

            // load user drop down
            elapsedTimeService.listUsers(null, onSuccessLoadUsers, onFailedLoad);

            function onSuccessLoadUsers(data) {
                $scope.applicationUsers = data;
            }

            $scope.generateReport = function () {

                //if ($scope.connectionKey == "") {
                //    $scope.connectionKey = "''";
                //}

                //if ($scope.computerName == "") {
                //    $scope.computerName = "''";
                //}

                elapsedTimeService.getElapsedTimeData(
                    {
                        value1: JSON.stringify($scope.computerName),
                        value2: JSON.stringify($scope.connectionKey),
                        value3: $scope.applicationUser
                    }, onSuccessLoadElapsedTimeData, onFailedLoad);
                
                if ($scope.saveSearch) {

                    var searchObj = {};

                    searchObj.Computer      = $scope.computerName;
                    searchObj.ConnectionKey = $scope.connectionKey;
                    searchObj.LogUser       = $scope.applicationUser;
                    searchObj.GroupBy       = $scope.groupBy;

                    elapsedTimeService.setStoredSearchData({
                        value: $scope.settingCategoryName,
                        value1: JSON.stringify(searchObj)
                    }, onSuccessSetStoredSearchData, onFailedLoad);                    
                }

                $scope.saveSearch = true;
            }

            function onSuccessLoadElapsedTimeData(data) {
                
                $scope.groupingLevel = 0;

                if ($scope.groupBy == "None") {
                    $scope.elapsedTimeData = data;
                }
                else {
                    $scope.groups = {};
                    $scope.groupingLevel = 1;

                    var uGroup = [];
                    for (var i = 0; i < data.length; i++) {

                        var grpByValue = data[i][$scope.groupBy];

                        if (uGroup.indexOf(grpByValue) == -1) {
                            uGroup.push(grpByValue);
                        }

                        if ($scope.groups[grpByValue] == undefined) {
                            $scope.groups[grpByValue] = [];
                        }
                        $scope.groups[grpByValue].push(data[i]);
                    }

                    $scope.groupValues = uGroup;
                }

            }

            function onSuccessSetStoredSearchData(data) {
            }

        }
    ]);
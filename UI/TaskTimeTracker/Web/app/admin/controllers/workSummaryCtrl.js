'use strict';

angular.module('rootAppShell')
    .controller('workSummaryCtrl', [
        '$location', '$scope', '$injector', '$routeParams', '$modal', 'userService', 'workSummaryService', '$timeout', '$log',
        function ($location, $scope, $injector, $routeParams, $modal, userService, workSummaryService, $timeout, $log) {

            $scope.showWorkCategoryReport = false;
            $scope.showbranchSummaryReport = false;
            $scope.settingCategoryName = "workSummaryReport";

            $scope.saveSearch = false;

            function onFailedLoad(serverResponse) {
                userService.AlertManager.logFailureAlert('', serverResponse.data, []);
            }

            // load user drop down
            workSummaryService.listUsers(null, onSuccessLoadUsers, onFailedLoad);

            function onSuccessLoadUsers(data) {
                $scope.applicationUsers = data;

                workSummaryService.getStoredSearchData({
                    value: $scope.settingCategoryName
                }, onSuccessGetStoredSearchData, onFailedLoad);
            }

            function onSuccessGetStoredSearchData(data) {


                $scope.fromSearchDate = data.FromSearchDate;
                $scope.toSearchDate = data.ToSearchDate;

                // search for related records in result data
                var items = jQuery.grep($scope.applicationUsers, function (a) {
                    return a.ApplicationUserId == data.ApplicationUserId
                });

                if (items.length > 0) {
                    $scope.selectedUser = items[0];
                }
                else {
                    $scope.selectedUser = $scope.applicationUsers[0];
                }


                $scope.generateReport();
            }

            $scope.generateReport = function () {

                workSummaryService.getWorkCategoryReportData(
                    {
                        value1: $scope.selectedUser.ApplicationUserId,
                        value2: $scope.fromSearchDate,
                        value3: $scope.toSearchDate
                    }, onSuccessLoadWorkCategoryReportData, onFailedLoad);

                workSummaryService.getBranchSummaryReportData(
                    {
                        value1: $scope.selectedUser.ApplicationUserId,
                        value2: $scope.fromSearchDate,
                        value3: $scope.toSearchDate
                    }, onSuccessLoadBranchSummaryReportData, onFailedLoad);

                if ($scope.saveSearch) {

                        var searchObj = {};

                        searchObj.ApplicationUserId = $scope.selectedUser.ApplicationUserId;
                        searchObj.FromSearchDate = $scope.fromSearchDate;
                        searchObj.ToSearchDate = $scope.toSearchDate;

                        workSummaryService.setStoredSearchData({
                            value: $scope.settingCategoryName,
                            value1: JSON.stringify(searchObj)
                        }, onSuccessSetStoredSearchData, onFailedLoad);
                    }

                $scope.saveSearch = true;
            }

            function onSuccessLoadWorkCategoryReportData(data) {

                $scope.workCategoryReportGrandTotal = 0;
                var tempData = [];

                if(data.length > 0)
                {
                    $scope.workCategoryReportGrandTotal = data[data.length - 1].Total;
                }

                for (var i = 0; i < data.length; i++) {
                    
                    if (data[i].Name != "Total") {
                        data[i].Percentage = ((data[i].Total / $scope.workCategoryReportGrandTotal) * 100).toFixed(2) + "%";
                    }
                    else {
                        data[i].Percentage = "";
                    }

                    if (i == (data.length - 1)) {
                        data[i].Total = "";
                    }
                    tempData.push(data[i]);
                }

                $scope.workCategoryReportData = tempData;
                $scope.showWorkCategoryReport = true;
            }

            function onSuccessLoadBranchSummaryReportData(data) {                

                $scope.branchSummaryGrandTotal = 0;
                var tempData1 = [];

                if (data.length > 0) {
                    $scope.branchSummaryReportGrandTotal = data[data.length - 1].Total;
                }

                for (var i = 0; i < data.length; i++) {

                    if (data[i].Name != "Total") {
                        data[i].Percentage = ((data[i].Total / $scope.branchSummaryReportGrandTotal) * 100).toFixed(2) + "%";
                    }
                    else {
                        data[i].Percentage = "";
                    }

                    if (i == (data.length - 1)) {
                        data[i].Total = "";
                    }

                    tempData1.push(data[i]);
                }

                $scope.branchSummaryReportData = tempData1;
                $scope.showbranchSummaryReport = true;
            }

            function onSuccessSetStoredSearchData(data) {
            }

        }
    ]);
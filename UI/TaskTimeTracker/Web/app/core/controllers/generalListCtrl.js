'use strict';

angular.module('rootAppShell')
    .controller('generalListCtrl', [
        '$scope', '$injector', '$routeParams', '$location', 'fieldConfigurationService', 'userService', 'cachingService', 'uiGridConstants', '$interval', '$http', '$timeout',

function ($scope, $injector, $routeParams, $location, fieldConfigurationService, userService, cachingService, uiGridConstants, $interval, $http, $timeout) {

    $scope.entityName = toTitleCase($routeParams.entityName);
    $scope.PrimaryKey = '';
    var entityService = $injector.get($routeParams.entityName + 'Service');

    $scope.gridOptions = {
        data: 'entityItems',
        columnDefs: 'columns'
    };
    function toTitleCase(str) {
        return str.replace(/\b\w/g, function (txt) { return txt.toUpperCase(); });
    }
    var settingCategory = $scope.entityName + "AngularSearchControl";

    $scope.UniqueCategories = [];
    $scope.UniqueCategoriesChunked = [];
    $scope.errors = [];
    $scope.CanSave = true;
    $scope.SubmitMessage = 'Add New';
    $scope.Grouped = [];
    $scope.searchText = cachingService.get('' + $scope.entityName + 'Search');

    $scope.$watch('searchText', function (newVal, oldVal, scope) {
        cachingService.set('CalcualtionSearch', newVal);
    });

    // error function
    function onFailedLoad(serverResponse) {
        userService.AlertManager.logFailureAlert('', serverResponse.data, []);
    }

   
    // get search filter columns
    fieldConfigurationService.getSearchFilterColumns({ value: $scope.entityName, value1: settingCategory }, onSuccessLoadSearchFC, onFailedLoad);

    // on successfull load of search filter columns
    function onSuccessLoadSearchFC(data) {
        $scope.searchFieldConfigurations = data;
        $scope.searchData();
    }

    // data search function
    $scope.searchData = function () {

        var searchObj = {};

        for (var i = 0; i < $scope.searchFieldConfigurations.length; i++) {

            var filterName = $scope.searchFieldConfigurations[i].Name;
            var filterValue = $scope.searchFieldConfigurations[i].Value;

            searchObj[filterName] = filterValue;
        }

        var searchString = JSON.stringify(searchObj);

        // get entity records
        entityService.getList({ value: searchString, value1: settingCategory }, onSuccessLoad, onFailedLoad);

    };

    function onSuccessLoad(data) {
        $scope.entityItems = data;
    }

    // add new link function
    $scope.addNew = function () {
        $scope.SubmitMessage = 'Adding New ...';
        $location.url('/' + $scope.entityName + '/save/{New}');
    };

    $scope.reloadFC = function (myFcMode) {
        $scope.myFcMode = myFcMode;
        saveUserFieldConfiguration();
        getFieldConfigurations();
    };

    // get fc modes applicable to entity
    fieldConfigurationService.getFCModes({ value: $scope.entityName }, onSuccessLoadFCModes, onFailedLoad);

    function onSuccessLoadFCModes(data) {
        $scope.fcModes = data;

        // get user preferred fc mode
        fieldConfigurationService.getUserFieldConfigurationMode({ value: $scope.entityName }, onSuccessLoadUserFCMode, onFailedLoad);
    }

    function onSuccessLoadUserFCMode(data, getResponseHeaders) {
        $scope.myFcMode = data;

        if ($scope.myFcMode.FieldConfigurationModeId == -1) {
            $scope.myFcMode = $scope.fcModes[0];
            saveUserFieldConfiguration();
        }
        getFieldConfigurations();
    }

    function getFieldConfigurations() {

        // get user preferred columns based on user preferred fc mode
        fieldConfigurationService.getFieldConfigurations({ value: $scope.myFcMode.FieldConfigurationModeId, value1: $scope.entityName },
           onSuccessLoadColumns, onFailedLoad);
    }

    function onSuccessLoadColumns(response) {

        var fcColumns = [];
        var tmpKey = $scope.entityName + "Id";
        for (var i = 0; i < response.length; i++) {
            if (response[i].Name.toLowerCase() == tmpKey.toLowerCase()) {
                $scope.PrimaryKey = response[i].Name;
            }

            if (response[i].HorizontalAlignment != 'Right') {
                var fcItem1 = { field: response[i].Name, displayName: response[i].FieldConfigurationDisplayName };

                fcColumns.push(fcItem1);
            }
            else {

                // add right aligned columns
                var fcItem2 = {
                    field: response[i].Name
                    , displayName: response[i].FieldConfigurationDisplayName
                    , cellTemplate: '<div class="text-right" style="padding-right: 3px;">{{row.getProperty(col.field)}}</div>'
                };
                fcColumns.push(fcItem2);
            }
        }

        var fcItem = {
            field: $scope.PrimaryKey
                , displayName: 'Actions'
                , cellTemplate: ' <a href="#/' + $scope.entityName + '/save/{{row.getProperty(col.field)}}">Edit</a> <a href="#/' + $scope.entityName + '/detail/{{row.getProperty(col.field)}}">Detail</a>'
        };
        fcColumns.push(fcItem);

        $scope.columns = fcColumns;

    }

    function saveUserFieldConfiguration() {
        fieldConfigurationService.updateUserFieldConfigurationMode({ value: $scope.myFcMode.FieldConfigurationModeId, value1: $scope.entityName },
            function () { },
            function (serverResponse) {
                userService.AlertManager.logFailureAlert('', serverResponse.data, []);
            });
    }

}

    ]);
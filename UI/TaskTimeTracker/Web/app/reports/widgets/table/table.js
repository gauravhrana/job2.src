'use strict';

angular.module('reports.widgets.table', ['adf.provider'])
    .config([
        'dashboardProvider', function(dashboardProvider) {
            dashboardProvider
                .widget('table', {
                    title: 'Table',
                    description: 'Table widget',
                    controller: 'reportsWidgetsTableCtrl',
                    templateUrl: 'app/reports/widgets/table/table.html',
                    config: {
                        widgetInfo: {
                            WidgetCode: 'table',
                            Configuration: {
                                DashboardDatasetCode: undefined,
                                TableHeight: 300,
                                TableColumns: []
                            }
                        }
                    },
                    edit: {
                        templateUrl: 'app/reports/widgets/table/edit.html',
                        reload: true
                    }
                });
        }
    ]).controller('reportsWidgetsTableCtrl', [
        '$scope', 'config', 'dashboardState', 'displayFormatService', 'userService', function ($scope, config, dashboardState, displayFormatService,userService) {
            $scope.config = config;
            config.dashboard = dashboardState.getState();

            //load displayFormat data
            config.DisplayFormats = [];
            config.DisplayFormatsByCode = {};
            config.DisplayFormats = displayFormatService.query(null,
                function () {
                    config.DisplayFormats.forEach(function (format) {
                        config.DisplayFormatsByCode[format.CalculationCode] = format;
                    });
                },
                function (errorResponse) {
                    userService.AlertManager.addFailureResponse(errorResponse);
                });


            config.dataset = {};
            if (config.widgetInfo.Configuration.DashboardDatasetCode != undefined) {
                config.dataset = config.dashboard.DatasetsByCode[config.widgetInfo.Configuration.DashboardDatasetCode];
            }

            config.tableClass = ["table", "table table-condensed", "table-striped"];

            config.widgetInfo.Configuration.TableColumns.sort(function(a, b) {
                return a.DisplayOrder - b.DisplayOrder;
            });

            config.deleteColumn = function(column) {
                var index = config.widgetInfo.Configuration.TableColumns.indexOf(column);
                if (index >= 0) {
                    config.widgetInfo.Configuration.TableColumns.splice(index, 1);
                }
            };


            config.addColumn = function() {
                config.widgetInfo.Configuration.TableColumns.push({
                    Field: '',
                    DisplayName: '',
                    Width: 120,
                    Sortable: true,
                    Resizable: true,
                    DisplayOrder: config.widgetInfo.Configuration.TableColumns.length + 1
                });
            };

            config.getFormatType = function(column) {
                var formatType = 'none';
                if (column.JavascriptFormatString) {
                    if (column.JavascriptFormatString.indexOf('number:') == 0) {
                        formatType = 'number';
                    }
                    else if (column.JavascriptFormatString.indexOf('numeraljs:') == 0) {
                        formatType = 'numeraljs';
                    }
                    else if (column.JavascriptFormatString.indexOf('date:') == 0) {
                        formatType = 'date';
                    }                    
                }
                return formatType;
            }

            config.getFormatFilter = function(column) {
                var formatFilter = '';
                if (column.JavascriptFormatString) {
                    if (column.JavascriptFormatString.indexOf('number:') == 0) {
                        formatFilter = column.JavascriptFormatString.substr(7);
                    }
                    else if (column.JavascriptFormatString.indexOf('numeraljs:') == 0) {
                        formatFilter = column.JavascriptFormatString.substr(10).replace(/'/g, '');
                    }
                    else if (column.JavascriptFormatString.indexOf('date:') == 0) {
                        formatFilter = column.JavascriptFormatString.substr(5).replace(/'/g, '');
                    }
                }
                return formatFilter;
            }

            $scope.$watch('config.widgetInfo.Configuration.DashboardDatasetCode', function(newValue, oldValue) {
                if (newValue == undefined)return;
                if (newValue != oldValue) {
                    var dataset = $scope.config.dashboard.DatasetsByCode[newValue];
                    var i;
                    config.widgetInfo.Configuration.TableColumns = [];
                    for (i = 0; i < dataset.ExplorerQuery.Fields.length; i++) {
                        config.widgetInfo.Configuration.TableColumns.push({
                            Field: dataset.ExplorerQuery.Fields[i].Path,
                            DisplayName: dataset.ExplorerQuery.Fields[i].Path,
                            DisplayOrder: i
                        });
                    }
                }
            });
        }
    ]);


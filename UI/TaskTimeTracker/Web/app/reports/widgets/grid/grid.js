'use strict';

angular.module('reports.widgets.grid', ['adf.provider'])
    .config([
        'dashboardProvider', function (dashboardProvider) {
            dashboardProvider
                .widget('grid', {
                    title: 'Grid',
                    description: 'Grid widget',
                    controller: 'reportsWidgetsGridCtrl',
                    templateUrl: 'app/reports/widgets/grid/grid.html',
                    config: {
                        widgetInfo: {
                            WidgetCode: 'grid',
                            Configuration: {
                                DashboardDatasetCode: undefined,
                                GridHeight: 300,
                                GridColumns: []
                            }
                        }
                    },
                    edit: {
                        templateUrl: 'app/reports/widgets/grid/edit.html',
                        reload: true
                    }
                });
        }
    ]).controller('reportsWidgetsGridCtrl', [
        '$scope', 'config', 'dashboardState', 'displayFormatService','userService', function ($scope, config, dashboardState, displayFormatService,userService) {
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

            var gridOptions = {
                data: 'config.dataset.Results',
                columnDefs: [],
                enableColumnResize: true,
                enableColumnReordering: true,
            };

            config.gridStyle = { "height": config.widgetInfo.Configuration.GridHeight + 'px' };

            config.widgetInfo.Configuration.GridColumns.sort(function (a, b) {
                return a.DisplayOrder - b.DisplayOrder;
            });


            config.widgetInfo.Configuration.GridColumns.forEach(function (gridColumn) {
                var columnDef =
                {
                    field: gridColumn.PropertyName,
                    displayName: gridColumn.DisplayName,
                    width: gridColumn.Width,
                    sortable: gridColumn.Sortable,
                    resizable: gridColumn.Resizable
                };

                if (gridColumn.JavascriptFormatString && gridColumn.JavascriptFormatString != '') {
                    columnDef.cellFilter = gridColumn.JavascriptFormatString;
                    if (gridColumn.JavascriptFormatString.indexOf("number") == 0
                        || gridColumn.JavascriptFormatString.indexOf("numeraljs") == 0) {
                        columnDef.cellClass = 'text-right';
                    }
                }

                gridOptions.columnDefs.push(columnDef);
            });

            config.gridOptions = gridOptions;

            $scope.config.deleteColumn = function (column) {
                var index = config.widgetInfo.Configuration.GridColumns.indexOf(column);
                if (index >= 0) {
                    config.widgetInfo.Configuration.GridColumns.splice(index, 1);
                }
            };

            $scope.config.addColumn = function () {
                config.widgetInfo.Configuration.GridColumns.push({
                    Field: '',
                    DisplayName: '',
                    Width: 120,
                    Sortable: true,
                    Resizable: true,
                    DisplayOrder: config.widgetInfo.Configuration.GridColumns.length + 1
                });
            };

            $scope.$watch('config.widgetInfo.Configuration.DashboardDatasetCode', function (newValue, oldValue) {
                if (newValue == undefined) return;
                if (newValue != oldValue) {
                    var dataset = $scope.config.dashboard.DatasetsByCode[newValue];
                    var i;
                    config.widgetInfo.Configuration.GridColumns = [];
                    for (i = 0; i < dataset.ExplorerQuery.Fields.length; i++) {
                        config.widgetInfo.Configuration.GridColumns.push({
                            Field: dataset.ExplorerQuery.Fields[i].PropertyName,
                            DisplayName: dataset.ExplorerQuery.Fields[i].Path,
                            Width: 120,
                            Sortable: true,
                            Resizable: true,
                            DisplayOrder: i
                        });
                    }
                }
            });
        }
    ]);

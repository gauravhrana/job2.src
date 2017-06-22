'use strict';

angular.module('reports.widgets.barchart', ['adf.provider', 'highcharts-ng'])
    .config([
        'dashboardProvider', function(dashboardProvider) {
            dashboardProvider
                .widget('barchart', {
                    title: 'Bar Chart',
                    description: 'Bar chart widget',
                    controller: 'reportsWidgetsBarchartCtrl',
                    templateUrl: 'app/reports/widgets/barchart/barchart.html',
                    config: {
                        widgetInfo: {
                            WidgetCode: 'barchart',
                            Configuration: {
                                ChartHeight: 300,
                                SeriesList: []
                            }
                        }
                    },
                    edit: {
                        templateUrl: 'app/reports/widgets/barchart/edit.html',
                        reload: true
                    }
                });
        }
    ])
    .controller('reportsWidgetsBarchartCtrl', [
        '$scope', 'config', 'dashboardState','userService', function($scope, config, dashboardState,userService) {
            $scope.config = config;
            config.dashboard = dashboardState.getState();
            config.chartOptions = {};        

            function syncChartOptions() {
                var chartOptions = {
                    options: {
                        chart: {
                            type: 'bar',
                            height: config.widgetInfo.Configuration.ChartHeight,
                        }
                    },
                    series: []
                };

                config.widgetInfo.Configuration.SeriesList.forEach(function(series) {
                    var seriesOptions = {
                        name: series.Name,
                        data: []
                    }
                    var dataset = config.dashboard.DatasetsByCode[series.DashboardDatasetCode];
                    if (dataset && dataset.Results) {
                        dataset.Results.$promise.then(function(results) {

                            results.forEach(function(result) {
                                seriesOptions.data.push(result[series.ValueField]);
                            });
                        });
                    }
                    chartOptions.series.push(seriesOptions);
                });

                config.chartOptions = chartOptions;
            }

            syncChartOptions();

            $scope.config.deleteSeries = function(series) {
                var index = config.widgetInfo.Configuration.SeriesList.indexOf(series);
                if (index >= 0) {
                    config.widgetInfo.Configuration.SeriesList.splice(index, 1);
                }
            };

            $scope.config.addSeries = function() {
                config.widgetInfo.Configuration.SeriesList.push({
                    Name: '',
                    DashboardDatasetCode: '',
                    ValueField: ''
                });
            };
        }
    ]);

'use strict';





angular.module('rootAppShell')
    .controller('reportDashboardCtrl', [
        '$scope', '$routeParams', '$modal', '$filter', 'dashboard', 'dashboardService', 'dashboardState', 'userService', function ($scope, $routeParams, $modal, $filter, dashboard, dashboardService, dashboardState, userService) {
            $scope.supportsDate = Modernizr.inputtypes.date;
            //create an empty dashboard model
            $scope.dashboard = {
                DashboardCode: '',
                DashboardLayoutCode: '6-6',
                Description: '',
                Parameters: [],
                ParametersByCode: {},
                Datasets: [],
                Widgets: []
            };
            dashboardState.setState($scope.dashboard);

            $scope.isParamsCollapsed = false;
            $scope.showDashboard = false;
            $scope.model = {};

            function loadDatasetResults(dataset, forDropdown) {
                var request = {
                    DashboardCode: $scope.dashboard.DashboardCode,
                    DashboardDatasetCode: dataset.DashboardDatasetCode,
                    Parameters: []
                };


                //wire up the params
                $scope.dashboard.Parameters.forEach(function (parameter) {
                    if (parameter.DataTypeCode == 'DateTime' && parameter.Value != undefined) {
                        parameter.Value = $filter('date')(parameter.Value, 'MM/dd/yyyy');
                    }
                    request.Parameters.push({
                        Name: parameter.DashboardParameterCode,
                        Value: parameter.Value
                    });
                });

                if (forDropdown) {

                    dataset.DropdownResults = dashboardService.getDatasetResults(request, function () {
                    }, function (errorResponse) {
                        userService.AlertManager.addFailureResponse(errorResponse);
                    });

                } else {

                    dataset.Results = dashboardService.getDatasetResults(request, function () {
                    }, function (errorResponse) {
                        userService.AlertManager.addFailureResponse(errorResponse);
                    });

                }
            };

            //query server for dashboard info
            $scope.dashboard = dashboardService.getByCode({ dashboardCode: $routeParams.dashboardCode },
                function () {
                    dashboardState.setState($scope.dashboard);

                    //index the datasets
                    $scope.dashboard.DatasetsByCode = {};
                    $scope.dashboard.Datasets.forEach(function (dataset) {
                        //append an empty results
                        dataset.Results = [];
                        $scope.dashboard.DatasetsByCode[dataset.DashboardDatasetCode] = dataset;
                        dataset.IsAvailableValuesDataset = false;
                    });


                    //index the parameters
                    $scope.ParameterRows = [];
                    var currentParameterRow = [];
                    $scope.ParameterRows.push(currentParameterRow);
                    $scope.dashboard.ParametersByCode = {};
                    $scope.dashboard.Parameters.sort(function (a, b) {
                        return a.DisplayOrder - b.DisplayOrder;
                    });


                    $scope.dashboard.Parameters.forEach(function (parameter) {
                        //mark datasets for use as AvailableValuesDatsets
                        if (parameter.AvailableValuesDatasetCode != undefined) {
                            try {
                                $scope.dashboard.DatasetsByCode[parameter.AvailableValuesDatasetCode].IsAvailableValuesDataset = true;
                            } catch (e) {
                            }
                        }

                        $scope.dashboard.ParametersByCode[parameter.DashboardParameterCode] = parameter;
                        //put the parameters in rows for display
                        if (currentParameterRow.length == 2) {
                            currentParameterRow = [];
                            $scope.ParameterRows.push(currentParameterRow);
                        }
                        currentParameterRow.push(parameter);
                    });

                    //now that parameters and datasets are indexed we can bind them together where needed
                    $scope.dashboard.Parameters.forEach(function (parameter) {
                        if (parameter.AvailableValuesDatasetCode != undefined) {
                            parameter.AvailableValuesDataset = $scope.dashboard.DatasetsByCode[parameter.AvailableValuesDatasetCode];
                        }
                    });

                    //now that all params and datasets are bound we can kickoff the dataset loading for parameter dropdowns
                    $scope.dashboard.Datasets.forEach(function (dataset) {
                        if (dataset.IsAvailableValuesDataset) {
                            loadDatasetResults(dataset, true);
                        }
                    });
                }
                , function (errorResponse) {
                    userService.AlertManager.addFailureResponse(errorResponse);
                }
            );

            $scope.LoadAllData = function () {

                $scope.showDashboard = false;
                //query the data for all datasets
                $scope.dashboard.Datasets.forEach(function (dataset) {
                    loadDatasetResults(dataset, false);
                });

                //hide the params
                //$scope.isParamsCollapsed = true;

                //create the dashboard model
                var structure = dashboard.structures[$scope.dashboard.DashboardLayoutCode];
                var model = {
                    title: '',
                    structure: $scope.dashboard.DashboardLayoutCode,
                    rows: angular.copy(structure.rows)
                };

                //setup the columns for the model
                model.rows.forEach(function (row) {
                    row.columns.forEach(function (col) {
                        col.widgets = [];
                    });
                });

                //setup the widgets
                $scope.dashboard.Widgets.forEach(function (widgetInfo) {
                    //if widget is in a row that doesn't exist, put it at the end
                    if (widgetInfo.Row >= model.rows.length) {
                        widgetInfo.Row = model.rows.length - 1;
                    }
                    var row = model.rows[widgetInfo.Row];

                    //if widget is in a column that doesn't exist, put it at the end
                    if (widgetInfo.Column >= row.columns.length) {
                        widgetInfo.Column = row.columns.length - 1;
                    }
                    var column = row.columns[widgetInfo.Column];


                    while (column.widgets.length <= widgetInfo.Index) {
                        column.widgets.push(undefined);
                    };

                    //create a widget
                    var widget =
                    {
                        title: widgetInfo.Title,
                        type: widgetInfo.WidgetCode,
                        //attach our details to the widget
                        config: {
                            widgetInfo: widgetInfo,
                            parent: $scope
                        }
                    };

                    //put the widget into the model
                    //if the space for this index is free put it where it belongs
                    if (column.widgets[widgetInfo.Index] == undefined) {
                        column.widgets[widgetInfo.Index] = widget;
                    } else {
                        //otherwise, put it at the end 
                        column.widgets.push(widget);
                    }
                });
                //set the dashboard 
                $scope.dashboardModel = model;

                //Make dates chrome-friendly
                $scope.dashboard.Parameters.forEach(function (parameter) {
                    if (parameter.DataTypeCode == 'DateTime' && parameter.Value != undefined) {
                        var date = new Date(parameter.Value);
                        parameter.Value = date.chromeDate();
                    }
                });

                //make it visible
                $scope.showDashboard = true;
            };

            //save widget changes
            $scope.$on('widgetConfigChanged', function () {
                console.log("here");
            });

            //save dashboard changes
            $scope.$on('adfDashboardChanged', function (event, name, model) {
                var dashboardInfo = {
                    DashboardCode: $scope.dashboard.DashboardCode,
                    DashboardLayoutCode: model.structure,
                    Widgets: []
                };

                var r, c, w;
                for (r = 0; r < model.rows.length; r++) {
                    var row = model.rows[r];
                    for (c = 0; c < row.columns.length; c++) {
                        var column = row.columns[c];
                        for (w = 0; w < column.widgets.length; w++) {
                            var widget = column.widgets[w];
                            if (widget.config != undefined && widget.config.widgetInfo != undefined) {
                                var widgetInfo = widget.config.widgetInfo;
                                widgetInfo.Title = widget.title;
                                widgetInfo.Row = r;
                                widgetInfo.Column = c;
                                widgetInfo.Index = w;

                                dashboardInfo.Widgets.push(widgetInfo);
                            }
                        }
                    }
                }
                dashboardService.saveDashboard(dashboardInfo, function () { }, function (errorResponse) {
                    userService.AlertManager.addFailureResponse(errorResponse);
                });
            });
        }
    ]);

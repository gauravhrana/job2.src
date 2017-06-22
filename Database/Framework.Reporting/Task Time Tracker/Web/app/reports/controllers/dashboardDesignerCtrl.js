'use strict';

angular.module('rootAppShell')
    .controller('dashboardDesignerCtrl', [
        '$scope', '$routeParams', '$modal', 'dashboard', 'dashboardService', 'dashboardState', 'userService', '$location', 'explorerService', function($scope, $routeParams, $modal, dashboard, dashboardService, dashboardState, userService, $location, explorerService) {

            function copyWidgets(source, target) {
                if (source.widgets && source.widgets.length > 0) {
                    var w = source.widgets.shift();
                    while (w) {
                        target.widgets.push(w);
                        w = source.widgets.shift();
                    }
                }
            }

            function changeStructure(model, structure) {
                var columns = readColumns(model);
                model.rows = structure.rows;
                var counter = 0;
                while (counter < columns.length) {
                    counter = fillStructure(model, columns, counter);
                }
            }

            function readColumns(model) {
                var columns = [];
                angular.forEach(model.rows, function(row) {
                    angular.forEach(row.columns, function(col) {
                        columns.push(col);
                    });
                });
                return columns;
            }


            function fillStructure(model, columns, counter) {
                angular.forEach(model.rows, function(row) {
                    angular.forEach(row.columns, function(column) {
                        if (!column.widgets) {
                            column.widgets = [];
                        }
                        if (columns[counter]) {
                            copyWidgets(columns[counter], column);
                            counter++;
                        }
                    });
                });
                return counter;
            }

            $scope.structures = dashboard.structures;

            $scope.dashboard = {
                OriginalDashboardCode: $routeParams.dashboardCode,
                DashboardCode: '',
                Description: '',
                DashboardLayoutCode: '6-6',
                Parameters: [],
                Datasets: [],
                Widgets: [],
            };

            dashboardState.setState($scope.dashboard);

            $scope.dashboardModel = {
                title: '',
                structure: $scope.dashboard.DashboardLayoutCode,
                rows: angular.copy(dashboard.structures[$scope.dashboard.DashboardLayoutCode].rows),
                editMode: true
            };

            $scope.$watch('dashboard.DashboardLayoutCode', function(newValue, oldValue) {
                if (newValue == undefined || oldValue == undefined) return;
                if (newValue != oldValue) {
                    changeStructure($scope.dashboardModel, dashboard.structures[newValue]);
                }
            });

            if ($routeParams.dashboardCode != '{New}') {
                //query server for dashboard model
                $scope.dashboard = dashboardService.getByCode({ dashboardCode: $routeParams.dashboardCode },
                    function() {
                        dashboardState.setState($scope.dashboard);
                        $scope.dashboard.OriginalDashboardCode = $routeParams.dashboardCode;
                        $scope.setupDashboardIndexes();
                        $scope.loadModel();

                    }, function(errorResponse) {
                        userService.AlertManager.addFailureResponse(errorResponse);
                    });
            }

            $scope.setupDashboardIndexes = function() {
                //index the parameters
                $scope.dashboard.ParametersByCode = {};
                $scope.dashboard.Parameters.forEach(function(parameter) {
                    $scope.dashboard.ParametersByCode[parameter.DashboardParameterCode] = parameter;
                });

                //index the datasets
                $scope.dashboard.DatasetsByCode = {};
                $scope.dashboard.Datasets.forEach(function(dataset) {
                    $scope.dashboard.DatasetsByCode[dataset.DashboardDatasetCode] = dataset;
                });
            }

            $scope.loadModel = function() {

                $scope.dashboardModel.structure = $scope.dashboard.DashboardLayoutCode;
                $scope.dashboardModel.rows = angular.copy(dashboard.structures[$scope.dashboard.DashboardLayoutCode].rows);

                //setup the columns for the model
                $scope.dashboardModel.rows.forEach(function(row) {
                    row.columns.forEach(function(col) {
                        col.widgets = [];
                    });
                });

                //setup the widgets
                $scope.dashboard.Widgets.forEach(function(widgetInfo) {
                    //if widget is in a row that doesn't exist, put it at the end
                    if (widgetInfo.Row >= $scope.dashboardModel.rows.length) {
                        widgetInfo.Row = $scope.dashboardModel.rows.length - 1;
                    }
                    var row = $scope.dashboardModel.rows[widgetInfo.Row];

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
                            widgetInfo: widgetInfo
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
            }

            $scope.save = function() {
                $scope.dashboard.Widgets = [];
                var r, c, w;
                for (r = 0; r < $scope.dashboardModel.rows.length; r++) {
                    var row = $scope.dashboardModel.rows[r];
                    for (c = 0; c < row.columns.length; c++) {
                        var column = row.columns[c];
                        if (column.widgets) {
                            for (w = 0; w < column.widgets.length; w++) {
                                var widget = column.widgets[w];
                                if (widget.config != undefined && widget.config.widgetInfo != undefined) {
                                    var widgetInfo = widget.config.widgetInfo;
                                    widgetInfo.Title = widget.title;
                                    widgetInfo.Row = r;
                                    widgetInfo.Column = c;
                                    widgetInfo.Index = w;

                                    $scope.dashboard.Widgets.push(widgetInfo);
                                }
                            }
                        }
                    }
                }

                dashboardService.saveDashboard($scope.dashboard, function() {
                    userService.AlertManager.addSuccessAlert($scope.dashboard.DashboardCode + ' saved successfully.');
                    $location.url('/dashboards/');
                }, function(errorResponse) {
                    userService.AlertManager.addFailureResponse(errorResponse);
                });
            };

            $scope.delete = function() {
                dashboardService.deleteDashboard({ dashboardCode: $scope.dashboard.DashboardCode },
                    function() {
                        userService.AlertManager.addSuccessAlert($scope.dashboard.DashboardCode + ' deleted successfully.');
                        $location.url('/dashboards/');
                    }, function(errorResponse) {
                        userService.AlertManager.addFailureResponse(errorResponse);
                    });
            };

            $scope.accordionStatus = {
                parametersIsOpen: true,
                datasetsIsOpen: true,
                widgetsIsOpen: true
            };


            $scope.showDashboardAssistant = function() {
                var modalInstance = $modal.open({
                    size: 'lg',
                    templateUrl: 'app/reports/views/dashboard-assistant.html',
                    controller: 'dashboardAssistantCtrl',
                    resolve: {
                        dashboard: function() {
                            return $scope.dashboard;
                        }
                    }
                });

                modalInstance.result.then(function(parameter) {
                    userService.AlertManager.addSuccessAlert('assistant done');
                });
            };

            $scope.addParameter = function() {
                var modalInstance = $modal.open({
                    size: 'lg',
                    templateUrl: 'app/reports/views/add-edit-parameter.html',
                    controller: 'addEditParameterCtrl',
                    resolve: {
                        modalInfo: function() {
                            return { Type: 'Add' };
                        },
                        parameter: function() {
                            return {
                                DashboardParameterCode: '',
                                DashboardCode: $scope.dashboard.DashboardCode,
                                DataTypeCode: 'String',
                                Prompt: '',
                                Multiple: false,
                                Required: true,
                                DisplayOrder: 0,
                                AvailableValuesDatasetCode: undefined,
                                AvailableValuesDatasetValueField: undefined,
                                AvailableValuesDatasetLabelField: undefined,
                            };
                        },
                        datasets: function() {
                            return $scope.dashboard.Datasets;
                        }
                    }
                });

                modalInstance.result.then(function(parameter) {
                    $scope.dashboard.Parameters.push(parameter);
                    $scope.setupDashboardIndexes();
                });
            };

            $scope.editParameter = function(editParameter) {
                var modalInstance = $modal.open({
                    size: 'lg',
                    templateUrl: 'app/reports/views/add-edit-parameter.html',
                    controller: 'addEditParameterCtrl',
                    resolve: {
                        modalInfo: function() {
                            return { Type: 'Edit' }
                        },
                        parameter: function() {
                            return angular.copy(editParameter);
                        },
                        datasets: function() {
                            return $scope.dashboard.Datasets;
                        }
                    }
                });

                modalInstance.result.then(function(parameter) {
                    angular.copy(parameter, editParameter);
                    $scope.setupDashboardIndexes();
                }, function(message) {
                    if (message == 'delete') {
                        var index = $scope.dashboard.Parameters.indexOf(editParameter);
                        if (index >= 0) {
                            $scope.dashboard.Parameters.splice(index, 1);
                            $scope.setupDashboardIndexes();
                        }
                    }
                });
            };

            $scope.addDataset = function() {
                var modalInstance = $modal.open({
                    size: 'lg',
                    templateUrl: 'app/reports/views/add-edit-dataset.html',
                    controller: 'addEditDatasetCtrl',
                    resolve: {
                        modalInfo: function() {
                            return { Type: 'Add' };
                        },
                        dataset: function() {
                            return {
                                DashboardDatasetCode: '',
                                DashboardCode: $scope.dashboard.DashboardCode,
                                ExplorerQueryId: undefined,
                                ExplorerQuery: {}
                            };
                        }
                    }
                });

                modalInstance.result.then(function(modalScope) {
                    var query = {
                        EntityCode: modalScope.explorer.SelectedEntity.EntityCode,
                        Fields: []
                    };

                    modalScope.explorer.selectedPaths.forEach(function(path) {
                        query.Fields.push({ Path: path });
                    });

                    query = explorerService.saveQuery(query,
                        function() {
                            modalScope.dataset.ExplorerQuery = query;
                            modalScope.dataset.ExplorerQueryId = query.ExplorerQueryId;
                            $scope.dashboard.Datasets.push(modalScope.dataset);
                            $scope.setupDashboardIndexes();
                        },
                        function(errorResponse) {
                            userService.AlertManager.addFailureResponse(errorResponse);
                        });
                });
            };

            $scope.editDataset = function(editDataset) {
                var modalInstance = $modal.open({
                    size: 'lg',
                    templateUrl: 'app/reports/views/add-edit-dataset.html',
                    controller: 'addEditDatasetCtrl',
                    resolve: {
                        modalInfo: function() {
                            return { Type: 'Edit' };
                        },
                        dataset: function() {
                            return angular.copy(editDataset);
                        }
                    }
                });

                modalInstance.result.then(function(modalScope) {
                    var query = {
                        EntityCode: modalScope.explorer.SelectedEntity.EntityCode,
                        Fields: []
                    };

                    modalScope.explorer.selectedPaths.forEach(function(path) {
                        query.Fields.push({ Path: path });
                    });
                    query = explorerService.saveQuery(query, function() {

                        modalScope.dataset.ExplorerQueryId = query.ExplorerQueryId;
                        modalScope.dataset.ExplorerQuery = query;
                        angular.copy(modalScope.dataset, editDataset);
                        //refreshDatasetFields(editDataset);
                        $scope.setupDashboardIndexes();
                    });
                }, function(message) {
                    if (message == 'delete') {
                        var index = $scope.dashboard.Datasets.indexOf(editDataset);
                        if (index >= 0) {
                            $scope.dashboard.Datasets.splice(index, 1);
                            $scope.setupDashboardIndexes();
                        }
                    }
                });
            };

            function addWidget(widgetCode) {
                var w = {
                    type: widgetCode,
                    config: angular.copy(dashboard.widgets[widgetCode].config)
                };
                $scope.dashboardModel.rows[0].columns[0].widgets.unshift(w);
            }

            $scope.addGridWidget = function() {
                addWidget('grid');
            };

            $scope.addTableWidget = function() {
                addWidget('table');
            };

            $scope.addBarChartWidget = function() {
                addWidget('barchart');
            };

            $scope.addLineChartWidget = function() {
                addWidget('lineChart');
            };
        }
    ]);
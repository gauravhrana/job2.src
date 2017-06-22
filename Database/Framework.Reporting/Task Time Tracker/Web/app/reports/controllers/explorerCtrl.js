'use strict';

angular.module('rootAppShell')
    .controller('explorerCtrl', [
        '$scope', 'explorerService', 'userService', '$timeout',
        function ($scope, explorerService, userService, $timeout) {
            $scope.SelectedEntity = {};
            $scope.SelectedEntityId = '';
            $scope.EntityTypes = {};
            $scope.Entities = {};
            $scope.tree = [];
            $scope.selectedPaths = ['DtdBeginAccruedInterest', 'Investment.InvestmentCode'];

            $scope.Data = explorerService.getEntityList(null, function() {
                    $scope.EntityTypes = {};
                    $scope.Entities = {};
                    $scope.Data.forEach(function(entity) {
                        //create a category lookup
                        var entityType = $scope.EntityTypes[entity.EntityTypeCode];
                        if (entityType == undefined) {
                            entityType = { Entities: [] };
                            $scope.EntityTypes[entity.EntityTypeCode] = entityType;
                        }
                        entityType.Entities.push(entity);
                        $scope.Entities[entity.EntityCode] = entity;
                    });
                },
                function(errorResponse) {
                    userService.AlertManager.addFailureResponse(errorResponse);
                });

            $scope.entityChanged = function () {
                $scope.SelectedEntity = $scope.Entities[$scope.SelectedEntityId];
                $scope.tree = [];
                $scope.selectedPaths = [];
                $scope.Entities[$scope.SelectedEntityId].Fields.forEach(function(field) {
                    var node = {
                        parent: undefined,
                        field: field,
                        expanded: false,
                        nodes: [],
                        selected: false
                    };
                    var path = getPath(node);
                    if ($scope.selectedPaths.indexOf(path) >= 0) {
                        node.selected = true;
                    }

                    $scope.tree.push(node);
                });
            };

            $scope.collapse = function(node) {
                node.expanded = false;
            };

            $scope.expand = function(node) {
                if (node.nodes.length == 0) {
                    var entity = $scope.Entities[node.field.ReferenceEntityCode];
                    if (entity != undefined) {
                        entity.Fields.forEach(function(field) {
                            var child = {
                                parent: node,
                                field: field,
                                expanded: false,
                                nodes: [],
                                selected: false
                            };
                            var path = getPath(child);
                            if ($scope.selectedPaths.indexOf(path) >= 0) {
                                child.selected = true;
                            }
                            node.nodes.push(child);
                        });
                    }
                }
                node.expanded = true;
            };

            function getPath(node, path) {
                var newPath;

                var suffix = '';
                if (path != undefined) {
                    suffix = '.' + path;
                }

                if (node.field.ReferenceEntityCode != undefined) {
                    newPath = node.field.ReferenceName + suffix;
                } else {
                    newPath = node.field.FieldCode + suffix;
                }

                //recurse
                if (node.parent == undefined) {
                    return newPath;
                }

                return getPath(node.parent, newPath);
            }

            $scope.selectNode = function(node) {
                var path = getPath(node);
                if (node.selected) {
                    $scope.selectedPaths.push(path);
                } else {
                    var index = $scope.selectedPaths.indexOf(path);
                    if (index >= 0) {
                        $scope.selectedPaths.slice(index, 1);
                    }
                }
            };

            $scope.save = function() {

                var query = {
                    EntityCode: $scope.SelectedEntity.EntityCode,
                    Fields: []
                };
                $scope.selectedPaths.forEach(function(path) {
                    query.Fields.push({ Path: path });
                });
                explorerService.saveQuery(query,
                    function() {
                        userService.AlertManager.addSuccessAlert("Query saved successfully.");
                    },
                    function(errorResponse) {
                        userService.AlertManager.addFailureResponse(errorResponse);
                    });
            };

            // used to prevent out of control loop
            $scope.SetSelectedEntityCounter = 1;
            
            $scope.SetSelectedEntity = function (event, args) {
                
                if (Object.keys($scope.Entities).length == 0 || $scope.Entities == undefined) {                    

                    $scope.SetSelectedEntityCounter++;
                    
                    if ($scope.SetSelectedEntityCounter < 100) {
                        //console.log('delay');
                        $timeout(function () { $scope.SetSelectedEntity(event, args); }, 500);
                    }
                    
                } else {
                    $scope.SetSelectedEntityCounter = 0;
                    $scope.SelectedEntityId = args.SelectedEntityId;
                    $scope.entityChanged();                    
                }
            };

            // Event 'Selected Entity Set' from another part of the application
            $scope.$on("SelectedEntitySetEvent", function (event, args) {
                $scope.SetSelectedEntity(event, args);
            });
        }
    ]);

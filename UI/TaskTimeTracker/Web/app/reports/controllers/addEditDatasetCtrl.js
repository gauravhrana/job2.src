'use strict';

angular.module('rootAppShell')
    .controller('addEditDatasetCtrl', [
        '$scope', '$modalInstance', 'modalInfo', 'dataset', 'explorerService', 'userService', function($scope, $modalInstance, modalInfo, dataset, explorerService, userService) {

            $scope.modalInfo = modalInfo;
            $scope.dataset = dataset;

            $scope.ok = function() {
                $modalInstance.close($scope);
            };

            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            };

            $scope.delete = function() {
                $modalInstance.dismiss('delete');
            };

            $scope.explorer = {};
            $scope.explorer.SelectedEntity = {};
            $scope.explorer.SelectedEntityId = '';
            $scope.explorer.EntityTypes = {};
            $scope.explorer.Entities = {};
            $scope.explorer.tree = [];
            $scope.explorer.selectedPaths = [];            
            $scope.query = {};

            //retrieve explorer data
            $scope.explorer.Data = explorerService.getEntityList(null, function() {
                $scope.explorer.EntityTypes = {};
                $scope.explorer.Entities = {};
                $scope.explorer.Data.forEach(function(entity) {
                    //create a category lookup
                    var entityType = $scope.explorer.EntityTypes[entity.EntityTypeCode];
                    if (entityType == undefined) {
                        entityType = { Entities: [] };
                        $scope.explorer.EntityTypes[entity.EntityTypeCode] = entityType;
                    }
                    entityType.Entities.push(entity);
                    $scope.explorer.Entities[entity.EntityCode] = entity;
                });


                if (dataset.ExplorerQueryId == undefined) {
                    buildTree(false);
                } else {
                    $scope.query = explorerService.getQuery({ explorerQueryId: dataset.ExplorerQueryId },
                        function() {
                            $scope.explorer.SelectedEntity = $scope.explorer.Entities[$scope.query.EntityCode];
                            $scope.explorer.SelectedEntityId = $scope.query.EntityCode;
                            $scope.explorer.selectedPaths = [];
                            $scope.query.Fields.forEach(function(field) {
                                $scope.explorer.selectedPaths.push(field.Path);
                            });                            
                            buildTree(false);
                        }, function(errorResponse) {
                            userService.AlertManager.addFailureResponse(errorResponse);
                            $modalInstance.dismiss('cancel');
                        });
                }
            }, function(errorResponse) {
                userService.AlertManager.addFailureResponse(errorResponse);
                $modalInstance.dismiss('cancel');
            });

            function buildTree(initialize) {

                if ($scope.explorer.SelectedEntityId != undefined && $scope.explorer.SelectedEntityId != '') {
                    $scope.explorer.SelectedEntity = $scope.explorer.Entities[$scope.explorer.SelectedEntityId];
                }

                if (initialize) {
                    $scope.explorer.tree = [];
                    $scope.explorer.selectedPaths = [];
                }

                if ($scope.explorer.SelectedEntityId != undefined && $scope.explorer.SelectedEntityId != '') {
                    $scope.explorer.Entities[$scope.explorer.SelectedEntityId].Fields.forEach(function(field) {
                        var node = {
                            parent: undefined,
                            field: field,
                            expanded: false,
                            nodes: [],
                            selected: false
                        };
                        var path = getPath(node);
                        if ($scope.explorer.selectedPaths.indexOf(path) >= 0) {
                            node.selected = true;
                        }
                        $scope.explorer.tree.push(node);
                    });
                }

            };

            $scope.explorer.selectNode = function(node) {
                var path = getPath(node);
                if (node.selected) {
                    $scope.explorer.selectedPaths.push(path);
                } else {
                    var index = $scope.explorer.selectedPaths.indexOf(path);
                    if (index >= 0) {
                        $scope.explorer.selectedPaths.splice(index, 1);
                    }
                }                
            };

            $scope.explorer.entityChanged = function() {
                buildTree(true);
            }

            $scope.explorer.collapse = function(node) {
                node.expanded = false;
            };

            $scope.explorer.expand = function(node) {
                if (node.nodes.length == 0) {
                    var entity = $scope.explorer.Entities[node.field.ReferenceEntityCode];
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
                            if ($scope.explorer.selectedPaths.indexOf(path) >= 0) {
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
        }
    ]);











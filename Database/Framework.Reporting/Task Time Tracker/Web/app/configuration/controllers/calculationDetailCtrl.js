'use strict';

angular.module('rootAppShell')
    .controller('calculationDetailCtrl', [
        '$timeout', '$location', '$rootScope', '$scope', '$routeParams', '$modal', '$q', 'calculationService', 'referenceDataService', 'userService', 'summaryService', 'utilityUIService', 'createDialogService', '$route',
        function ($timeout, $location, $rootScope, $scope, $routeParams, $modal, $q, calculationService, referenceDataService, userService, summaryService, utilityUIService, createDialogService, $route) {
            // *** 
            // Common Properties
            // *** 
            $scope.errors = [];
            $scope.CanSave = true; // todo: if valid 
            $scope.CanDelete = true;
            $scope.SubmitMessage = 'Save';
            $scope.DeleteMessage = 'Delete';

            $scope.calculation = {
                CalculationCode: '',
                Name: '',
                DataTypeCode: 'Decimal',
                CalculationTypeCode: 'FirstPass',
                Description: '',
                SortOrder: 1,
                Formula: 'return -999;',
                SummaryDependencies: [],
                Categories: [],                
                FormulaError: '',
                DataRepository: 'TaxLot',
            };

            $scope.isCollapsed = true;
            $scope.CalculationTypes = [];
            $scope.DataTypes = [];
            $scope.SummarySource = [];
            $scope.ExistingCategories = [];
            $scope.NewCategory = '';
            $scope.SelectedCategories = [];

            function bindData(data) {
                data.isCollapsed = true;
                $scope.calculation = data;
                $scope.calculation.DataRepository = 'TaxLot';                
                $rootScope.$broadcast("SelectedEntitySetEvent", { SelectedEntityId: $scope.calculation.DataRepository });
            }

            function loadReferenceData() {

                // fail handler

                function onFailedLoad(data) {
                    userService.AlertManager.addFailureResponse(data, JSON.stringify($scope.calculation));
                };

                // success handler CalculationTypes

                function onSuccessLoadCalculationType(data) {
                    $scope.CalculationTypes = data;
                    return data;
                };

                // success handler DataTypes

                function onSuccessLoadDataType(data) {
                    $scope.DataTypes = data;
                    return data;
                };

                // Category

                function onSuccessLoadCategory(data) {
                    angular.forEach(data, function(value, key) {
                        $scope.ExistingCategories.push({ name: value, ticked: false });
                    });

                    return data;
                };

                // success handler DependencyList

                function onSuccessLoadSummaryList(data) {
                    $scope.DependencyList = data;
                    return data;
                };

                // TODO: move up to  js app cache ... as certain lists will not change ...
                referenceDataService.getReferenceList({ referenceKey: 'CalculationType' }, onSuccessLoadCalculationType, onFailedLoad);
                referenceDataService.getReferenceList({ referenceKey: 'DataType' }, onSuccessLoadDataType, onFailedLoad);

                calculationService.getCategoryList(null, onSuccessLoadCategory, onFailedLoad);

                summaryService.query(null, onSuccessLoadSummaryList, onFailedLoad);
            }

            // Load

            function refreshData() {

                // fail handler

                function onFailedLoad(data) {
                    userService.AlertManager.addFailureResponse(data, JSON.stringify($scope.calculation));
                };

                // sucess handler

                function onSuccessLoad(data) {

                    bindData(data);

                    if ($scope.calculation.CalculationCode == null || $scope.calculation.CalculationCode.length == 0) {

                        userService.AlertManager.addFailureAlert('Calculation \'' + $routeParams.calculationCode + '\' does not exist, please specify details.');

                        $location.url('/calculations/{New}');
                    }

                    enableSaveButton();

                    var existingCategories = _.chain($scope.ExistingCategories).pluck('name').flatten().unique().value();

                    var upperCasedArray = $.map(existingCategories, function(item, index) {
                        return item.toUpperCase();
                    });

                    angular.forEach(data.Categories, function(value, key) {
                        var index = _.indexOf(upperCasedArray, value.toUpperCase());
                        $scope.ExistingCategories[index].ticked = true;
                    });

                    return data;
                };

                //console.log($routeParams.calculationCode);

                if (!$scope.WorkflowStateIsNew()) {
                    calculationService.getByCodeWithDependencyDetail(
                        { calculationCode: $routeParams.calculationCode }, onSuccessLoad, onFailedLoad
                    );
                } else {

                    // Add New screen
                    // if new entity don't allow delete
                    //if (($scope.summary.SummaryId === undefined)) {                        
                    //disableDeleteButton('Not Deleteable');                        
                    disableDeleteButton('Delete');
                    //}
                }
            }

            // Save
            $scope.save = function() {

                function onFailedSave(serverResponse) {

                    if (serverResponse.data.ExceptionType == 'Lux.Transcend.Calc.DynamicObjectFactory+LuxCompilationException') {

                        var message = '';
                        message += '\nMessage: "' + serverResponse.data.Message.trim() + '"\n';
                        //message += '\nException Type: "' + serverResponse.data.ExceptionType.trim() + '"\n';
                        message += '\nException Message: \n' + serverResponse.data.ExceptionMessage.trim() + '\n';

                        $scope.calculation.FormulaError = message;

                        userService.AlertManager.logFailureAlert('', serverResponse.data, JSON.stringify($scope.calculation));

                    } else {
                        //userService.AlertManager.addFailureAlert('Calculation \'' + $scope.calculation.CalculationCode + '\' failed to save.\n' + msg);
                        userService.AlertManager.addFailureResponse(serverResponse, JSON.stringify($scope.calculation));
                    }

                    resetActionButton();
                }

                function onSuccessfulSave(data) {

                    $scope.calculation.FormulaError = '';

                    var info = 'Calculation \'' + $scope.calculation.CalculationCode + '\' Saved.';

                    createDialogService({
                        id: 'simpleDialog',
                        template: info,
                        backdrop: false,
                        css: {
                            top: '100px',
                            margin: '0 auto'
                        }
                    });
                    
                    //userService.AlertManager.addSuccessAlert();

                    if ($scope.WorkflowStateIsNew()) {
                        $location.url('/calculations/' + $scope.calculation.CalculationCode);
                    } else {
                        //$route.reload();
                        refreshData();
                    }

                    //$scope.SubmitMessage = 'Saved';
                    resetActionButton();
                }

                disableSaveButton();
                disableDeleteButton();

                //var id = $scope.calculation.CalculationCode;                    
                //$scope.calculation.Dependencies = utilityUIService.getMatchingReferencesFromProperty($scope.DependencyList, $scope.calculation.Dependencies, 'SummaryCode');                    

                // exisiting item
                if (!$scope.WorkflowStateIsNew()) {
                    calculationService.update($scope.calculation, onSuccessfulSave, onFailedSave);
                }
                    // save new record
                else {
                    calculationService.create($scope.calculation, onSuccessfulSave, onFailedSave);
                }
            };

            $scope.WorkflowStateIsNew = function() {
                return ($routeParams.calculationCode == '{New}');
            };

            // Delete
            $scope.delete = function() {

                function onFailedDelete(data) {
                    //userService.AlertManager.addFailureAlert('Calculation \'' + $scope.calculation.CalculationCode + '\' failed to delete.' + JSON.stringify(data));
                    userService.AlertManager.addFailureResponse(data, JSON.stringify($scope.calculation));
                    //$scope.DeleteMessage = 'Delete failed ...';
                    resetActionButton();
                }

                // to resolve ...

                function onSuccessfulDelete(data) {
                    userService.AlertManager.addSuccessAlert('Calculation \'' + $scope.calculation.CalculationCode + '\' deleted.');
                    $location.url('/calculations/');
                    //resetActionButton();
                }

                disableSaveButton();
                disableDeleteButton();

                var id = $scope.calculation.CalculationCode;
                calculationService.delete({ calculationCode: id }, onSuccessfulDelete, onFailedDelete);
            };

            // *****
            // Visual Cues -- maybe absctracted to common encapulsated object
            // *****

            // Reset logic 

            function resetActionButton() {
                // want to make it visual transition ...
                // alternative for now
                $timeout(function() {
                    $scope.$apply(function() {
                        enableDeleteButton();
                        enableSaveButton();
                        // should we send back to proper URL ? 
                        //$location.url('/main');                    
                    });
                }, 100);
            }

            function disableSaveButton(message) {

                if (typeof message !== 'string') {
                    message = '... ..';
                }

                $scope.SubmitMessage = message;
                $scope.CanSave = false;
            }

            function enableSaveButton(message) {
                if (typeof message !== 'string') {
                    message = 'Save';
                }
                $scope.SubmitMessage = message;
                $scope.CanSave = true;
            }

            function disableDeleteButton(message) {

                if (typeof message !== 'string') {
                    message = '... ..';
                }

                $scope.DeleteMessage = message;
                $scope.CanDelete = false;
            }

            function enableDeleteButton(message) {
                if (typeof message !== 'string') {
                    message = 'Delete';
                }
                $scope.DeleteMessage = message;
                $scope.CanDelete = true;
            }

            $scope.CategorySelectionClosed = function(data) {

                var selectedList = $.map($scope.SelectedCategories, function(item, index) {
                    return item.name;
                });

                $scope.calculation.Categories = selectedList;
            };

            $scope.AddNewCategory = function(event) {

                //if (event.which != 13) return;

                var val = $scope.NewCategory.trim();

                if (val == '') return;

                var existingCategories = _.chain($scope.ExistingCategories).pluck('name').flatten().unique().value();

                var upperCasedArray = $.map(existingCategories, function(item, index) {
                    return item.toUpperCase();
                });

                // find if exists already in possible list
                var index = _.indexOf(upperCasedArray, val.toUpperCase());

                if (index >= 0) {

                    val = $scope.ExistingCategories[index];
                    val.ticked = true;

                    // find if exists already in selected list
                    var upperSelectedArray = $.map($scope.calculation.Categories, function(item, index) {
                        return item.toUpperCase();
                    });

                    var indexFromSelected = _.indexOf(upperSelectedArray, val.name.toUpperCase());
                    if (indexFromSelected == -1) {
                        // must recreate the array for angular to recongize change
                        $scope.calculation.Categories.push(val.name);
                    }

                } else {

                    $scope.ExistingCategories.push({ name: val, ticked: true });
                    $scope.calculation.Categories.push(val);
                }

                // must recreate the array for angular to recongize change
                $scope.calculation.Categories = $scope.calculation.Categories.slice(0);
                $scope.ExistingCategories = $scope.ExistingCategories.slice(0);

                $scope.NewCategory = '';
            };

            // 'init'                                
            loadReferenceData();

            // this is done to wait for all promises to have completed.
            $q.all([
                $scope.CalculationTypes.$promise,
                $scope.DataTypes.$promise,
                $scope.SummarySource.$promise
            ]).then(function(data) {
                refreshData();
                //setInterval(function () { prettyPrint(); }, 10000);
            });

            // ace editor event handler
            $scope.aceOnLoad = function(editor) {

                //console.log('aceLoaded');

                // Options
                editor.setReadOnly(false);
                editor.setTheme("ace/theme/monokai");
                editor.getSession().setMode("ace/mode/csharp");
                editor.setPrintMarginColumn(false);
                editor.setAutoScrollEditorIntoView(true);
                editor.setOption("maxLines", 80);
                editor.setOption("minLines", 5);
                editor.setFontSize(12);
                editor.showGutter = true;
                
                //editor.style.fontSize = '18px';

                //editor.getSession().insert("Awesome!");
                
                //console.log(ace);

                //var StatusBar = ace.require("ace/ext/statusbar").StatusBar;
                // create a simple selection status indicator
                //var statusBar = new StatusBar(editor, document.getElementById("statusBar"));

                //var langTools = ace.require("ace/ext/language_tools");
                //console.log(langTools);
                //editor.setOptions({ enableBasicAutocompletion: true });

                //// uses http://rhymebrain.com/api.html
                //var rhymeCompleter = {
                //    getCompletions: function(editor, session, pos, prefix, callback) {
                //        if (prefix.length === 0) {
                //            callback(null, []);
                //            return;
                //        }
                //        $.getJSON(
                //            "http://rhymebrain.com/talk?function=getRhymes&word=" + prefix,
                //            function(wordList) {
                //                // wordList like [{"word":"flow","freq":24,"score":300,"flags":"bc","syllables":"1"}]
                //                callback(null, wordList.map(function(ea) {
                //                    return { name: ea.word, value: ea.word, score: ea.score, meta: "rhyme" };
                //                }));
                //            });
                //    }
                //};

                //langTools.addCompleter(rhymeCompleter);

                //editor.style.fontSize = '28px';

                //editor.resize();

                //// Options
                //_editor.setReadOnly(true);
                //_session.setUndoManager(new ace.UndoManager());
                //_renderer.setShowGutter(false);

                //// Events
                //_editor.on("changeSession", function(){ ... });
                //_session.on("change", function(){ ... });
                
            };

            $scope.aceOnChange = function(e) {
                //console.log(e);
                //e[1].getSession().insert("Awesome!");

                //e[1].blur();

                //var editor = e[1];
                
                //var d = editor.selection.getCursor();                
                //console.log(d);
                
                ////editor.selection.selectAll();
                
                ////var d2 = editor.selection.getRange();
                ////console.log(d2);
                
                //var code = editor.getSession().getValue();              
                //console.log({ code: code });
                
                //editor.find('return', {
                //    backwards: true,
                //    wrap: false,
                //    caseSensitive: false,
                //    wholeWord: false,
                //    regExp: false
                //});
                //editor.findNext();
                //editor.findPrevious();

                //console.log($scope.calculation.DataRepository);
                //$rootScope.$broadcast("SelectedEntitySetEvent", { SelectedEntityId: $scope.calculation.DataRepository });
                
                //e[1].selection.moveCursorLineStart();                                
            };

            $scope.launchInlineModal = function() {
                
            };

            $scope.HideExplorerSection = false;            
            
            $scope.ShowExplorer = function () {
                if ($scope.HideExplorerSection) {
                    $scope.ExplorerColsNon = 'col-sm-10';
                    $scope.ExplorerCols = 'col-sm-0';
                } else {
                    $scope.ExplorerColsNon = 'col-sm-7';
                    $scope.ExplorerCols = 'col-sm-3';                    
                }
            };

            $scope.ShowExplorer();
        }
    ]);


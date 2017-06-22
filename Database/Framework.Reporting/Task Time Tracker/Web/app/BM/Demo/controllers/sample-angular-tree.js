'use strict';

angular.module('rootAppShell')
    .controller('treeCtrl2',
	[
        '$scope'
	,	function ($scope) {

			$scope.treeOptions = {
				nodeChildren: "children",
				dirSelectable: true,
				
			injectClasses: {
				ul: "a1",
				li: "a2",
				liSelected: "a7",
				iExpanded: "a3",
				iCollapsed: "a4",
				iLeaf: "a5",
				label: "a6",
				labelSelected: "a8"
			}
		}

		$scope.treeOptions2 = {
			nodeChildren: "children",
			dirSelectable: false,
			multiSelect: true,
			injectClasses: {
				ul: "a1",
				li: "a2",
				liSelected: "a7",
				iExpanded: "a3",
				iCollapsed: "a4",
				iLeaf: "a5",
				label: "a6",
				labelSelected: "a8"
			}
		}

		$scope.dataForTheTree =
		[
			{
				"name": "Joe", "age": "21", "children": [
				  { "name": "Smith", "age": "42", "children": [] },
				  {
				  	"name": "Gary", "age": "21", "children": [
					  {
					  	"name": "Jenifer", "age": "23", "children": [
						  { "name": "Dani", "age": "32", "children": [] },
						  { "name": "Max", "age": "34", "children": [] }
					  	]
					  }
				  	]
				  }
				]
			},
			{ "name": "Albert", "age": "33", "children": [] },
			{ "name": "Ron", "age": "29", "children": [] }
		];

		$scope.dataForTheTreeV2 =
		[
			{
				"name": "Joe", "age": "21", "children": [
				  { "name": "Smith", "age": "42", "children": [] },
				  {
				  	"name": "Gary", "age": "21", "children": [
					  {
					  	"name": "Jenifer", "age": "23", "children": [
						  { "name": "Dani", "age": "32", "children": [] },
						  { "name": "Max", "age": "34", "children": [] }
					  	]
					  }
				  	]
				  }
				]
			},
			{ "name": "Albert", "age": "33", "children": [] },
			{ "name": "Ron", "age": "29", "children": [] }
		];

		$scope.treedata = $scope.dataForTheTreeV2; //createSubTree(3, 4, "");

		$scope.showToggle = function (node, expanded) {
			$("#events-listing").append("•"+node.label+ (expanded?" expanded":" collapsed") + "");
		};

		$scope.showSelected = function(node, selected) {
			$("#events-listing").append("•"+node.label+ (selected?" selected":" deselected") + "");
		};
	}
]);
'use strict';

angular.module('rootAppShell')
    .factory('calculationSelectionService', ['$log',
        function ($log) {
        
        // expecteing calculations array who has Categories
        var getUniqueTags = function (calculations) {
            
            var items = [];
            
            angular.forEach(calculations, function (item) {
                items.push(item.Categories);
            });
            
            if (_.isUndefined(items)) return [];

            var answer = _.chain(items)
              //.pluck('ActiveStatusCode')
              .flatten()
              .unique()
              .value()
              .sort();

            return answer;
        };
            
        var grouped = [];
        
        var groupByCategory = function (uniqueCategories, calculations, selectedItems, hideOrSelect, chunkedDivisor) {

            var unclassifiedKey = '{Not classified}';

            // add to beginging to bucket calculations without buckets
            if (uniqueCategories.indexOf(unclassifiedKey) < 0) {                
                uniqueCategories.unshift(unclassifiedKey);
            }
            
            angular.forEach(uniqueCategories, function (category) {
                grouped[category] = [];
            });
            
            angular.forEach(calculations, function (item) {

                var selected = (selectedItems.indexOf(item.CalculationCode) > -1);
                                
                if (hideOrSelect == 'Hide' && selected) {
                    // skip it
                } else {

                    if (item.Categories.length > 0) {
                        angular.forEach(item.Categories, function (category) {
                            grouped[category].push({ Selected: selected, Calculation: item });
                        });
                    } else {
                        grouped[unclassifiedKey].push({ Selected: selected, Calculation: item });;
                    }                    
                }

            });
            
            var groupedResult = [];
            angular.forEach(uniqueCategories, function (category) {                
                if (grouped[category].length > 0) {
                    groupedResult.push({ Category: category, Items: grouped[category] });
                }                
            });

            var answer = chunk(groupedResult, chunkedDivisor);
            
            return answer;
        };
        
        var chunk = function (array, chunkSize) {

            return [].concat.apply([],
                array.map(function (elem, i) {
                    return i % chunkSize ? [] : [array.slice(i, i + chunkSize)];
                })
            );

        };
        
        var getSelectedItems = function (groupedCollection) {

            // flatten to list of items
            var flattenList = _.chain(groupedCollection)
                .flatten()
                .pluck('Items')
                .flatten()
                .value();                            
            
            // find those that are selected
            var itemsOfInterest = _.filter(flattenList, function (item) { return item.Selected; });

            // get array of CalcuationCode
            var result = _.chain(itemsOfInterest)
                .flatten()
                .pluck('Calculation')
                .flatten()                
                .value();

            return result;
        };
        
        return {
            getUniqueTags: getUniqueTags,
            groupByCategory: groupByCategory,
            chunk: chunk,
            getSelectedItems: getSelectedItems,
            parentGrouped: grouped,
        };
    }]);
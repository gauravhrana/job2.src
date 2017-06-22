'use strict';

angular.module('rootAppShell')
    .service('utilityUIService',
        function () {
                        
            this.getMatchingReferences = function(fixedList, selectedList, propertyToMatchOn) {

                // to object reference evaluation in javascript
                var selectedArrayList = [];

                if (selectedList == null) return selectedArrayList;

                for (var i = 0; i < selectedList.length; i++) {

                    var itemId = selectedList[i][propertyToMatchOn];

                    for (var j = 0; j < fixedList.length; j++) {

                        var item = fixedList[j];

                        if (itemId === item[propertyToMatchOn]) {
                            selectedArrayList.push(item[propertyToMatchOn]);
                            break;
                        }
                    }
                }

                //console.log(fixedList);                
                //console.log(selectedArrayList);

                return selectedArrayList;
            };

            this.getMatchingReferencesFromProperty = function(fixedList, propertyList, propertyToMatchOn) {

                // to object reference evaluation in javascript
                var listReturn = [];

                for (var i = 0; i < propertyList.length; i++) {

                    var itemId = propertyList[i];

                    for (var j = 0; j < fixedList.length; j++) {

                        var item = fixedList[j];

                        if (itemId === item[propertyToMatchOn]) {
                            listReturn.push(item);
                            break;
                        }
                    }
                }

                return listReturn;
            };
            
            //this.getMatchingReferencesSingle = function (fixedList, propertyValue, propertyToMatchOn) {

            //    // to object reference evaluation in javascript
            //    var selectedArrayList = [];

            //    console.log('getMatchingReferencesSingle');
            //    console.log(fixedList);
            //    console.log(propertyValue);

            //    for (var j = 0; j < fixedList.length; j++) {

            //        var item = fixedList[j];

            //        console.log('j :');
            //        console.log(item);

            //        if (propertyValue.toString() == item[propertyToMatchOn].toString()) {
            //            //selectedArrayList.push(item[propertyToMatchOn]);
            //            return item;
            //            break;
            //        }
            //    }

            //    return selectedArrayList;
            //};
        });


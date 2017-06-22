'use strict';

angular.module('rootAppShell')
    .factory('cachingService', function() {

        var savedData = [];

        function set(key, data) {
            savedData[key] = data;            
        }

        function get(key) {
            return savedData[key];
        }

        return {
            set: set,
            get: get
        };
        
    });
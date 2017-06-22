'use strict';

angular.module('rootAppShell')
    .factory('jiraListService', [
        '$resource',
        function ($resource) {
            return $resource('./api/JiraManager/:detailId'
                , null
                , {                    
                    'getJIRAIssues': { method: 'GET', url: '../../apiV2/JiraManager/ListJiraIssues', isArray: true }
                        , 'create': { method: 'POST', url: '../../apiV2/JiraManager/Create' }
                        , 'update': { method: 'POST', url: '../../apiV2/JiraManager/Update' }
                        , 'delete': { method: 'DELETE', url: '../../apiV2/JiraManager/Delete/:detailId' }
                }
                );
        }
    ]);


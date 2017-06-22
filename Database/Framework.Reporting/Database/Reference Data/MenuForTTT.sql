-- "Help Sub menu"
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'BetaDevelopmentII'
 ,	@ParentMenuId = 7
 ,	@Description = 'Beta Development II . . .'
 ,	@SortOrder = 43
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'

-- "Authorization Sub menu"
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Application'
 ,	@ParentMenuId = 8
 ,	@Description = 'Application'
 ,	@SortOrder = 43
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Configuration/Application/Default.aspx'

 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Application User'
 ,	@ParentMenuId = 8
 ,	@Description = 'Application User'
 ,	@SortOrder = 44
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/AuthenticationAndAuthorization/ApplicationUser/Default.aspx'

 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Application Role'
 ,	@ParentMenuId = 8
 ,	@Description = 'Application Role'
 ,	@SortOrder = 45
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/AuthenticationAndAuthorization/ApplicationRole/Default.aspx'

 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Application Operation'
 ,	@ParentMenuId = 8
 ,	@Description = 'Application Operation'
 ,	@SortOrder = 46
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/AuthenticationAndAuthorization/ApplicationOperation/Default.aspx'

 -- "Configuration Sub menu"
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Application Entity Field Label'
 ,	@ParentMenuId = 9
 ,	@Description = 'Application Entity Field Label'
 ,	@SortOrder = 47
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Configuration/ApplicationEntityFieldLabel/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Application Entity Parental Hierarchy'
 ,	@ParentMenuId = 9
 ,	@Description = 'Application Entity Parental Hierarchy'
 ,	@SortOrder = 48
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Configuration/ApplicationEntityParentalHierarchy/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Menu'
 ,	@ParentMenuId = 9
 ,	@Description = 'Menu'
 ,	@SortOrder = 49
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Configuration/Menu/Default.aspx'

 -- "Verification Sub menu"
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Generic Details'
 ,	@ParentMenuId = 10
 ,	@Description = 'Generic Details'
 ,	@SortOrder = 50
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/TestAndAuditDetails.aspx'

 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Demo'
 ,	@ParentMenuId = 10
 ,	@Description = 'Demo'
 ,	@SortOrder = 51
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/ApplicationManagement/Development/Demo/Default.aspx'

 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Entity Test Data'
 ,	@ParentMenuId = 10
 ,	@Description = 'Entity Test Data'
 ,	@SortOrder = 52
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/ApplicationManagement/Development/TestData/EntityTestData.aspx'

 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Records With Missing History'
 ,	@ParentMenuId = 10
 ,	@Description = 'Records With Missing History'
 ,	@SortOrder = 53
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/ApplicationManagement/Development/TestData/RecordsWithMissingHistory.aspx'

 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Broken History Records'
 ,	@ParentMenuId = 10
 ,	@Description = 'Broken History Records'
 ,	@SortOrder = 54
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/ApplicationManagement/Development/TestData/BrokenHistoryRecords.aspx'

 -- "Location Services . . . Sub menu"
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'TimeZone'
 ,	@ParentMenuId = 11
 ,	@Description = 'TimeZone'
 ,	@SortOrder = 55
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/TimeZone/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Country'
 ,	@ParentMenuId = 11
 ,	@Description = 'Country'
 ,	@SortOrder = 56
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/Country/Default.aspx'

 -- "Location Services . . . Sub menu"
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'TimeZone'
 ,	@ParentMenuId = 11
 ,	@Description = 'TimeZone'
 ,	@SortOrder = 55
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/TimeZone/Default.aspx'

 -- "Risk Reward Metrics Sub menu"
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Risk'
 ,	@ParentMenuId = 20
 ,	@Description = 'Risk'
 ,	@SortOrder = 57
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/RiskAndReward/Risk/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Reward'
 ,	@ParentMenuId = 20
 ,	@Description = 'Reward'
 ,	@SortOrder = 58
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/RiskAndReward/Reward/Default.aspx'

 -- "Skill Metrics Sub menu"
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Skill'
 ,	@ParentMenuId = 21
 ,	@Description = 'Skill'
 ,	@SortOrder = 59
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Aptitude/Skill/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Skill Level'
 ,	@ParentMenuId = 21
 ,	@Description = 'Skill Level'
 ,	@SortOrder = 60
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Aptitude/SkillLevel/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Preferences'
 ,	@ParentMenuId = 21
 ,	@Description = 'Preferences . . .'
 ,	@SortOrder = 61
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Configuration/UserPreferenceDataType/Default.aspx'

 -- "Task Sub menu"
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Task Type'
 ,	@ParentMenuId = 28
 ,	@Description = 'Task Type'
 ,	@SortOrder = 62
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/WBS/TaskType/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Task Package'
 ,	@ParentMenuId = 28
 ,	@Description = 'Task Package'
 ,	@SortOrder = 63
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/WBS/TaskPackage/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Task Priority Type'
 ,	@ParentMenuId = 28
 ,	@Description = 'Task Priority Type'
 ,	@SortOrder = 64
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/WBS/TaskPriorityType/Default.aspx'

 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Task'
 ,	@ParentMenuId = 28
 ,	@Description = 'Task'
 ,	@SortOrder = 65
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/WBS/Task/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Task Person Mapping'
 ,	@ParentMenuId = 28
 ,	@Description = 'Task Person Mapping'
 ,	@SortOrder = 66
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/WBS/TaskPersonMapping/TaskView.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Task Formulation'
 ,	@ParentMenuId = 28
 ,	@Description = 'Task Formulation'
 ,	@SortOrder = 67
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/WBS/TaskFormulation/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'TaskPriorityXPerson'
 ,	@ParentMenuId = 28
 ,	@Description = 'Task Priority X Person'
 ,	@SortOrder = 68
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/WBS/TaskPriorityXPerson/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'TaskRiskRewardRankingXPerson'
 ,	@ParentMenuId = 28
 ,	@Description = 'Task Risk Reward Ranking X Person'
 ,	@SortOrder = 69
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/WBS/TaskRiskRewardRankingXPerson/Default.aspx'

 -- "Activity Sub menu"
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Activity'
 ,	@ParentMenuId = 29
 ,	@Description = 'Activity'
 ,	@SortOrder = 70
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/WBS/Activity/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'ActivityAlgorithm'
 ,	@ParentMenuId = 29
 ,	@Description = 'Activity Algorithm'
 ,	@SortOrder = 71
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/WBS/ActivityAlgorithm/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'ActivityAlgorithmItem'
 ,	@ParentMenuId = 29
 ,	@Description = 'Activity Algorithm Item'
 ,	@SortOrder = 72
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/WBS/ActivityAlgorithmItem/Default.aspx'

 -- "About DB.. Sub menu"
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'ReleaseLog'
 ,	@ParentMenuId = 41
 ,	@Description = 'Release Log'
 ,	@SortOrder = 73
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/ApplicationManagement/ReleaseLog/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Release Log Detail'
 ,	@ParentMenuId = 41
 ,	@Description = 'ReleaseLogDetail'
 ,	@SortOrder = 74
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/ApplicationManagement/ReleaseLogDetail/Default.aspx'

 -- "Beta Development . . . Sub menu"
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'ActivityStream'
 ,	@ParentMenuId = 42
 ,	@Description = 'Activity Stream'
 ,	@SortOrder = 75
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/ApplicationManagement/Development/ActivityStream/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'TypeOfIssue'
 ,	@ParentMenuId = 42
 ,	@Description = 'Type Of Issue'
 ,	@SortOrder = 76
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/TypeOfIssue/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Audit'
 ,	@ParentMenuId = 42
 ,	@Description = 'Audit . . .'
 ,	@SortOrder = 81
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/Audit/AuditAction/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Import'
 ,	@ParentMenuId = 42
 ,	@Description = 'Import . . .'
 ,	@SortOrder = 82
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/FileType/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'TasksAndWorkflow'
 ,	@ParentMenuId = 42
 ,	@Description = 'TasksAndWorkflow . . .'
 ,	@SortOrder = 83
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/TasksAndWorkflow/TaskEntityType/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Monitored Events'
 ,	@ParentMenuId = 42
 ,	@Description = 'Monitored Events . . .'
 ,	@SortOrder = 84
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/EventMonitoring/ApplicationMonitoredEventSource/Default.aspx'
 
 
 -- "Beta Development II . . . Sub menu" 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'App User to App Role Mapping'
 ,	@ParentMenuId = 10080
 ,	@Description = 'App User to App Role Mapping'
 ,	@SortOrder = 77
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/AuthenticationAndAuthorization/ApplicationRoleMapping/PersonView.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'App Operation to App Role Mapping'
 ,	@ParentMenuId = 10080
 ,	@Description = 'App Operation to App Role Mapping'
 ,	@SortOrder = 78
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/AuthenticationAndAuthorization/ApplicationOperationRoleMapping/ApplicationOperationView.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Project - Need Mapping'
 ,	@ParentMenuId = 10080
 ,	@Description = 'Project - Need Mapping'
 ,	@SortOrder = 79
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/ProjectXNeed/ProjectView.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Need - Feature Mapping'
 ,	@ParentMenuId = 10080
 ,	@Description = 'Need - Feature Mapping'
 ,	@SortOrder = 80
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/NeedXFeature/NeedView.aspx'
 
 -- "Preferences . . . Sub menu"
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'UserPreferenceDataType'
 ,	@ParentMenuId = 10036
 ,	@Description = 'User Preference Data Type'
 ,	@SortOrder = 85
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Configuration/UserPreferenceDataType/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'UserPreferenceCategory'
 ,	@ParentMenuId = 10036
 ,	@Description = 'User Preference Category'
 ,	@SortOrder = 86
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Configuration/UserPreferenceCategory/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'UserPreferenceKey'
 ,	@ParentMenuId = 10036
 ,	@Description = 'User Preference Key'
 ,	@SortOrder = 87
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Configuration/UserPreferenceKey/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'UserPreference'
 ,	@ParentMenuId = 10036
 ,	@Description = 'User Preference'
 ,	@SortOrder = 88
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Configuration/UserPreference/Default.aspx?user=admin'


 -- "Audit . . . Sub menu" 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'AuditAction'
 ,	@ParentMenuId = 10056
 ,	@Description = 'Audit Action'
 ,	@SortOrder = 89
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/Audit/AuditAction/Default.aspx'

 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'AuditHistory'
 ,	@ParentMenuId = 10056
 ,	@Description = 'Audit History'
 ,	@SortOrder = 90
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/Audit/AuditHistory/Default.aspx'


 -- "Import . . . Sub menu" 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'FileType'
 ,	@ParentMenuId = 10057
 ,	@Description = 'File Type'
 ,	@SortOrder = 91
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/FileType/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'BatchFileStatus'
 ,	@ParentMenuId = 10057
 ,	@Description = 'Batch File Status'
 ,	@SortOrder = 92
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/BatchFileStatus/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'Batch File Set'
 ,	@ParentMenuId = 10057
 ,	@Description = 'Batch File Set'
 ,	@SortOrder = 93
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/BatchFileSet/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'BatchFile'
 ,	@ParentMenuId = 10057
 ,	@Description = 'Import (BatchFile)'
 ,	@SortOrder = 94
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/Import/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'BatchFileHistory'
 ,	@ParentMenuId = 10057
 ,	@Description = 'Batch File History'
 ,	@SortOrder = 95
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/BatchFileHistory/Default.aspx'


 -- "TasksAndWorkflow . . . Sub menu" 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'TaskEntityType'
 ,	@ParentMenuId = 10058
 ,	@Description = 'Task Entity Type'
 ,	@SortOrder = 96
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/TasksAndWorkflow/TaskEntityType/Default.aspx'

 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'TaskScheduleType'
 ,	@ParentMenuId = 10058
 ,	@Description = 'Task Schedule Type'
 ,	@SortOrder = 97
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/TasksAndWorkflow/TaskScheduleType/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'TaskEntity'
 ,	@ParentMenuId = 10058
 ,	@Description = 'Task Entity'
 ,	@SortOrder = 98
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/TasksAndWorkflow/TaskEntity/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'TaskSchedule'
 ,	@ParentMenuId = 10058
 ,	@Description = 'Task Schedule'
 ,	@SortOrder = 99
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/TasksAndWorkflow/TaskSchedule/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'TaskRun'
 ,	@ParentMenuId = 10058
 ,	@Description = 'Task Run'
 ,	@SortOrder = 100
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/TasksAndWorkflow/TaskRun/Default.aspx'


 -- "Monitored Events . . . Sub menu" 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'ApplicationMonitoredEventSource'
 ,	@ParentMenuId = 10059
 ,	@Description = 'Application Monitored Event Source'
 ,	@SortOrder = 101
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/EventMonitoring/ApplicationMonitoredEventSource/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'ApplicationMonitoredEventProcessingState'
 ,	@ParentMenuId = 10059
 ,	@Description = 'Application Monitored Event ProcessingState'
 ,	@SortOrder = 102
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/EventMonitoring/ApplicationMonitoredEventProcessingState/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 100
 ,	@Name = 'ApplicationMonitoredEventEmail'
 ,	@ParentMenuId = 10059
 ,	@Description = 'Application Monitored Event Email'
 ,	@SortOrder = 103
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/EventMonitoring/ApplicationMonitoredEventEmail/Default.aspx'
 
  

--"Main Menu"
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Home'
 ,	@ParentMenuId = NULL
 ,	@Description = 'Home'
 ,	@SortOrder = 1
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Default.aspx'
 
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Admin'
 ,	@ParentMenuId = NULL
 ,	@Description = 'Admin'
 ,	@SortOrder = 2
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'
 
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Audit'
 ,	@ParentMenuId = NULL
 ,	@Description = 'Audit'
 ,	@SortOrder = 3
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'
 
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Coordinator'
 ,	@ParentMenuId = NULL
 ,	@Description = 'Coordinator'
 ,	@SortOrder = 4
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'
 
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Users'
 ,	@ParentMenuId = NULL
 ,	@Description = 'Users'
 ,	@SortOrder = 5
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'
 
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Preferences'
 ,	@ParentMenuId = NULL
 ,	@Description = 'Preferences'
 ,	@SortOrder = 6
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'
 
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Reports'
 ,	@ParentMenuId = NULL
 ,	@Description = 'Reports'
 ,	@SortOrder = 7
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'
 
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Help'
 ,	@ParentMenuId = NULL
 ,	@Description = 'Help'
 ,	@SortOrder = 8
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'

 --------------------------------------------------------------------------------------------------------------------------------
 -- "2nd Level Menus

 -- "Admin Sub Menus"
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'ApplicationUser'
 ,	@ParentMenuId = 10082
 ,	@Description = 'Application User'
 ,	@SortOrder = 9
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/AuthenticationAndAuthorization/ApplicationUser/Default.aspx'

 
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'SystemEntityType'
 ,	@ParentMenuId = 10082
 ,	@Description = 'System Entity Type'
 ,	@SortOrder = 10
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/SystemEntityType/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'ApplicationRole'
 ,	@ParentMenuId = 10082
 ,	@Description = 'Application Role'
 ,	@SortOrder = 12
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~AuthenticationAndAuthorization/ApplicationRole/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'LogInSettings'
 ,	@ParentMenuId = 10082
 ,	@Description = 'LogIn Settings'
 ,	@SortOrder = 11
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/LogInSettings.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'PersonView-Role Mapping'
 ,	@ParentMenuId = 10082
 ,	@Description = 'Person View - Role Mapping'
 ,	@SortOrder = 13
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/ApplicationRoleMapping/PersonView.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'RoleView-RoleMapping'
 ,	@ParentMenuId = 10082
 ,	@Description = 'Role View - Role Mapping'
 ,	@SortOrder = 14
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Admin/ApplicationRoleMapping/RoleView.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Student'
 ,	@ParentMenuId = 10082
 ,	@Description = 'Student'
 ,	@SortOrder = 15
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Student/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Teacher'
 ,	@ParentMenuId = 10082
 ,	@Description = 'Teacher'
 ,	@SortOrder = 16
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Teacher/Default.aspx'

 -- "Audit Sub Menus" 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'AuditAction'
 ,	@ParentMenuId = 10083
 ,	@Description = 'Audit Action'
 ,	@SortOrder = 17
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Audit/AuditAction/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'AuditHistory'
 ,	@ParentMenuId = 10083
 ,	@Description = 'Audit History'
 ,	@SortOrder = 18
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Audit/AuditHistory/Default.aspx'

 -- "Coordinator Sub Menus" 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'FoodType'
 ,	@ParentMenuId = 10084
 ,	@Description = 'Food Type'
 ,	@SortOrder = 19
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/FoodType/Default.aspx'

 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'MealType'
 ,	@ParentMenuId = 10084
 ,	@Description = 'Meal Type'
 ,	@SortOrder = 20
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/MealType/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Discount'
 ,	@ParentMenuId = 10084
 ,	@Description = 'Discount'
 ,	@SortOrder = 21
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Discount/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'PaymentMethod'
 ,	@ParentMenuId = 10084
 ,	@Description = 'Payment Method'
 ,	@SortOrder = 22
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/PaymentMethod/Default.aspx'

 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'ActivityType'
 ,	@ParentMenuId = 10084
 ,	@Description = 'Activity Type'
 ,	@SortOrder = 23
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/ActivityType/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'ActivitySubType'
 ,	@ParentMenuId = 10084
 ,	@Description = 'Activity Sub Type'
 ,	@SortOrder = 24
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/ActivitySubType/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'EventType'
 ,	@ParentMenuId = 10084
 ,	@Description = 'Event Type'
 ,	@SortOrder = 25
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/EventType/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'DiaperStatus'
 ,	@ParentMenuId = 10084
 ,	@Description = 'Diaper Status'
 ,	@SortOrder = 26
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/DiaperStatus/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'NeedItem'
 ,	@ParentMenuId = 10084
 ,	@Description = 'Need Item'
 ,	@SortOrder = 27
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/NeedItem/Default.aspx'

 -- "Users" Sub menu
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Meal'
 ,	@ParentMenuId = 10085
 ,	@Description = 'Meal'
 ,	@SortOrder = 28
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Meal/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'MealDetail'
 ,	@ParentMenuId = 10085
 ,	@Description = 'MealDetail'
 ,	@SortOrder = 29
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/MealDetail/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Activity'
 ,	@ParentMenuId = 10085
 ,	@Description = 'Activity'
 ,	@SortOrder = 30
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Activity/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'AccidentReport'
 ,	@ParentMenuId = 10085
 ,	@Description = 'Accident Report'
 ,	@SortOrder = 31
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/AccidentReport/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'AccidentPlace'
 ,	@ParentMenuId = 10085
 ,	@Description = 'Accident Place'
 ,	@SortOrder = 32
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/AccidentPlace/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Sleep'
 ,	@ParentMenuId = 10085
 ,	@Description = 'Sleep'
 ,	@SortOrder = 33
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Sleep/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Comment'
 ,	@ParentMenuId = 10085
 ,	@Description = 'Comment'
 ,	@SortOrder = 34
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Comment/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Tuition'
 ,	@ParentMenuId = 10085
 ,	@Description = 'Tuition'
 ,	@SortOrder = 35
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Tuition/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'SickReport'
 ,	@ParentMenuId = 10085
 ,	@Description = 'Sick Report'
 ,	@SortOrder = 36
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/SickReport/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Bathroom'
 ,	@ParentMenuId = 10085
 ,	@Description = 'Bathroom'
 ,	@SortOrder = 37
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Bathroom/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'Needs'
 ,	@ParentMenuId = 10085
 ,	@Description = 'Needs'
 ,	@SortOrder = 38
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/Needs/Default.aspx'
 
 -- "Preferences" Sub Menu 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'UserPreferenceDataType'
 ,	@ParentMenuId = 10086
 ,	@Description = 'User Preference Data Type'
 ,	@SortOrder = 39
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/UserPerferences/UserPreferenceDataType/Default.aspx'

 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'UserPreferenceKey'
 ,	@ParentMenuId = 10086
 ,	@Description = 'User Preference Key'
 ,	@SortOrder = 40
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/UserPerferences/UserPreferenceKey/Default.aspx'
 
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'UserPreferences'
 ,	@ParentMenuId = 10086
 ,	@Description = 'User Preferences'
 ,	@SortOrder = 41
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/UserPerferences/UserPreferences/Default.aspx'
 
 -- "Help" Sub Menu
 EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 200
 ,	@Name = 'About'
 ,	@ParentMenuId = 10088
 ,	@Description = 'About'
 ,	@SortOrder = 42
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '~/About.aspx'
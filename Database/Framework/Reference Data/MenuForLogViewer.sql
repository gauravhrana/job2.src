
--"Main Menu"
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 800
 ,	@Name = 'File'
 ,	@ParentMenuId = NULL
 ,	@Description = 'File'
 ,	@SortOrder = 1
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'
 
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 800
 ,	@Name = 'View'
 ,	@ParentMenuId = NULL
 ,	@Description = 'View'
 ,	@SortOrder = 2
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'
 
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 800
 ,	@Name = 'Tools'
 ,	@ParentMenuId = NULL
 ,	@Description = 'Tools'
 ,	@SortOrder = 2
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'
 
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 800
 ,	@Name = 'About'
 ,	@ParentMenuId = NULL
 ,	@Description = 'About'
 ,	@SortOrder = 4
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'

 
 -- "View Sub Menu"
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 800
 ,	@Name = 'Toolbars'
 ,	@ParentMenuId = 10124
 ,	@Description = 'Toolbars'
 ,	@SortOrder = 5
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'
 
 -- "Tools Sub Menu"
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 800
 ,	@Name = 'Options'
 ,	@ParentMenuId = 10125
 ,	@Description = 'Options'
 ,	@SortOrder = 6
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'
 
 -- "About Sub Menu"
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 800
 ,	@Name = 'ReleaseLog'
 ,	@ParentMenuId = 10126
 ,	@Description = 'Release Log'
 ,	@SortOrder = 7
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'

EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 800
 ,	@Name = 'ReleaseLogDetail'
 ,	@ParentMenuId = 10126
 ,	@Description = 'Release Log Detail'
 ,	@SortOrder = 8
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'
 
 -- "Toolbars Sub Menu"
EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 800
 ,	@Name = 'MenuBar'
 ,	@ParentMenuId = 10127
 ,	@Description = 'Menu Bar'
 ,	@SortOrder = 9
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'

EXEC dbo.MenuInsert
	@AuditId = 10
 ,	@ApplicationId = 800
 ,	@Name = 'StatusBar'
 ,	@ParentMenuId = 10127
 ,	@Description = 'Status Bar'
 ,	@SortOrder = 10
 ,	@IsChecked = 0
 ,	@IsVisible = 1
 ,	@NavigateURL = '#'